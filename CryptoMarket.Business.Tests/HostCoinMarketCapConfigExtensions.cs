using CryptoMarket.Business;
using CryptoMarket.Business.Models;
using CryptoMarket.Core.CrossCuttingConcerns.Serilog;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;

namespace CryptoMarket.WebAPI.Tests
{
    public class HostCoinMarketCapConfigExtensions
    {
        private readonly ServiceCollection _services;
        private readonly Mock<ILoggerServiceBase> _loggerServiceBaseMock;
        private readonly Mock<IHost> _appMock;

        public HostCoinMarketCapConfigExtensions()
        {
            _services = new ServiceCollection();
            _loggerServiceBaseMock = new Mock<ILoggerServiceBase>();
            _appMock = new Mock<IHost>();
        }

        [Theory]
        [MemberData(nameof(CoinMarketCapInvalidConfigData))]
        public void CoinMarketCapConfigValidator_WhenConfigIsInvalid_ShouldHaveValidationErrors(CoinMarketCapConfig coinMarketCapConfig, int expectedCount)
        {
            // Arrange
            var inMemorySettings = GenerateInMemoryCollation(coinMarketCapConfig);            
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _loggerServiceBaseMock.Setup(s => s.Error(It.IsAny<string>()));

            _services.AddBusinessServices(configuration);
            _services.AddSingleton(_loggerServiceBaseMock.Object);
            var serviceProvider = _services.BuildServiceProvider();

            _appMock.Setup(app => app.Services).Returns(serviceProvider);

            var app = _appMock.Object;

            // Act
            Action act = () => { app.ValidateCoinMarketCapConfig(); };

            // Assert
            act.Should().ThrowExactly<InvalidOperationException>();
            _loggerServiceBaseMock.Verify(s => s.Error(It.IsAny<string>()), Times.Exactly(expectedCount + 1));
        }

        [Fact]
        public void CoinMarketCapConfigValidator_WhenConfigIsValid_NoExceptionThrown()
        {
            // Arrange
            var coinMarketCapConfig = new CoinMarketCapConfig
            {
                BaseAddress = "https://dummy.com",
                ApiKey = "dummyApiKey",
                Currencies = ["DummyCurrency"]
            };

            var inMemorySettings = GenerateInMemoryCollation(coinMarketCapConfig);
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _loggerServiceBaseMock.Setup(s => s.Error(It.IsAny<string>()));

            _services.AddBusinessServices(configuration);
            _services.AddSingleton(_loggerServiceBaseMock.Object);
            var serviceProvider = _services.BuildServiceProvider();

            _appMock.Setup(app => app.Services).Returns(serviceProvider);

            var app = _appMock.Object;

            // Act
            Action act = () => { app.ValidateCoinMarketCapConfig(); };

            // Assert
            act.Should().NotThrow();
            _loggerServiceBaseMock.Verify(s => s.Error(It.IsAny<string>()), Times.Never);
        }

        private Dictionary<string, string> GenerateInMemoryCollation(CoinMarketCapConfig coinMarketCapConfig)
        {
            var inMemorySettings = new Dictionary<string, string>
            {
                {"CoinMarketCap:BaseAddress", coinMarketCapConfig.BaseAddress},
                {"CoinMarketCap:ApiKey", coinMarketCapConfig.ApiKey},
            };

            if (coinMarketCapConfig.Currencies != null)
            {
                for (int i = 0; i < coinMarketCapConfig.Currencies.Length; i++)
                {
                    inMemorySettings[$"CoinMarketCap:Currencies:{i}"] = coinMarketCapConfig.Currencies[i];
                }
            }

            return inMemorySettings;
        }

        public static IEnumerable<object[]> CoinMarketCapInvalidConfigData()
        {
            return new List<object[]>
            {
                new object[] {
                    new CoinMarketCapConfig
                    {
                        BaseAddress = string.Empty,
                        ApiKey = null,
                        Currencies = Array.Empty<string>()
                    }, 3
                },
                new object[] {
                    new CoinMarketCapConfig
                    {
                        BaseAddress = "DummyData",
                        ApiKey = "",
                        Currencies = ["DummyCurrency", ""]
                    }, 2
                },
                new object[] {
                    new CoinMarketCapConfig
                    {
                        BaseAddress = "DummyData",
                        ApiKey = "",
                        Currencies = ["DummyCurrency"]
                    }, 1
                }
            };
        }
    }
}
