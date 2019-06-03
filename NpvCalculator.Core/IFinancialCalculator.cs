using NpvCalculator.Core.Classes;
using System.Collections.Generic;

namespace NpvCalculator.Core
{
    public interface IFinancialCalculator
    {
        double CalculatePresentValue(double futureValue, double rate, double exponent);
        double CalculateFutureValue(double presentValue, double rate, double exponent);
        double CalculateNetPresentValue(double initialInvestment, double rate, IEnumerable<CashFlow> cashFlows);
        IEnumerable<NetPresentValue> CalculateNetPresentValueDynamicRate(
            double initialInvestment,
            double lowerBoundDiscountRate,
            double upperBoundDiscountRate,
            double discountRateIncrement,
            IEnumerable<CashFlow> cashFlows);
    }
}
