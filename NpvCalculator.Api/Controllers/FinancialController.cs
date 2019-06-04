using Microsoft.AspNetCore.Mvc;
using NpvCalculator.Core;
using NpvCalculator.Core.Classes;
using System.Collections.Generic;

namespace NpvCalculator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialController : ControllerBase
    {
        private readonly IFinancialCalculator _calc;

        public FinancialController(IFinancialCalculator calc)
        {
            _calc = calc;
        }

        [HttpPost("pv/multi")]
        public IActionResult CalculatePresentValue(PresentValueRequest request)
        {
            var result = _calc.CalculatePresentValueMulti(
                request.FutureValue,
                request.DiscountRate,
                request.Periods);

            return Ok(result);
        }

        [HttpPost("fv/multi")]
        public IActionResult CalculateFutureValue(FutureValueRequest request)
        {
            var result = _calc.CalculateFutureValueMulti(
                request.PresentValue,
                request.InterestRate,
                request.Periods);

            return Ok(result);
        }

        [HttpGet("npv")]
        public IActionResult CalculateNetPresentValue()
        {
            double initialInvestment = 1000D;
            double rate = 12D;

            var cashFlows = new List<PeriodAmount>()
            {
                new PeriodAmount() { Amount = 100D, Period = 1 },
                new PeriodAmount() { Amount = 200D, Period = 2 },
                new PeriodAmount() { Amount = 300D, Period = 3 },
                new PeriodAmount() { Amount = 400D, Period = 4 },
                new PeriodAmount() { Amount = 900D, Period = 5 },
            };

            // 227.15D
            var result = _calc.CalculateNetPresentValue(initialInvestment, rate, cashFlows);
            return Ok(result);
        }

        [HttpPost("npv/dynamicrate")]
        public IActionResult CalculateNetPresentValueDynamicRate(NetPresentValueRequest request)
        {
            var result = _calc.CalculateNetPresentValueDynamicRate(
               request.InitialInvestment,
               request.LowerBoundDiscountRate,
               request.UpperBoundDiscountRate,
               request.DiscountRateIncrement,
               request.CashFlows);

           return Ok(result);
        }
    }
}
