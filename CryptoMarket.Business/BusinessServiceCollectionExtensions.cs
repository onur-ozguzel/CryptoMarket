using CryptoMarket.Business.Constants;
using CryptoMarket.Business.Models;
using CryptoMarket.Business.Services;
using CryptoMarket.Business.Validators;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoMarket.Business
{
    public static class BusinessServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddScoped<ICoinMarketCapService, CoinMarketCapService>();
            services.AddHttpClient<CoinMarketCapService>()
                .AddStandardResilienceHandler();
            RegisterCoinMarketCapConfig(services, configuration);

            return services;
        }

        private static void RegisterCoinMarketCapConfig(this IServiceCollection services, IConfigurationRoot configuration)
        {
            var coinMarketCapConfig = configuration.GetSection(CoinMarketCapConstants.CoinMarketCapConfigName).Get<CoinMarketCapConfig>();
            if (coinMarketCapConfig == null)
            {
                throw new InvalidOperationException(ErrorMessages.CoinMarketCapConfigurationIsMissingOrInvalid);
            }

            services
                .AddSingleton<IValidator<CoinMarketCapConfig>, CoinMarketCapConfigValidator>()
                .AddSingleton(coinMarketCapConfig);
        }
    }
}
