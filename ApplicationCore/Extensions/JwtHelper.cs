using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;


namespace ApplicationCore.Extensions
{
    /// <summary>
    /// JWT helper
    /// </summary>
    public class JwtHelper
    {
        // Secret key (used to encrypt JWT)
        private const string SecretKey = "YourSuperSecretKey123456!";

        // Set Access Token validity period (minutes)
        private const int AccessTokenExpirationMinutes = 60;

        /// <summary>
        /// Generate JWT Token
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="username"></param>
        /// <param name="role">User role (e.g., Admin, User)</param>
        /// <returns>JWT Token </returns>
        public static string GenerateJwtToken(string userId, string username, string role)
        {
            // Create encryption key (using HMAC SHA256)
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

            // Create a signature certificate
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create Claims in JWT (can be used to store user data)
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),  
                new Claim(JwtRegisteredClaimNames.UniqueName, username), 
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // JWT unique identification code (to prevent duplication)
                new Claim(ClaimTypes.Role, role) // 加入角色資訊
            };

            // Generate JWT Token
            var token = new JwtSecurityToken(
                issuer: "ecom", // usually the server name
                audience: "web", // usually the client application
                claims: claims, 
                expires: DateTime.UtcNow.AddMinutes(AccessTokenExpirationMinutes), // Set the token expiration time
                signingCredentials: credentials // Set the signature method
            );

            // Convert JWT to a string and return it
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Verify and parse JWT Token
        /// </summary>
        /// <param name="token">JWT Token</param>
        /// <returns>The parsed ClaimsPrincipal (containing user information), or null if invalid</returns>
        public static ClaimsPrincipal? ValidateJwtToken(string token)
        {
            // Parse the encryption key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                // Set Token verification parameters
                var parameters = new TokenValidationParameters
                {
                    ValidateIssuer = true, 
                    ValidateAudience = true, 
                    ValidateLifetime = true, // Verify whether the Token is expired
                    ValidateIssuerSigningKey = true, // Verify the signature key
                    ValidIssuer = "ecom", // Set the legal publisher
                    ValidAudience = "web", // Set up legal Audience
                    IssuerSigningKey = key // Set a valid encryption key
                };

                // Verify the Token and parse the information in the Token
                return tokenHandler.ValidateToken(token, parameters, out _);
            }
            catch
            {
                return null;
            }
        }
    }


}
