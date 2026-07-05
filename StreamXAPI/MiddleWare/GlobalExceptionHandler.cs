using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StreamXAPI.CustomeExceptions;

namespace StreamXAPI.MiddleWare
{
    public class GlobalExceptionHandler : IExceptionHandler
    {

        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception,
            CancellationToken cancellationToken)
        {
            var problam = new ProblemDetails();

            switch(exception)
            {
                case NotFoundException notFoundException:
                    problam.Status = StatusCodes.Status404NotFound;
                    problam.Title = "Not Found";
                    problam.Detail = notFoundException.Message;
                    break;

                case DuplicateException duplicateException:
                    problam.Status = StatusCodes.Status409Conflict;
                    problam.Title = "Conflict";
                    problam.Detail = duplicateException.Message;
                    break;

                case ValidationException validationException:
                    problam.Status = StatusCodes.Status400BadRequest;
                    problam.Title = "Bad Request";
                    problam.Detail = validationException.Message;
                    break;

                default:
                    problam.Status = StatusCodes.Status500InternalServerError;
                    problam.Title = "Internal Server Error";
                    problam.Detail = exception.Message;
                    break;
            }
            problam.Instance = context.Request.Path;

            context.Response.StatusCode = problam.Status.Value;
            await context.Response.WriteAsJsonAsync(problam, cancellationToken);

            return true;
        }
    }
    
}
