using StreamXAPI.Authentication;
using System.Net;

namespace StreamXAPI.MiddleWare
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, IJwtValidation jwt)
        {

            var authHeader = context.Request.Headers.Authorization.ToString();

            if (string.IsNullOrWhiteSpace(authHeader))
            {
                await UnauthorizedResponse(context, "Authorization header is missing.");
                return;
            }

            if (!authHeader.StartsWith("Bearer "))
            {
                await UnauthorizedResponse(context, "Invalid authorization scheme.");
                return;
            }

            var token = authHeader["Bearer ".Length..].Trim();

            var principal = jwt.ValidateToken(token);

            if (principal is null)
            {
                await UnauthorizedResponse(context, "Invalid or expired token.");
                return;
            }

            context.User = principal;

            await _next(context);
        }
        private static async Task UnauthorizedResponse(HttpContext context, string message)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

            await context.Response.WriteAsJsonAsync(
                new AuthResponse
                {
                    StatusCode = 401,
                    StatusMessage = message
                });
        }
    }
}
