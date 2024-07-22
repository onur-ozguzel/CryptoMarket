using Microsoft.AspNetCore.Http;

namespace CryptoMarket.Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails
{
    public class BusinessProblemDetails : ProblemDetails
    {
        public BusinessProblemDetails(string detail)
        {
            Title = "Rule Violation";
            Detail = detail;
            Status = StatusCodes.Status400BadRequest;
        }
    }
}
