using System.Text.Json;

namespace CryptoMarket.Core.CrossCuttingConcerns.Exceptions.Extensions
{
    public static class ProblemDetailsExtensions
    {
        public static string AsJson<TProblemDetail>(this TProblemDetail details)
            where TProblemDetail : ProblemDetails => JsonSerializer.Serialize(details);
    }
}
