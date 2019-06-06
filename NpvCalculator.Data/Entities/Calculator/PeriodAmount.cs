namespace NpvCalculator.Data.Entities
{
    public class PeriodAmount
    {
        public int PeriodAmountId { get; set; }
        public int NetPresentValueId { get; set; }
        public double Amount { get; set; }
        public int Period { get; set; }

        public virtual NetPresentValue NetPresentValue { get; set; }
    }
}
