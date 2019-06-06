using System.Collections.Generic;

namespace NpvCalculator.Core.Classes
{
    public class NetPresentValueRequest
    {
        public int NetPresentValueId { get; set; }
        public string Name { get; set; }
        public double InitialInvestment { get; set; }
        public double LowerBoundDiscountRate { get; set; }
        public double UpperBoundDiscountRate { get; set; }
        public double DiscountRateIncrement { get; set; }
        public IEnumerable<PeriodAmount> CashFlows { get; set; }
        public bool SaveToDatabase { get; set; }
    }
}
