using System.Net;

namespace api_bulk_fill_v2.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; private set; }
        public T Value { get; private set; }
        public string Error { get; private set; }
        public int StatusCode { get; private set; }

        private Result(bool isSuccess, T value, string error, int statusCode)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error;
            StatusCode = statusCode;
        }

        public static Result<T> Success(T value, int statusCode = (int)HttpStatusCode.OK)
        {
            return new Result<T>(true, value, null, statusCode);
        }

        public static Result<T> Success(T value, HttpStatusCode statusCode)
        {
            return new Result<T>(true, value, null, (int)statusCode);
        }

        public static Result<T> Failure(string error, int statusCode = (int)HttpStatusCode.BadRequest)
        {
            return new Result<T>(false, default, error, statusCode);
        }

        public static Result<T> Failure(string error, HttpStatusCode statusCode)
        {
            return new Result<T>(false, default, error, (int)statusCode);
        }

        // Common success results
        public static Result<T> SuccessResult(T value) => Success(value, HttpStatusCode.OK);
        public static Result<T> CreatedResult(T value) => Success(value, HttpStatusCode.Created);
        public static Result<T> NoContentResult() => Success(default, HttpStatusCode.NoContent);

        // Common failure results
        public static Result<T> BadRequestResult(string error = "Bad request")
            => Failure(error, HttpStatusCode.BadRequest);

        public static Result<T> UnauthorizedResult(string error = "Unauthorized")
            => Failure(error, HttpStatusCode.Unauthorized);

        public static Result<T> ForbiddenResult(string error = "Forbidden")
            => Failure(error, HttpStatusCode.Forbidden);

        public static Result<T> NotFoundResult(string error = "Resource not found")
            => Failure(error, HttpStatusCode.NotFound);

        public static Result<T> ConflictResult(string error = "Resource already exists")
            => Failure(error, HttpStatusCode.Conflict);

        public static Result<T> InternalServerErrorResult(string error = "Internal server error")
            => Failure(error, HttpStatusCode.InternalServerError);
    }
}