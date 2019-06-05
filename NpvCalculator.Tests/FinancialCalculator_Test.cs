using NpvCalculator.Core.Classes;
using NpvCalculator.Core.Services;
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

        [Theory(DisplayName = "CalculatePresentValueMulti Test Result")]
        [MemberData(nameof(FinancialCalculatorData.PresentValueMultiData), MemberType = typeof(FinancialCalculatorData))]
        public void CalculatePresentValueMulti_Test_Result(double futureValue, double rate, int periods, IEnumerable<PeriodAmount> expectedValue)
        {
            // Arrange
            // Act
            var actualValue = _calc.CalculatePresentValueMulti(futureValue, rate, periods);

            Assert.Equal(expectedValue.Count(), actualValue.Count());

            var zip = expectedValue.OrderBy(x => x.Period).Zip(actualValue.OrderBy(x => x.Period), (expected, actual) =>
                (expected.Period == actual.Period) && (Math.Round(expected.Amount, 2) == Math.Round(actual.Amount, 2)));

            Assert.DoesNotContain(false, zip);

            //var zip = expectedValue.OrderBy(x => x.Period).Zip(actualValue.OrderBy(x => x.Period), (expected, actual) => new { Expected = expected, Actual = actual });
            //foreach (var item in zip)
            //{
            //    Assert.Equal(item.Expected.Period, item.Actual.Period);
            //    Assert.Equal(item.Expected.Amount, item.Actual.Amount, 2);
            //}
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

        [Theory(DisplayName = "CalculateFutureValueMulti Test Result")]
        [MemberData(nameof(FinancialCalculatorData.FutureValueMultiData), MemberType = typeof(FinancialCalculatorData))]
        public void CalculateFutureValueMulti_Test_Result(double presentValue, double rate, int periods, IEnumerable<PeriodAmount> expectedValue)
        {
            // Arrange
            // Act
            var actualValue = _calc.CalculateFutureValueMulti(presentValue, rate, periods);

            Assert.Equal(expectedValue.Count(), actualValue.Count());

            var zip = expectedValue.OrderBy(x => x.Period).Zip(actualValue.OrderBy(x => x.Period), (expected, actual) =>
                (expected.Period == actual.Period) && (Math.Round(expected.Amount, 2) == Math.Round(actual.Amount, 2)));

            Assert.DoesNotContain(false, zip);

            //var zip = expectedValue.OrderBy(x => x.Period).Zip(actualValue.OrderBy(x => x.Period), (expected, actual) => new { Expected = expected, Actual = actual });
            //foreach (var item in zip)
            //{
            //    Assert.Equal(item.Expected.Period, item.Actual.Period);
            //    Assert.Equal(item.Expected.Amount, item.Actual.Amount, 2);
            //}
        }

        [Theory(DisplayName = "CalculateNetPresentValue Test Result")]
        [MemberData(nameof(FinancialCalculatorData.NetPresentValueData), MemberType = typeof(FinancialCalculatorData))]
        public void CalculateNetPresentValue_Test_Result(double initialInvestment, double rate, IEnumerable<PeriodAmount> cashFlows, double expectedValue)
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
            IEnumerable<PeriodAmount> cashFlows,
            IEnumerable<NetPresentValuePerRate> expectedValue)
        {
            // Arrange
            // Act
            IEnumerable<NetPresentValuePerRate> actualValue = _calc.CalculateNetPresentValueDynamicRate(
                                                        initialInvestment,
                                                        lowerBoundDiscountRate,
                                                        upperBoundDiscountRate,
                                                        discountRateIncrement,
                                                        cashFlows);

            // Assert
            Assert.Equal(expectedValue.Count(), actualValue.Count());

            var zip = expectedValue.OrderBy(x => x.Rate).Zip(actualValue.OrderBy(x => x.Rate), (expected, actual) =>
                (Math.Round(expected.Rate, 2) == Math.Round(actual.Rate, 2)) && (Math.Round(expected.Rate, 2) == Math.Round(actual.Rate, 2)));

            Assert.DoesNotContain(false, zip);

            //var zip = expectedValue.OrderBy(x => x.Rate).Zip(actualValue.OrderBy(x => x.Rate), (expected, actual) => new { Expected = expected, Actual = actual });
            //foreach (var item in zip)
            //{
            //    Assert.Equal(item.Expected.Value, item.Actual.Value, 2);
            //    Assert.Equal(item.Expected.Rate, item.Actual.Rate, 2);
            //}
        }
    }
}
