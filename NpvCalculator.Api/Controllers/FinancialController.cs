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

        [HttpGet("npv")]
        public IActionResult CalculateNetPresentValue()
        {
            double initialInvestment = 1000D;
            double rate = 12D;

            var cashFlows = new List<CashFlow>()
            {
                new CashFlow() { Amount = 100, Period = 1D },
                new CashFlow() { Amount = 200, Period = 2D },
                new CashFlow() { Amount = 300, Period = 3D },
                new CashFlow() { Amount = 400, Period = 4D },
                new CashFlow() { Amount = 900, Period = 5D },
            };

            var result = _calc.CalculateNetPresentValue(initialInvestment, rate, cashFlows);
            return Ok(result);
        }

        [HttpPost("npv2")]
        public IActionResult CalculateNetPresentValueDynamicRate(NetPresentValueRequest request)
        {
           var result = _calc.CalculateNetPresentValueDynamicRate(
               request.InitialInvestment,
               request.LowerBoundDiscountRate,
               request.UpperBoundDiscountRate,
               request.DiscountRateIncrement,
               request.CashFlows);

           return Ok(new NetPresentValueResponse()
           {
               NetPresentValues = result
           });
        }
    }
}
