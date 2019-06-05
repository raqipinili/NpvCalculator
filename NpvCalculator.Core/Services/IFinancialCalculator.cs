using NpvCalculator.Core.Classes;
using System.Collections.Generic;

namespace NpvCalculator.Core.Services
{
    public interface IFinancialCalculator
    {
        double CalculatePresentValue(double futureValue, double rate, double exponent);
        IEnumerable<PeriodAmount> CalculatePresentValueMulti(double futureValue, double rate, int periods);
        double CalculateFutureValue(double presentValue, double rate, double exponent);
        IEnumerable<PeriodAmount> CalculateFutureValueMulti(double presentValue, double rate, int periods);
        double CalculateNetPresentValue(double initialInvestment, double rate, IEnumerable<PeriodAmount> cashFlows);
        IEnumerable<NetPresentValuePerRate> CalculateNetPresentValueDynamicRate(
            double initialInvestment,
            double lowerBoundDiscountRate,
            double upperBoundDiscountRate,
            double discountRateIncrement,
            IEnumerable<PeriodAmount> cashFlows);
    }
}
