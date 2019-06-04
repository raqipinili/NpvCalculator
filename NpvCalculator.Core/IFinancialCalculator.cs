using NpvCalculator.Core.Classes;
using System.Collections.Generic;

namespace NpvCalculator.Core
{
    public interface IFinancialCalculator
    {
        double CalculatePresentValue(double futureValue, double rate, double exponent);
        IEnumerable<PeriodAmount> CalculatePresentValueMulti(double futureValue, double rate, int periods);
        double CalculateFutureValue(double presentValue, double rate, double exponent);
        IEnumerable<PeriodAmount> CalculateFutureValueMulti(double presentValue, double rate, int periods);
        double CalculateNetPresentValue(double initialInvestment, double rate, IEnumerable<PeriodAmount> cashFlows);
        IEnumerable<NetPresentValue> CalculateNetPresentValueDynamicRate(
            double initialInvestment,
            double lowerBoundDiscountRate,
            double upperBoundDiscountRate,
            double discountRateIncrement,
            IEnumerable<PeriodAmount> cashFlows);
    }
}
