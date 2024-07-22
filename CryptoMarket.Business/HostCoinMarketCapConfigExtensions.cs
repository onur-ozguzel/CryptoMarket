using CryptoMarket.Business.Constants;
using CryptoMarket.Business.Models;
using CryptoMarket.Core.CrossCuttingConcerns.Serilog;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CryptoMarket.Business
{
    public static class HostCoinMarketCapConfigExtensions
    {
        public static void ValidateCoinMarketCapConfig(this IHost app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var logger = app.Services.GetRequiredService<ILoggerServiceBase>();

                var validator = scope.ServiceProvider.GetRequiredService<IValidator<CoinMarketCapConfig>>();
                var coinMarketCapConfig = scope.ServiceProvider.GetRequiredService<CoinMarketCapConfig>();
                var validationResult = validator.Validate(coinMarketCapConfig);

                if (!validationResult.IsValid)
                {
                    logger.Error(CoinMarketCapConstants.CoinMarketCapConfigurationIsInvalidSeeLogsForDetails);

                    foreach (var error in validationResult.Errors)
                    {
                        logger.Error($"{CoinMarketCapConstants.ValidationError} {error.ErrorMessage}");
                    }

                    throw new InvalidOperationException(CoinMarketCapConstants.CoinMarketCapConfigurationIsInvalidSeeLogsForDetails);
                }
            };
        }
    }
}
