using Microsoft.IdentityModel.Tokens;
using Movie_Store_Data.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Store_API.Extensions
{
    public class JwtHelpers
    {
        private string secretKey;
        private static object lockobj = new object();
        private static JwtHelpers _instance;

        public static JwtHelpers GetInstance(string secretkey)
        {
            lock (lockobj)
            {
                if (_instance == null)
                    _instance = new JwtHelpers(secretkey);
            }
            return _instance;
        }

        private JwtHelpers(string secretKey)
        {
            if (string.IsNullOrEmpty(this.secretKey))
                this.secretKey = secretKey;
        }

        public string CreateToken(User user, double exp = 20)
        {
            var symmetricKey = Encoding.UTF8.GetBytes(secretKey);
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                        {
                            new Claim("id", user.ID.ToString()),
                            new Claim(ClaimTypes.Name, user.Username)
                        }),

                Expires = now.AddMinutes(exp),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }

        public JwtSecurityToken IsValidToken(string token, string user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            return (JwtSecurityToken)validatedToken;
        }
    }
}
