using NpvCalculator.Core.Classes;
using NpvCalculator.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NpvCalculator.Core.Services
{
    public class FinancialCalculator : IFinancialCalculator
    {
        // PV = FV / ((1 + r) ^ P)
        public double CalculatePresentValue(double futureValue, double rate, double exponent) => futureValue / Math.Pow(1 + rate / 100, exponent);

        public IEnumerable<PeriodAmount> CalculatePresentValueMulti(double futureValue, double rate, int periods)
        {
            var presentValues = new List<PeriodAmount>();

            for (int period = periods; period >= 0; period--)
            {
                presentValues.Add(new PeriodAmount()
                {
                    Period = periods - period,
                    Amount = CalculatePresentValue(futureValue, rate, period)
                });
            }

            return presentValues;
        }

        // FV = FV * ((1 + r) ^ P)
        public double CalculateFutureValue(double presentValue, double rate, double exponent) => presentValue * Math.Pow(1 + rate / 100, exponent);

        public IEnumerable<PeriodAmount> CalculateFutureValueMulti(double presentValue, double rate, int periods)
        {
            var futureValues = new List<PeriodAmount>();

            for (int period = 1; period <= periods; period++)
            {
                futureValues.Add(new PeriodAmount()
                {
                    Period = period,
                    Amount = CalculateFutureValue(presentValue, rate, period)
            });
            }

            return futureValues;
        }

        // Cash Flow must start with period 1
        // NPV = FV1 + (FV2 / ((1 + r) ^ P1)) + (FV3 / ((1 + r) ^ P2))
        public double CalculateNetPresentValue(double initialInvestment, double rate, IEnumerable<PeriodAmount> cashFlows)
        {
            double npv = 0;

            foreach (var cashFlow in cashFlows)
            {
                npv += CalculatePresentValue(cashFlow.Amount, rate, cashFlow.Period);
            }

            return npv - initialInvestment;
        }

        public IEnumerable<NetPresentValue> CalculateNetPresentValueDynamicRate(
            double initialInvestment,
            double lowerBoundDiscountRate,
            double upperBoundDiscountRate,
            double discountRateIncrement,
            IEnumerable<PeriodAmount> cashFlows)
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
                    Amount = value,
                    Rate = rate
                });
            }

            return netPresentValues;
        }
    }
}
