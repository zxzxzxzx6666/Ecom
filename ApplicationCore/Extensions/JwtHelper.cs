﻿using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using ApplicationCore.Entities.IdentityAggregate;


namespace ApplicationCore.Extensions
{
    /// <summary>
    /// JWT helper
    /// </summary>
    public class JwtHelper
    {
        // todo put secret in config
        // Secret key (used to encrypt JWT)
        private const string SecretKey = "YourSuperSecretKey123456789101112131415!";

        // Set Access Token validity period (minutes)
        private const int AccessTokenExpirationMinutes = 60;

        /// <summary>
        /// Generate JWT Token ，使用 JWT Bearer 驗證方案改用 api 後可使用這個解法 [Authorize(AuthenticationSchemes = "JwtBearer")] 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="username"></param>
        /// <param name="role">User role (e.g., Admin, User)</param>
        /// <returns>JWT Token </returns>
        public static string GenerateJwtToken(string userId, string username, List<string> roles)
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
            };

            // claims add roles
            foreach (var role in roles)
            {
                claims.Append(new Claim(ClaimTypes.Role, role));
            }

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
        /// GenerateClaims for mvc redirect 
        /// todo : 整合 GenerateJwtToken 去重
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="username"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public static Claim[] GenerateClaims(string userId, string username, List<string> roles)
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
            };

            // claims add roles
            foreach (var role in roles)
            {
                claims.Append(new Claim(ClaimTypes.Role, role));
            }

            // Convert JWT to a string and return it
            return claims;
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
