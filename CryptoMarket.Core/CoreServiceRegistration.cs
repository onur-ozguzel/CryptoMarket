using CryptoMarket.Core.CrossCuttingConcerns.Serilog;
using CryptoMarket.Core.CrossCuttingConcerns.Serilog.Loggers;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoMarket.Core
{
    public static class CoreServiceRegistration
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerServiceBase, FileLogger>();

            return services;
        }
    }
}
