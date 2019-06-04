using Microsoft.AspNetCore.Mvc;
using Moq;
using NpvCalculator.Api.Controllers;
using NpvCalculator.Core;
using NpvCalculator.Core.Classes;
using System.Collections.Generic;
using Xunit;

namespace NpvCalculator.Tests
{
    public class FinancialController_Test
    {
        public FinancialController_Test()
        {

        }

        [Fact(DisplayName = "/pv/multi", Skip = "Not yet done")]
        public void ValidateRate_Throws_ArgumentException()
        {
            // Arrange
            var request = new PresentValueRequest()
            {
                FutureValue = 1100D,
                DiscountRate = 10D,
                Periods = 10
            };

            var mockService = new Mock<IFinancialCalculator>();
            mockService.Setup(s => s.CalculatePresentValueMulti(request.FutureValue, request.DiscountRate, request.Periods)).Returns(It.IsAny<IEnumerable<PeriodAmount>>());

            var controller = new FinancialController(mockService.Object);

            // Act
            var result = controller.CalculatePresentValue(request);

            // Assert
            var iAsyncResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            var okResult = iAsyncResult as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        private IEnumerable<PeriodAmount> Test(IEnumerable<PeriodAmount> a)
        {
            yield return new PeriodAmount() { Amount = 100D, Period = 1 };
        }
    }
}
