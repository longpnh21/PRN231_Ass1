using BusinessObject;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eStoreAPI.Helpers
{
    public class JwtServices
    {
        public string GenerateJwtToken(Member member)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var role = "member";
            if (member.Equals("admin@estore.com"))
            {
                role = "admin";
            }
            var key = Encoding.ASCII.GetBytes("secretCode");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", member.MemberId.ToString()), new Claim("role", role) }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
