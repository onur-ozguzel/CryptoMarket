using CryptoMarket.Core.CrossCuttingConcerns.Exceptions.Handlers;
using CryptoMarket.Core.CrossCuttingConcerns.Logging;
using CryptoMarket.Core.CrossCuttingConcerns.Serilog;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace CryptoMarket.Core.CrossCuttingConcerns.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HttpExceptionHandler _httpExceptionHandler;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public readonly ILoggerServiceBase _loggerServiceBase;

        public ExceptionMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor, ILoggerServiceBase loggerServiceBase)
        {
            _next = next;
            _httpExceptionHandler = new HttpExceptionHandler();
            _httpContextAccessor = httpContextAccessor;
            _loggerServiceBase = loggerServiceBase;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await LogException(context, ex);
                await HandleExceptionAsync(context.Response, ex);
            }
        }

        private Task LogException(HttpContext context, Exception ex)
        {
            List<LogParameter> logParameters = new() {
                new LogParameter{ Type = context.GetType().Name, Value = ex.ToString() }
            };

            LogDetailWithException logDetail = new()
            {
                MethodName = _next.Method.Name,
                Parameters = logParameters,
                User = _httpContextAccessor.HttpContext?.User.Identity?.Name ?? "?",
                ExceptionMessage = ex.Message
            };

            _loggerServiceBase.Error(JsonSerializer.Serialize(logDetail));

            return Task.CompletedTask;
        }

        private Task HandleExceptionAsync(HttpResponse response, Exception exception)
        {
            response.ContentType = "application/json";
            _httpExceptionHandler.Response = response;
            return _httpExceptionHandler.HandleExceptionAsync(exception);
        }
    }
}
