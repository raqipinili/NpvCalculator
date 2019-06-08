using NpvCalculator.Core.Classes;
using System.Collections.Generic;

namespace NpvCalculator.Tests.Data
{
    public class FinancialCalculatorData
    {
        public static IEnumerable<object[]> PresentValueData => new List<object[]>
        {
            // futureValue, rate, exponent, expectedValue
            new object[] { 110D, 10D, 1D, 100D },
            new object[] { 1500D, 15D, 5D, 745.77D },
            new object[] { 4700, 9.3D, 3D, 3599.46D },
            new object[] { 56000D, 15D, 5D, 27841.90D },
            new object[] { 18500D, 14.5, 4D, 10763.41D },
        };

        public static IEnumerable<object[]> PresentValueMultiData => new List<object[]>
        {
            // futureValue, rate, periods, expectedValue
            new object[]
            {
                1100D,
                10D,
                10,
                new List<PeriodAmount>()
                {
                    new PeriodAmount() { Amount = 424.10D, Period = 0 },
                    new PeriodAmount() { Amount = 466.51D, Period = 1 },
                    new PeriodAmount() { Amount = 513.16D, Period = 2 },
                    new PeriodAmount() { Amount = 564.47D, Period = 3 },
                    new PeriodAmount() { Amount = 620.92D, Period = 4 },
                    new PeriodAmount() { Amount = 683.01D, Period = 5 },
                    new PeriodAmount() { Amount = 751.31D, Period = 6 },
                    new PeriodAmount() { Amount = 826.45D, Period = 7 },
                    new PeriodAmount() { Amount = 909.09D, Period = 8 },
                    new PeriodAmount() { Amount = 1000D, Period = 9 },
                    new PeriodAmount() { Amount = 1100D, Period = 10 }
                }
            },
        };

        public static IEnumerable<object[]> FutureValueData => new List<object[]>
        {
            // presentValue, rate, exponent, expectedValue
            new object[] { 100D, 10D, 1D, 110D },
            new object[] { 745.77D, 15D, 5D, 1500.01D },
            new object[] { 3599.46D, 9.3D, 3D, 4700D },
            new object[] { 27841.90D, 15D, 5D, 56000.01D },
            new object[] { 10763.41D, 14.5, 4D, 18500D },
        };

        public static IEnumerable<object[]> FutureValueMultiData => new List<object[]>
        {
            // futureValue, rate, periods, expectedValue
            new object[]
            {
                1000D,
                10D,
                10,
                new List<PeriodAmount>()
                {
                    new PeriodAmount() { Amount = 1100D, Period = 1 },
                    new PeriodAmount() { Amount = 1210D, Period = 2 },
                    new PeriodAmount() { Amount = 1331D, Period = 3 },
                    new PeriodAmount() { Amount = 1464.10D, Period = 4 },
                    new PeriodAmount() { Amount = 1610.51D, Period = 5 },
                    new PeriodAmount() { Amount = 1771.56D, Period = 6 },
                    new PeriodAmount() { Amount = 1948.72D, Period = 7 },
                    new PeriodAmount() { Amount = 2143.59D, Period = 8 },
                    new PeriodAmount() { Amount = 2357.95D, Period = 9 },
                    new PeriodAmount() { Amount = 2593.74D, Period = 10 }
                }
            },
        };

        public static IEnumerable<object[]> NetPresentValueData => new List<object[]>
        {
            // initialInvestment, rate, cashFlows, expectedValue
            new object[]
            {
                1000D,
                12D,
                new List<PeriodAmount>()
                {
                    new PeriodAmount() { Amount = 100D, Period = 1 },
                    new PeriodAmount() { Amount = 200D, Period = 2 },
                    new PeriodAmount() { Amount = 300D, Period = 3 },
                    new PeriodAmount() { Amount = 400D, Period = 4 },
                    new PeriodAmount() { Amount = 900D, Period = 5 }
                },
                227.15D
            },
            new object[]
            {
                37893D,
                17D,
                new List<PeriodAmount>()
                {
                    new PeriodAmount() { Amount = 3589D, Period = 1 },
                    new PeriodAmount() { Amount = 6892D, Period = 2 },
                    new PeriodAmount() { Amount = 4826D, Period = 3 },
                    new PeriodAmount() { Amount = 7483D, Period = 4 },
                    new PeriodAmount() { Amount = 5376D, Period = 5 }
                },
                -20332.21D
            }
        };


