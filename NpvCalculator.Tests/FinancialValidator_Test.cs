using NpvCalculator.Core.Helpers;
using System;
using Xunit;

namespace NpvCalculator.Tests
{
    public class FinancialValidator_Test
    {
        [Fact(DisplayName = "ValidateRate Throws ArgumentException")]
        public void ValidateRate_Throws_ArgumentException()
        {
            // Arrange
            double invalidRate = -1D;

            // Act
            Action act = () => FinancialValidator.ValidateRate(invalidRate);

            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        [Fact(DisplayName = "ValidateExponent Throws ArgumentException")]
        public void ValidateExponent_Throws_ArgumentException()
        {
            // Arrange
            double invalidExponent = -2D;

            // Act
            Action act = () => FinancialValidator.ValidateExponent(invalidExponent);

            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        [Fact(DisplayName = "ValidateRateIncrement Throws ArgumentException")]
        public void ValidateRateIncrement_Throws_ArgumentException()
        {
            // Arrange
            double upperBoundRate = 15D;
            double invalidRateIncrement = 16D;

            // Act
            Action act = () => FinancialValidator.ValidateRateIncrement(upperBoundRate, invalidRateIncrement);

            // Assert
            Assert.Throws<ArgumentException>(act);
        }
    }
}
