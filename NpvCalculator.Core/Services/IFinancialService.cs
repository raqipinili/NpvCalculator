using NpvCalculator.Core.Classes;
using System;
using System.Threading.Tasks;

namespace NpvCalculator.Core.Services
{
    public interface IFinancialService
    {
        Task<int> AddNetPresentValue(Guid userId, NetPresentValueRequest request);
        Task<Data.Entities.NetPresentValue> GetNetPresentValueLatest(Guid userId);
    }
}
