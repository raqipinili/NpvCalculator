namespace NpvCalculator.Core.Classes
{
    public class PresentValueRequest
    {
        public double FutureValue { get; set; }
        public double DiscountRate { get; set; }
        public int Periods { get; set; }
        public double CompountInterval { get; set; }
    }
}