        public static IEnumerable<object[]> NetPresentValueDynamicRateData => new List<object[]>
        {
            // initialInvestment, lowerBound, upperBound, rateIncrement, cashFlows, expectedValue
            new object[]
            {
                1000D,
                1D,
                15D,
                .25D,
                new List<PeriodAmount>()
                {
                    new PeriodAmount() { Amount = 100D, Period = 1 },
                    new PeriodAmount() { Amount = 200D, Period = 2 },
                    new PeriodAmount() { Amount = 300D, Period = 3 },
                    new PeriodAmount() { Amount = 400D, Period = 4 },
                    new PeriodAmount() { Amount = 900D, Period = 5 }
                },
                new List<NetPresentValuePerRate>()
                {
                    new NetPresentValuePerRate() { Amount = 826.9574D, Rate = 1D },
                    new NetPresentValuePerRate() { Amount = 809.2922D, Rate = 1.25D },
                    new NetPresentValuePerRate() { Amount = 791.8576D, Rate = 1.5D },
                    new NetPresentValuePerRate() { Amount = 774.6499D, Rate = 1.75D },
                    new NetPresentValuePerRate() { Amount = 757.6656D, Rate = 2D },
                    new NetPresentValuePerRate() { Amount = 740.9010D, Rate = 2.25D },
                    new NetPresentValuePerRate() { Amount = 724.3528D, Rate = 2.5D },
                    new NetPresentValuePerRate() { Amount = 708.0175D, Rate = 2.75D },
                    new NetPresentValuePerRate() { Amount = 691.8918D, Rate = 3D },
                    new NetPresentValuePerRate() { Amount = 675.9724D, Rate = 3.25D },
                    new NetPresentValuePerRate() { Amount = 660.2561D, Rate = 3.5D },
                    new NetPresentValuePerRate() { Amount = 644.7396D, Rate = 3.75D },
                    new NetPresentValuePerRate() { Amount = 629.4201D, Rate = 4D },
                    new NetPresentValuePerRate() { Amount = 614.2943D, Rate = 4.25D },
                    new NetPresentValuePerRate() { Amount = 599.3592D, Rate = 4.5D },
                    new NetPresentValuePerRate() { Amount = 584.6120D, Rate = 4.75D },
                    new NetPresentValuePerRate() { Amount = 570.0498D, Rate = 5D },
                    new NetPresentValuePerRate() { Amount = 555.6697D, Rate = 5.25D },
                    new NetPresentValuePerRate() { Amount = 541.4689D, Rate = 5.5D },
                    new NetPresentValuePerRate() { Amount = 527.4448D, Rate = 5.75D },
                    new NetPresentValuePerRate() { Amount = 513.5945D, Rate = 6D },
                    new NetPresentValuePerRate() { Amount = 499.9156D, Rate = 6.25D },
                    new NetPresentValuePerRate() { Amount = 486.4053D, Rate = 6.5D },
                    new NetPresentValuePerRate() { Amount = 473.0612D, Rate = 6.75D },
                    new NetPresentValuePerRate() { Amount = 459.8807D, Rate = 7D },
                    new NetPresentValuePerRate() { Amount = 446.8614D, Rate = 7.25D },
                    new NetPresentValuePerRate() { Amount = 434.0009D, Rate = 7.5D },
                    new NetPresentValuePerRate() { Amount = 421.2968D, Rate = 7.75D },
                    new NetPresentValuePerRate() { Amount = 408.7468D, Rate = 8D },
                    new NetPresentValuePerRate() { Amount = 396.3486D, Rate = 8.25D },
                    new NetPresentValuePerRate() { Amount = 384.1000D, Rate = 8.5D },
                    new NetPresentValuePerRate() { Amount = 371.9987D, Rate = 8.75D },
                    new NetPresentValuePerRate() { Amount = 360.0425D, Rate = 9D },
                    new NetPresentValuePerRate() { Amount = 348.2294D, Rate = 9.25D },
                    new NetPresentValuePerRate() { Amount = 336.5572D, Rate = 9.5D },
                    new NetPresentValuePerRate() { Amount = 325.0238D, Rate = 9.75D },
                    new NetPresentValuePerRate() { Amount = 313.6274D, Rate = 10D },
                    new NetPresentValuePerRate() { Amount = 302.3657D, Rate = 10.25D },
                    new NetPresentValuePerRate() { Amount = 291.2370D, Rate = 10.5D },
                    new NetPresentValuePerRate() { Amount = 280.2393D, Rate = 10.75D },
                    new NetPresentValuePerRate() { Amount = 269.3706D, Rate = 11D },
                    new NetPresentValuePerRate() { Amount = 258.6291D, Rate = 11.25D },
                    new NetPresentValuePerRate() { Amount = 248.0131D, Rate = 11.5D },
                    new NetPresentValuePerRate() { Amount = 237.5206D, Rate = 11.75D },
                    new NetPresentValuePerRate() { Amount = 227.1500D, Rate = 12D },
                    new NetPresentValuePerRate() { Amount = 216.8994D, Rate = 12.25D },
                    new NetPresentValuePerRate() { Amount = 206.7673D, Rate = 12.5D },
                    new NetPresentValuePerRate() { Amount = 196.7518D, Rate = 12.75D },
                    new NetPresentValuePerRate() { Amount = 186.8514D, Rate = 13D },
                    new NetPresentValuePerRate() { Amount = 177.0644D, Rate = 13.25D },
                    new NetPresentValuePerRate() { Amount = 167.3893D, Rate = 13.5D },
                    new NetPresentValuePerRate() { Amount = 157.8244D, Rate = 13.75D },
                    new NetPresentValuePerRate() { Amount = 148.3682D, Rate = 14D },
                    new NetPresentValuePerRate() { Amount = 139.0192D, Rate = 14.25D },
                    new NetPresentValuePerRate() { Amount = 129.7759D, Rate = 14.5D },
                    new NetPresentValuePerRate() { Amount = 120.6368D, Rate = 14.75D },
                    new NetPresentValuePerRate() { Amount = 111.6005D, Rate = 15D }
                }
            },
            new object[]
            {
                27000D,
                1D,
                5D,
                1D,
                new List<PeriodAmount>()
                {
                    new PeriodAmount() { Amount = 8268D, Period = 1 },
                    new PeriodAmount() { Amount = 5671D, Period = 2 },
                    new PeriodAmount() { Amount = 4759D, Period = 3 },
                    new PeriodAmount() { Amount = 7890D, Period = 4 },
                    new PeriodAmount() { Amount = 9675D, Period = 5 }
                },
                new List<NetPresentValuePerRate>()
                {
                    new NetPresentValuePerRate() { Amount = 8152.0015D, Rate = 1D },
                    new NetPresentValuePerRate() { Amount = 7093.2685D, Rate = 2D },
                    new NetPresentValuePerRate() { Amount = 6083.7078D, Rate = 3D },
                    new NetPresentValuePerRate() { Amount = 5120.4418D, Rate = 4D },
                    new NetPresentValuePerRate() { Amount = 4200.7912D, Rate = 5D }
                }
            }
        };
    }
}
