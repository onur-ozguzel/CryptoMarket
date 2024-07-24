using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace CryptoMarket.Business.Services
{
    public interface IErrorHandlingService
    {
        IActionResult HandleResult<T>(IResult<T> result);
    }
}