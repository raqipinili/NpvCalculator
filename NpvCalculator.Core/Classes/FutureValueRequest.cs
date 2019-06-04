namespace NpvCalculator.Core.Classes
{
    public class FutureValueRequest
    {
        public double PresentValue { get; set; }
        public double InterestRate { get; set; }
        public int Periods { get; set; }
        public double CompountInterval { get; set; }
    }
}
