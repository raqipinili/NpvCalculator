using System;

namespace NpvCalculator.Core.Helpers
{
    public class FinancialValidator
    {
        public static void ValidateRate(double rate)
        {
            if (rate <= 0D || rate > 100D)
                throw new ArgumentException("rate must be 0 to 100");
        }

        public static void ValidateRateIncrement(double upperBoundRate, double increment)
        {
            if (increment > upperBoundRate)
                throw new ArgumentException($"increment rate must be less than or equal to '{upperBoundRate}'");
        }

        public static void ValidateExponent(double exponent)
        {
            if (exponent < 0)
                throw new ArgumentException("exponent must be greater than or equal 0");
        }
    }
}
