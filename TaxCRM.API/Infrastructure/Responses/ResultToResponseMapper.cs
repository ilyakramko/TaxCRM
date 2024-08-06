using Microsoft.AspNetCore.Mvc;
using TaxCRM.Domain.Results;
using TaxCRM.Utils.Guards;

namespace TaxCRM.API.Infrastructure.Responses
{
    //TODO: use implicit operators?
    public static class ResultToResponseMapper
    {
        public static ActionResult ToResult(this Result result) =>
            result.Success ? result.Ok() : result.Failure();
        
        public static ObjectResult ToObjectResult<T>(this Result<T> result) => 
            result.Success ? result.Ok() : result.Failure();
        
        public static OkResult Ok(this Result result)
        {
            Guard.ArgumentPassCondition(result.Success, "Trying to call OK on failed result");
            return new OkResult();
        }

        public static OkObjectResult Ok<T>(this Result<T> result)
        {
            Guard.ArgumentPassCondition(result.Success, "Trying to call OK on failed result");
            return new OkObjectResult(result.Data);
        }

        public static CreatedResult Created<T>(this Result<T> result, string? uri)
        {
            Guard.ArgumentPassCondition(result.Success, "Trying to call OK on failed result");
            return new CreatedResult(uri, result.Data);
        }

        public static ObjectResult Failure(this Result result)
        {
            Guard.ArgumentPassCondition(!result.Success, "Trying to call Failure on succeed result");
            Guard.ArgumentIsNotNull(result.Error, "Error object is null on failed result");

            return HandleFailureInternal(result.Error);
        }

        public static ObjectResult Failure<T>(this Result<T> result)
        {
            Guard.ArgumentIsNotNull(result.Error, "Error object is null on failed result");
            return HandleFailureInternal(result.Error);
        }

        private static ObjectResult HandleFailureInternal(Error error) =>
            error.ErrorType switch
            {
                ErrorType.Argument => new BadRequestObjectResult(error),
                ErrorType.NotFound => new NotFoundObjectResult(error),
                _ => throw new ArgumentException("Invalid error type provided")
            };
}
}
