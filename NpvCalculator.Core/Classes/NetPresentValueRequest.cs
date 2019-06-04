using System;
using System.Collections.Generic;
using System.Text;

namespace NpvCalculator.Core.Classes
{
    public class NetPresentValueRequest
    {
        public double InitialInvestment { get; set; }
        public double LowerBoundDiscountRate { get; set; }
        public double UpperBoundDiscountRate { get; set; }
        public double DiscountRateIncrement { get; set; }
        public IEnumerable<PeriodAmount> CashFlows { get; set; }
    }
}
