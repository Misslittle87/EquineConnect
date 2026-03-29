using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EquineConnect.Mobile.Services
{
    public class TokenService
    {
        /// <summary>
        /// Dekoderar JWT-token och hämtar alla claims
        /// </summary>
        public static Dictionary<string, string> DecodeToken(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                
                var claims = new Dictionary<string, string>();
                foreach (var claim in jwtToken.Claims)
                {
                    claims[claim.Type] = claim.Value;
                }
                
                return claims;
            }
            catch
            {
                return new Dictionary<string, string>();
            }
        }

        /// <summary>
        /// Hämtar användar-ID från token
        /// </summary>
        public static string? GetUserId(string token)
        {
            var claims = DecodeToken(token);
            claims.TryGetValue(ClaimTypes.NameIdentifier, out var userId);
            return userId;
        }

        /// <summary>
        /// Hämtar e-post från token
        /// </summary>
        public static string? GetEmail(string token)
        {
            var claims = DecodeToken(token);
            claims.TryGetValue(ClaimTypes.Email, out var email);
            return email;
        }

        /// <summary>
        /// Hämtar alla roller från token
        /// </summary>
        public static List<string> GetRoles(string token)
        {
            var claims = DecodeToken(token);
            var roles = new List<string>();
            
            foreach (var claim in claims)
            {
                if (claim.Key == ClaimTypes.Role)
                {
                    roles.Add(claim.Value);
                }
            }
            
            return roles;
        }

        /// <summary>
        /// Kontrollerar om användaren har en specifik roll
        /// </summary>
        public static bool HasRole(string token, string role)
        {
            return GetRoles(token).Contains(role);
        }

        /// <summary>
        /// Kontrollerar om token är giltig och inte utgången
        /// </summary>
        public static bool IsTokenValid(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                
                return jwtToken.ValidTo > DateTime.UtcNow;
            }
            catch
            {
                return false;
            }
        }
    }
}
