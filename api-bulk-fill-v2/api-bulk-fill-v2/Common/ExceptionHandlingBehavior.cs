using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace api_bulk_fill_v2.Common
{
    public class ExceptionHandlingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TResponse : class
    {
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            try
            {
                return await next(); // Proceed to next handler
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx &&
                                               (sqlEx.Number == 2627 || sqlEx.Number == 2601)) // Duplicate
            {
                const string errorMessage = "A record with the same email already exists.";
                return CreateFailureResult(errorMessage, HttpStatusCode.Conflict); // 409
            }
            catch (UnauthorizedAccessException ex)
            {
                return CreateFailureResult(ex.Message, HttpStatusCode.Unauthorized); // 401
            }
            catch (KeyNotFoundException ex)
            {
                return CreateFailureResult(ex.Message, HttpStatusCode.NotFound); // 404
            }
            catch (ArgumentException ex)
            {
                return CreateFailureResult("Bad request: " + ex.Message, HttpStatusCode.BadRequest); // 400
            }
            catch (Exception ex)
            {
                var errorMessage = $"An unexpected error occurred: {ex.Message}";
                return CreateFailureResult(errorMessage, HttpStatusCode.InternalServerError); // 500
            }
        }

        private TResponse CreateFailureResult(string error, HttpStatusCode statusCode)
        {
            var responseType = typeof(TResponse);

            if (!responseType.IsGenericType || responseType.GetGenericTypeDefinition() != typeof(Result<>))
                throw new InvalidOperationException("TResponse must be Result<T>");

            var genericType = responseType.GetGenericArguments().First();

            var failureMethod = typeof(Result<>)
                .MakeGenericType(genericType)
                .GetMethod("Failure", new[] { typeof(string), typeof(int) });

            if (failureMethod != null)
            {
                return failureMethod.Invoke(null, new object[] { error, (int)statusCode }) as TResponse;
            }

            throw new InvalidOperationException("Unable to locate Result<T>.Failure method with (string, int) signature.");
        }
    }
}
