using System.Security.Claims;

namespace StreamXAPI.Authentication
{
    public interface IJwtValidation
    {
        public ClaimsPrincipal? ValidateToken(string token);
    }
}
