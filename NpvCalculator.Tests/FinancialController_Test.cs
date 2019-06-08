using Microsoft.AspNetCore.Mvc;
using Moq;
using NpvCalculator.Api.Controllers;
using NpvCalculator.Core.Classes;
using NpvCalculator.Core.Services;
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
        public void Method_Returns_StatusCode_200()
        {
            // Arrange
            var request = new PresentValueRequest()
            {
                FutureValue = 1100D,
                DiscountRate = 10D,
                Periods = 10
            };

            var mockCalc = new Mock<IFinancialCalculator>();
            mockCalc.Setup(s => s.CalculatePresentValueMulti(request.FutureValue, request.DiscountRate, request.Periods)).Returns(It.IsAny<IEnumerable<PeriodAmount>>());

            var mockService = new Mock<IFinancialService>();

            var controller = new FinancialController(mockCalc.Object, mockService.Object);

            // Act
            var result = controller.CalculatePresentValue(request);

            // Assert
            var iAsyncResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            var okResult = iAsyncResult as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
