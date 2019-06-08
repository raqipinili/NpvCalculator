using NpvCalculator.Core.Classes;
using NpvCalculator.Data;
using Entities = NpvCalculator.Data.Entities;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace NpvCalculator.Core.Services
{
    public class FinancialService : IFinancialService
    {
        private readonly ApplicationDbContext _context;

        public FinancialService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddNetPresentValue(Guid userId, NetPresentValueRequest request)
        {
            //if (await _context.NetPresentValues.AnyAsync(r => r.Name == request.Name))
            //    throw new Exception($"Error: NetPresentValue '{request.Name}' already exists");

            var npv = new Entities.NetPresentValue()
            {
                Name = null,
                InitialInvestment = request.InitialInvestment,
                LowerBoundDiscountRate = request.LowerBoundDiscountRate,
                UpperBoundDiscountRate = request.UpperBoundDiscountRate,
                DiscountRateIncrement = request.DiscountRateIncrement,
                CreatedDate = DateTime.UtcNow
            };

            /* https://devblogs.microsoft.com/cesardelatorre/using-resilient-entity-framework-core-sql-connections-and-transactions-retries-with-exponential-backoff/ */
            var strategy = _context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    int saveCount = 0;

                    await _context.NetPresentValues.AddAsync(npv);
                    saveCount = await _context.SaveChangesAsync();

                    var cashFlows = request.CashFlows.Where(cf => cf.Amount != 0).Select(cf => new Entities.PeriodAmount()
                    {
                        NetPresentValueId = npv.NetPresentValueId,
                        Amount = cf.Amount,
                        Period = cf.Period
                    });

                    await _context.PeriodAmounts.AddRangeAsync(cashFlows);
                    saveCount = await _context.SaveChangesAsync();

                    var userNpv = new Entities.UserNetPresentValue()
                    {
                        UserId = userId,
                        NetPresentValueId = npv.NetPresentValueId
                    };

                    await _context.UserNetPresentValues.AddAsync(userNpv);
                    saveCount = await _context.SaveChangesAsync();

                    transaction.Commit();
                }
            });

            return npv.NetPresentValueId;
        }

        public async Task<Entities.NetPresentValue> GetNetPresentValueLatest(Guid userId)
        {
            var result = await (from unpv in _context.UserNetPresentValues
                                join npv in _context.NetPresentValues on unpv.NetPresentValueId equals npv.NetPresentValueId
                                where unpv.UserId == userId
                                orderby npv.CreatedDate descending
                                select npv)
                                .Include(x => x.CashFlows)
                                .FirstOrDefaultAsync();

            return result;
        }
    }
}
