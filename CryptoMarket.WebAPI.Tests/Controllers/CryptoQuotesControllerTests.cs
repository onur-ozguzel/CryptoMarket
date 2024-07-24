using CryptoMarket.Business.Models;
using CryptoMarket.Business.Services;
using CryptoMarket.WebAPI.Controllers;
using FluentAssertions;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CryptoMarket.WebAPI.Tests.Controllers
{
    public class CryptoQuotesControllerTests
    {
        [Fact]
        public async Task GetCryptoCurrencyQuotesAsync_WithValidSymbol_ReturnsOkObjectResult()
        {
            // Arrange
            string symbol = "BTC";
            var expected = new List<CryptoCurrencyQuotesDto>();
            var mockService = new Mock<ICoinMarketCapService>();
            var mockErrorService = new Mock<IErrorHandlingService>();
            mockService.Setup(s => s.GetCryptoCurrencyQuotesAsync(It.IsAny<CancellationToken>(), symbol))
                       .ReturnsAsync(new List<CryptoCurrencyQuotesDto>());
            mockErrorService.Setup(s => s.HandleResult(It.IsAny<IResult<object>>()))
                       .Returns(new OkObjectResult(expected));

            var controller = new CryptoQuotesController(mockErrorService.Object, mockService.Object);

            // Act
            var result = await controller.GetCryptoCurrencyQuotesAsync(new CancellationToken(), symbol);

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                  .Which.Value.Should().BeEquivalentTo(expected);
            mockService.Verify(s => s.GetCryptoCurrencyQuotesAsync(It.IsAny<CancellationToken>(), symbol), Times.Once);
        }
    }
}