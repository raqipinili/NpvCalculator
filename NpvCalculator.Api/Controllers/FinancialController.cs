using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NpvCalculator.Core.Classes;
using NpvCalculator.Core.Services;
using NpvCalculator.Security.Helpers.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NpvCalculator.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialController : ControllerBase
    {
        private readonly IFinancialCalculator _calcalculator;
        private readonly IFinancialService _service;

        public FinancialController(IFinancialCalculator calcalculator, IFinancialService service)
        {
            _calcalculator = calcalculator;
            _service = service;
        }

        [HttpPost("pv/multi")]
        public IActionResult CalculatePresentValue(PresentValueRequest request)
        {
            var result = _calcalculator.CalculatePresentValueMulti(
                request.FutureValue,
                request.DiscountRate,
                request.Periods);

            return Ok(result);
        }

        [HttpPost("fv/multi")]
        public IActionResult CalculateFutureValue(FutureValueRequest request)
        {
            var result = _calcalculator.CalculateFutureValueMulti(
                request.PresentValue,
                request.InterestRate,
                request.Periods);

            return Ok(result);
        }

        [HttpGet("npv")]
        public IActionResult CalculateNetPresentValue()
        {
            double initialInvestment = 1000D;
            double rate = 12D;

            var cashFlows = new List<PeriodAmount>()
            {
                new PeriodAmount() { Amount = 100D, Period = 1 },
                new PeriodAmount() { Amount = 200D, Period = 2 },
                new PeriodAmount() { Amount = 300D, Period = 3 },
                new PeriodAmount() { Amount = 400D, Period = 4 },
                new PeriodAmount() { Amount = 900D, Period = 5 },
            };

            // 227.15D
            var result = _calcalculator.CalculateNetPresentValue(initialInvestment, rate, cashFlows);
            return Ok(result);
        }

        [HttpPost("npv/dynamicrate")]
        public async Task<IActionResult> CalculateNetPresentValueDynamicRate(NetPresentValueRequest request)
        {
            Guid userId = User.GetUserId();
            var save = await _service.AddNetPresentValue(userId, request);

            var result = _calcalculator.CalculateNetPresentValueDynamicRate(
               request.InitialInvestment,
               request.LowerBoundDiscountRate,
               request.UpperBoundDiscountRate,
               request.DiscountRateIncrement,
               request.CashFlows);

           return Ok(result);
        }

        [HttpGet("npv/getlatest")]
        public async Task<IActionResult> GetNetPresentValueLatest()
        {
            Guid userId = User.GetUserId();
            var npv = await _service.GetNetPresentValueLatest(userId);
            NetPresentValueResponse result = null;

            if (npv != null)
            {
                var cashFlows = npv.CashFlows.Select(cf => new PeriodAmount()
                {
                    Amount = cf.Amount,
                    Period = cf.Period
                });

                result = new NetPresentValueResponse()
                {
                    NetPresentValueId = npv.NetPresentValueId,
                    Name = npv.Name,
                    InitialInvestment = npv.InitialInvestment,
                    LowerBoundDiscountRate = npv.LowerBoundDiscountRate,
                    UpperBoundDiscountRate = npv.UpperBoundDiscountRate,
                    DiscountRateIncrement = npv.DiscountRateIncrement,
                    CashFlows = cashFlows,
                    Results = _calcalculator.CalculateNetPresentValueDynamicRate(
                               npv.InitialInvestment,
                               npv.LowerBoundDiscountRate,
                               npv.UpperBoundDiscountRate,
                               npv.DiscountRateIncrement,
                               cashFlows)
                };
            }
            else
            {
                var cashFlows = new List<PeriodAmount>()
                {
                    new PeriodAmount() { Amount = 100D, Period = 1 },
                    new PeriodAmount() { Amount = 200D, Period = 2 },
                    new PeriodAmount() { Amount = 300D, Period = 3 },
                    new PeriodAmount() { Amount = 400D, Period = 4 },
                    new PeriodAmount() { Amount = 500D, Period = 5 },
                };

                result = new NetPresentValueResponse()
                {
                    NetPresentValueId = 0,
                    Name = null,
                    InitialInvestment = 1500D,
                    LowerBoundDiscountRate = 1D,
                    UpperBoundDiscountRate = 15D,
                    DiscountRateIncrement = .25D,
                    CashFlows = cashFlows,
                    Results = _calcalculator.CalculateNetPresentValueDynamicRate(1500D, 1D, 15D, .25D, cashFlows)
                };
            }

            return Ok(result);
        }
    }
}
