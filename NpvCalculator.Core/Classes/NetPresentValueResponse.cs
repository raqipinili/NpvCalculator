using System.Collections.Generic;

namespace NpvCalculator.Core.Classes
{
    public class NetPresentValueResponse
    {
        public string Name { get; set; }
        public double InitialInvestment { get; set; }
        public double LowerBoundDiscountRate { get; set; }
        public double UpperBoundDiscountRate { get; set; }
        public double DiscountRateIncrement { get; set; }
        public IEnumerable<PeriodAmount> CashFlows { get; set; }
        public IEnumerable<NetPresentValuePerRate> Results { get; set; }
    }
}
