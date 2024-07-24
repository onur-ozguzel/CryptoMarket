using CryptoMarket.Core.CrossCuttingConcerns.Exceptions;
using Microsoft.AspNetCore.Builder;

namespace CryptoMarket.Core
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
