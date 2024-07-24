using CryptoMarket.Business.Models;
using CryptoMarket.Business.Models.Responses.GetCryptoCurrencyQuotes;
using CryptoMarket.Business.Services;
using CryptoMarket.Core.CrossCuttingConcerns.Exceptions.Types;
using FluentAssertions;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http.Json;

namespace CryptoMarket.Business.Tests.Services
{
    public class CoinMarketCapServiceTests
    {
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly CoinMarketCapService _service;
        private readonly HttpClient _httpClient;

        public CoinMarketCapServiceTests()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_httpMessageHandlerMock.Object);

            var config = new CoinMarketCapConfig
            {
                ApiKey = "test-api-key",
                BaseAddress = "https://www.dummyaddress.com",
                Currencies = ["USD"]

            };

            _service = new CoinMarketCapService(_httpClient, config);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task GetCryptoCurrencyQuotesAsync_WhenSymbolIsNullOrWhitespace_ShouldReturnValidationError(string symbol)
        {
            // Arrange
            var cancellationToken = CancellationToken.None;

            // Act
            Func<Task> act = () => _service.GetCryptoCurrencyQuotesAsync(cancellationToken, symbol);

            // Assert
            await act.Should().ThrowExactlyAsync<Exception>();
            _httpMessageHandlerMock.Protected().Verify(
                "SendAsync",
                Times.Never(),
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task GetCryptoCurrencyQuotesAsync_WhenServiceCallNotSuccessful_ReturnsFailure()
        {
            // Arrange
            var symbol = "BTC";
            var cancellationToken = CancellationToken.None;

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest
                });

            // Act
            Func<Task> act = () => _service.GetCryptoCurrencyQuotesAsync(cancellationToken, symbol);

            // Assert
            await act.Should().ThrowExactlyAsync<Exception>();
            _httpMessageHandlerMock.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task GetCryptoCurrencyQuotesAsync_WhenApiResultIsNull_ReturnsFailure()
        {
            // Arrange
            var symbol = "BTC";
            var cancellationToken = CancellationToken.None;

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = JsonContent.Create((CryptoCurrencyQuotesResponse)null)
                });

            // Act
            Func<Task> act = () => _service.GetCryptoCurrencyQuotesAsync(cancellationToken, symbol);

            // Assert
            await act.Should().ThrowExactlyAsync<BusinessException>();
            _httpMessageHandlerMock.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task GetCryptoCurrencyQuotesAsync_WhenApiResultHasNoData_ReturnsFailure()
        {
            // Arrange
            var symbol = "BTC";
            var cancellationToken = CancellationToken.None;

            var apiResponse = new CryptoCurrencyQuotesResponse
            {
                Data = null
            };

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = JsonContent.Create(apiResponse)
                });

            // Act
            Func<Task> act = () => _service.GetCryptoCurrencyQuotesAsync(cancellationToken, symbol);

            // Assert
            await act.Should().ThrowExactlyAsync<BusinessException>();
            _httpMessageHandlerMock.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task GetCryptoCurrencyQuotesAsync_WhenDataIsValid_ReturnsSuccess()
        {
            // Arrange
            var symbol = "BTC";
            var cancellationToken = CancellationToken.None;

            var apiResponse = new CryptoCurrencyQuotesResponse
            {
                Data = new Dictionary<string, List<CryptocurrencyData>>
                {
                    ["BTC"] = new List<CryptocurrencyData>
                    {
                        new CryptocurrencyData
                        {
                            Name = "Bitcoin",
                            Symbol = "BTC",
                            Slug = "bitcoin",
                            Quote = new Dictionary<string, Quote>{
                                { "USD", new Quote() { Price = 50000 } },
                                { "EUR", new Quote() { Price = 42000 } }
                            }
                        }
                    }
                }
            };

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = JsonContent.Create(apiResponse)
                });

            // Act
            var result = await _service.GetCryptoCurrencyQuotesAsync(cancellationToken, symbol);

            // Assert
            result.Value.Should().NotBeNull();
            result.Value.Should().HaveCount(1);

            var quote = result.Value.First();
            quote.Name.Should().Be("Bitcoin");
            quote.Symbol.Should().Be("BTC");
            quote.Slug.Should().Be("bitcoin");
            quote.Currencies.Should().Contain(new KeyValuePair<string, decimal?>("USD", 50000));
            quote.Currencies.Should().Contain(new KeyValuePair<string, decimal?>("EUR", 42000));
            quote.Currencies.Should().NotContain(new KeyValuePair<string, decimal?>("AUD", null));
            quote.Currencies.Should().NotContain(new KeyValuePair<string, decimal?>("BRL", null));
            quote.Currencies.Should().NotContain(new KeyValuePair<string, decimal?>("GBP", null));
            _httpMessageHandlerMock.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>());
        }
    }
}
