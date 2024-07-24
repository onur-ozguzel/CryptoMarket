using CryptoMarket.Business.Constants;
using CryptoMarket.Business.Models;
using CryptoMarket.Business.Services;
using CryptoMarket.Business.Validators;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Timeout;
using System.Net;

namespace CryptoMarket.Business
{
    public static class BusinessServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddHttpClient<ICoinMarketCapService, CoinMarketCapService>()
                .AddStandardResilienceHandler(options =>
                {
                    // sample usage of customizing ResilienceHandler
                    // Customize retry
                    options.Retry.ShouldHandle = new PredicateBuilder<HttpResponseMessage>()
                        .Handle<TimeoutRejectedException>()
                        .Handle<HttpRequestException>()
                        .HandleResult(response => response.StatusCode == HttpStatusCode.InternalServerError)
                        .HandleResult(response => response.StatusCode == HttpStatusCode.NotFound);
                    options.Retry.MaxRetryAttempts = 5;

                    // Customize attempt timeout
                    options.AttemptTimeout.Timeout = TimeSpan.FromSeconds(2);

                    // Customize circuit breaker
                    options.CircuitBreaker.ShouldHandle = new PredicateBuilder<HttpResponseMessage>()
                        .Handle<TimeoutRejectedException>()
                        .Handle<HttpRequestException>()
                        .HandleResult(response => response.StatusCode == HttpStatusCode.InternalServerError)
                        .HandleResult(response => response.StatusCode == HttpStatusCode.NotFound);
                    options.CircuitBreaker.FailureRatio = 0.2; // Break on 20% failures
                    options.CircuitBreaker.MinimumThroughput = 3; // Minimum 3 requests before acting
                    options.CircuitBreaker.BreakDuration = TimeSpan.FromSeconds(10); // Break for 10 seconds
                });

            RegisterCoinMarketCapConfig(services, configuration);

            return services;
        }

        private static void RegisterCoinMarketCapConfig(this IServiceCollection services, IConfigurationRoot configuration)
        {
            var coinMarketCapConfig = configuration.GetSection(CoinMarketCapConstants.CoinMarketCapConfigName).Get<CoinMarketCapConfig>();
            if (coinMarketCapConfig == null)
            {
                throw new InvalidOperationException(ExceptionMessages.CoinMarketCapConfigurationIsMissingOrInvalid);
            }

            services
                .AddSingleton<IValidator<CoinMarketCapConfig>, CoinMarketCapConfigValidator>()
                .AddSingleton(coinMarketCapConfig);
        }
    }
}
