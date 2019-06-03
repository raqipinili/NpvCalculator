using NpvCalculator.Core.Classes;
using NpvCalculator.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NpvCalculator.Core
{
    public class FinancialCalculator : IFinancialCalculator
    {
        // PV = FV / ((1 + r) ^ P)
        public double CalculatePresentValue(double futureValue, double rate, double exponent) => futureValue / Math.Pow(1 + rate / 100, exponent);

        // FV = FV * ((1 + r) ^ P)
        public double CalculateFutureValue(double presentValue, double rate, double exponent) => presentValue * Math.Pow(1 + rate / 100, exponent);

        // NPV = FV1 + (FV2 / ((1 + r) ^ P1)) + (FV3 / ((1 + r) ^ P2))
        public double CalculateNetPresentValue(double initialInvestment, double rate, IEnumerable<CashFlow> cashFlows)
        {
            double npv = 0;

            foreach (var cashFlow in cashFlows)
            {
                npv += CalculatePresentValue(cashFlow.Amount, rate, cashFlow.Period + 1);
            }

            return npv - initialInvestment;
        }

        public IEnumerable<NetPresentValue> CalculateNetPresentValueDynamicRate(
            double initialInvestment,
            double lowerBoundDiscountRate,
            double upperBoundDiscountRate,
            double discountRateIncrement,
            IEnumerable<CashFlow> cashFlows)
        {
            FinancialValidator.ValidateRate(lowerBoundDiscountRate);
            FinancialValidator.ValidateRate(upperBoundDiscountRate);
            FinancialValidator.ValidateRateIncrement(upperBoundDiscountRate, discountRateIncrement);
            cashFlows.ToList().ForEach(cf => FinancialValidator.ValidateExponent(cf.Period));

            var netPresentValues = new List<NetPresentValue>();

            for (double rate = lowerBoundDiscountRate; rate <= upperBoundDiscountRate; rate += discountRateIncrement)
            {
                var value = CalculateNetPresentValue(initialInvestment, rate, cashFlows);

                netPresentValues.Add(new NetPresentValue()
                {
                    Value = value,
                    Rate = rate
                });
            }

            return netPresentValues;
        }
    }
}
