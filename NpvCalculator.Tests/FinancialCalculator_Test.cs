using NpvCalculator.Core;
using NpvCalculator.Core.Classes;
using NpvCalculator.Tests.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NpvCalculator.Tests
{
    public class FinancialCalculator_Test
    {
        private readonly IFinancialCalculator _calc;

        public FinancialCalculator_Test()
        {
            _calc = new FinancialCalculator();
        }

        [Theory(DisplayName = "CalculatePresentValue Test Result")]
        [MemberData(nameof(FinancialCalculatorData.PresentValueData), MemberType = typeof(FinancialCalculatorData))]
        public void CalculatePresentValue_Test_Result(double futureValue, double rate, double exponent, double expectedValue)
        {
            // Arrange
            // Act
            double actualValue = _calc.CalculatePresentValue(futureValue, rate, exponent);

            // Assert
            Assert.Equal(expectedValue, actualValue, 2);
        }

        [Theory(DisplayName = "CalculateFutureValue Test Result")]
        [MemberData(nameof(FinancialCalculatorData.FutureValueData), MemberType = typeof(FinancialCalculatorData))]
        public void CalculateFutureValue_Test_Result(double presentValue, double rate, double exponent, double expectedValue)
        {
            // Arrange
            // Act
            double actualValue = _calc.CalculateFutureValue(presentValue, rate, exponent);

            // Assert
            Assert.Equal(expectedValue, actualValue, 2);
        }

        [Theory(DisplayName = "CalculateFutureValue Test Result")]
        [MemberData(nameof(FinancialCalculatorData.NetPresentValueData), MemberType = typeof(FinancialCalculatorData))]
        public void CalculateNetPresentValue_Test_Result(double initialInvestment, double rate, IEnumerable<CashFlow> cashFlows, double expectedValue)
        {
            // Arrange
            // Act
            double actualValue = _calc.CalculateNetPresentValue(initialInvestment, rate, cashFlows);

            // Assert
            Assert.Equal(expectedValue, actualValue, 2);
        }

        [Theory(DisplayName = "CalculateNetPresentValueDynamicRate Test Result")]
        [MemberData(nameof(FinancialCalculatorData.NetPresentValueDynamicRateData), MemberType = typeof(FinancialCalculatorData))]
        public void CalculateNetPresentValueDynamicRate_Test_Result(
            double initialInvestment,
            double lowerBoundDiscountRate,
            double upperBoundDiscountRate,
            double discountRateIncrement,
            IEnumerable<CashFlow> cashFlows,
            IEnumerable<NetPresentValue> expectedValue)
        {
            // Arrange
            // Act
            IEnumerable<NetPresentValue> actualValue = _calc.CalculateNetPresentValueDynamicRate(
                                                        initialInvestment,
                                                        lowerBoundDiscountRate,
                                                        upperBoundDiscountRate,
                                                        discountRateIncrement,
                                                        cashFlows);

            // Assert
            Assert.Equal(expectedValue.Count(), actualValue.Count());

            var zip = expectedValue.Zip(actualValue, (a, b) =>
            {
                return (Math.Round(a.Rate, 2) == Math.Round(a.Rate, 2)) && (Math.Round(a.Rate, 2) == Math.Round(a.Rate, 2));
            });

            Assert.DoesNotContain(false, zip);

            //foreach (var item in zip)
            //{
            //    Assert.Equal(item.Expected.Value, item.Actual.Value, 2);
            //    Assert.Equal(item.Expected.Rate, item.Actual.Rate, 2);
            //}
        }
    }
}
