using System;

namespace NpvCalculator.Data.Entities
{
    public class NetPresentValue
    {
        public int NetPresentValueId { get; set; }
        public string Name { get; set; }
        public double InitialInvestment { get; set; }
        public double LowerBoundDiscountRate { get; set; }
        public double UpperBoundDiscountRate { get; set; }
        public double DiscountRateIncrement { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
