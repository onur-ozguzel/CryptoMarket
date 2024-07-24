using CryptoMarket.Business.CrossCuttingConcerns.Errors;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoMarket.Business.Services
{
    public class ErrorHandlingService : IErrorHandlingService
    {
        private readonly Dictionary<Type, int> ErrorStatusCodes = new()
        {
            { typeof(FailedToFetchDataError), StatusCodes.Status500InternalServerError },
            { typeof(NoDataReturnedFromTheAPIError), StatusCodes.Status500InternalServerError },
            { typeof(ArgumentNullOrEmptyError), StatusCodes.Status400BadRequest }
        };

        public IActionResult HandleResult<T>(IResult<T> result)
        {
            if (result.IsSuccess)
            {
                return new OkObjectResult(result.Value);
            }

            var errors = result.Errors;
            if (errors.Any(error => ErrorStatusCodes.TryGetValue(error.GetType(), out var statusCode) && statusCode == StatusCodes.Status500InternalServerError))
            {
                return new ObjectResult(errors) { StatusCode = StatusCodes.Status500InternalServerError };
            }


            return new ObjectResult(result.Errors) { StatusCode = StatusCodes.Status400BadRequest };
        }
    }
}
