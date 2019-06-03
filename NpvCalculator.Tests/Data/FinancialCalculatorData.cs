using NpvCalculator.Core.Classes;
using System;
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

        public static IEnumerable<object[]> FutureValueData => new List<object[]>
        {
            // presentValue, rate, exponent, expectedValue
            new object[] { 100D, 10D, 1D, 110D },
            new object[] { 745.77D, 15D, 5D, 1500.01D },
            new object[] { 3599.46D, 9.3D, 3D, 4700D },
            new object[] { 27841.90D, 15D, 5D, 56000.01D },
            new object[] { 10763.41D, 14.5, 4D, 18500D },
        };


        public static IEnumerable<object[]> NetPresentValueData => new List<object[]>
        {
            // initialInvestment, rate, cashFlows, expectedValue
            new object[]
            {
                1000D,
                12D,
                new List<CashFlow>()
                {
                    new CashFlow() { Amount = 100D, Period = 1D },
                    new CashFlow() { Amount = 200D, Period = 2D },
                    new CashFlow() { Amount = 300D, Period = 3D },
                    new CashFlow() { Amount = 400D, Period = 4D },
                    new CashFlow() { Amount = 900D, Period = 5D }
                },
                227.15D
            },
            new object[]
            {
                37893D,
                17D,
                new List<CashFlow>()
                {
                    new CashFlow() { Amount = 3589D, Period = 1D },
                    new CashFlow() { Amount = 6892D, Period = 2D },
                    new CashFlow() { Amount = 4826D, Period = 3D },
                    new CashFlow() { Amount = 7483D, Period = 4D },
                    new CashFlow() { Amount = 5376D, Period = 5D }
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
                new List<CashFlow>()
                {
                    new CashFlow() { Amount = 100D, Period = 1D },
                    new CashFlow() { Amount = 200D, Period = 2D },
                    new CashFlow() { Amount = 300D, Period = 3D },
                    new CashFlow() { Amount = 400D, Period = 4D },
                    new CashFlow() { Amount = 900D, Period = 5D }
                },
                new List<NetPresentValue>()
                {
                    new NetPresentValue() { Value = 826.9574D, Rate = 1D },
                    new NetPresentValue() { Value = 809.2922D, Rate = 1.25D },
                    new NetPresentValue() { Value = 791.8576D, Rate = 1.5D },
                    new NetPresentValue() { Value = 774.6499D, Rate = 1.75D },
                    new NetPresentValue() { Value = 757.6656D, Rate = 2D },
                    new NetPresentValue() { Value = 740.9010D, Rate = 2.25D },
                    new NetPresentValue() { Value = 724.3528D, Rate = 2.5D },
                    new NetPresentValue() { Value = 708.0175D, Rate = 2.75D },
                    new NetPresentValue() { Value = 691.8918D, Rate = 3D },
                    new NetPresentValue() { Value = 675.9724D, Rate = 3.25D },
                    new NetPresentValue() { Value = 660.2561D, Rate = 3.5D },
                    new NetPresentValue() { Value = 644.7396D, Rate = 3.75D },
                    new NetPresentValue() { Value = 629.4201D, Rate = 4D },
                    new NetPresentValue() { Value = 614.2943D, Rate = 4.25D },
                    new NetPresentValue() { Value = 599.3592D, Rate = 4.5D },
                    new NetPresentValue() { Value = 584.6120D, Rate = 4.75D },
                    new NetPresentValue() { Value = 570.0498D, Rate = 5D },
                    new NetPresentValue() { Value = 555.6697D, Rate = 5.25D },
                    new NetPresentValue() { Value = 541.4689D, Rate = 5.5D },
                    new NetPresentValue() { Value = 527.4448D, Rate = 5.75D },
                    new NetPresentValue() { Value = 513.5945D, Rate = 6D },
                    new NetPresentValue() { Value = 499.9156D, Rate = 6.25D },
                    new NetPresentValue() { Value = 486.4053D, Rate = 6.5D },
                    new NetPresentValue() { Value = 473.0612D, Rate = 6.75D },
                    new NetPresentValue() { Value = 459.8807D, Rate = 7D },
                    new NetPresentValue() { Value = 446.8614D, Rate = 7.25D },
                    new NetPresentValue() { Value = 434.0009D, Rate = 7.5D },
                    new NetPresentValue() { Value = 421.2968D, Rate = 7.75D },
                    new NetPresentValue() { Value = 408.7468D, Rate = 8D },
                    new NetPresentValue() { Value = 396.3486D, Rate = 8.25D },
                    new NetPresentValue() { Value = 384.1000D, Rate = 8.5D },
                    new NetPresentValue() { Value = 371.9987D, Rate = 8.75D },
                    new NetPresentValue() { Value = 360.0425D, Rate = 9D },
                    new NetPresentValue() { Value = 348.2294D, Rate = 9.25D },
                    new NetPresentValue() { Value = 336.5572D, Rate = 9.5D },
                    new NetPresentValue() { Value = 325.0238D, Rate = 9.75D },
                    new NetPresentValue() { Value = 313.6274D, Rate = 10D },
                    new NetPresentValue() { Value = 302.3657D, Rate = 10.25D },
                    new NetPresentValue() { Value = 291.2370D, Rate = 10.5D },
                    new NetPresentValue() { Value = 280.2393D, Rate = 10.75D },
                    new NetPresentValue() { Value = 269.3706D, Rate = 11D },
                    new NetPresentValue() { Value = 258.6291D, Rate = 11.25D },
                    new NetPresentValue() { Value = 248.0131D, Rate = 11.5D },
                    new NetPresentValue() { Value = 237.5206D, Rate = 11.75D },
                    new NetPresentValue() { Value = 227.1500D, Rate = 12D },
                    new NetPresentValue() { Value = 216.8994D, Rate = 12.25D },
                    new NetPresentValue() { Value = 206.7673D, Rate = 12.5D },
                    new NetPresentValue() { Value = 196.7518D, Rate = 12.75D },
                    new NetPresentValue() { Value = 186.8514D, Rate = 13D },
                    new NetPresentValue() { Value = 177.0644D, Rate = 13.25D },
                    new NetPresentValue() { Value = 167.3893D, Rate = 13.5D },
                    new NetPresentValue() { Value = 157.8244D, Rate = 13.75D },
                    new NetPresentValue() { Value = 148.3682D, Rate = 14D },
                    new NetPresentValue() { Value = 139.0192D, Rate = 14.25D },
                    new NetPresentValue() { Value = 129.7759D, Rate = 14.5D },
                    new NetPresentValue() { Value = 120.6368D, Rate = 14.75D },
                    new NetPresentValue() { Value = 111.6005D, Rate = 15D }
                }
            },
            new object[]
            {
                27000D,
                1D,
                5D,
                1D,
                new List<CashFlow>()
                {
                    new CashFlow() { Amount = 8268D, Period = 1D },
                    new CashFlow() { Amount = 5671D, Period = 2D },
                    new CashFlow() { Amount = 4759D, Period = 3D },
                    new CashFlow() { Amount = 7890D, Period = 4D },
                    new CashFlow() { Amount = 9675D, Period = 5D }
                },
                new List<NetPresentValue>()
                {
                    new NetPresentValue() { Value = 8152.0015D, Rate = 1D },
                    new NetPresentValue() { Value = 7093.2685D, Rate = 2D },
                    new NetPresentValue() { Value = 6083.7078D, Rate = 3D },
                    new NetPresentValue() { Value = 5120.4418D, Rate = 4D },
                    new NetPresentValue() { Value = 4200.7912D, Rate = 5D }
                }
            }
        };
    }
}
