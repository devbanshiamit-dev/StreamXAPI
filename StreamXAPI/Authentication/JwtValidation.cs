using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StreamXAPI.Authentication
{
    public class JwtValidation : IJwtValidation
    {
        private readonly IConfiguration _conf;
        public JwtValidation(IConfiguration conf)
        {
            _conf = conf;
        }
        public ClaimsPrincipal? ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_conf["Jwt:Key"]!);

            try
            {
                var principal = tokenHandler.ValidateToken(
                    token,
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = _conf["Jwt:Issuer"],
                        ValidAudience = _conf["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(key),

                        ClockSkew = TimeSpan.Zero
                    },
                    out SecurityToken validatedToken);

                return principal;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
