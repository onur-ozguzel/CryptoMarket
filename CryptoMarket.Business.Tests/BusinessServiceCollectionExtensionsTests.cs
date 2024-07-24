using CryptoMarket.Business.Constants;
using CryptoMarket.Business.Models;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoMarket.Business.Tests
{
    public class BusinessServiceCollectionExtensionsTests
    {
        [Fact]
        public void RegisterCoinMarketCapConfig_ExecuteWhenValid_CoinMarketCapConfigIsRegistered()
        {
            // Arrange
            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(
                    new Dictionary<string, string>
                    {
                        { "CoinMarketCap:BaseAddress", "" },
                        { "CoinMarketCap:ApiKey", "" },
                        { "CoinMarketCap:Currencies", "" }
                    })
                .Build();

            // Act
            services.AddBusinessServices(configuration);
            var serviceProvider = services.BuildServiceProvider();

            // Assert
            serviceProvider.GetService<CoinMarketCapConfig>().Should().NotBeNull();
            serviceProvider.GetService<CoinMarketCapConfig>().Should().BeOfType<CoinMarketCapConfig>();
        }

        [Fact]
        public void RegisterCoinMarketCapConfig_ExecuteWhenNull_ShouldRaiseInvalidOperationException()
        {
            // Arrange
            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>())
                .Build();

            // Act
            Action act = () => services.AddBusinessServices(configuration);

            // Assert
            act.Should().ThrowExactly<InvalidOperationException>().WithMessage(ExceptionMessages.CoinMarketCapConfigurationIsMissingOrInvalid);
        }
    }
}
