using Exams.Application.Interfaces;
using Exams.Domain.Entities;
using Exams.Domain.Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Exams.Application.Services
{
    public class TokenGeneratorService(IConfiguration configuration) : ITokenGeneratorService
    {
        public string GetToken(User user)
        {
            var claims = GetClaims(user);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken(
                issuer: configuration["JWT:Issuer"],
                audience: configuration["JWT:Audience"],
                claims: claims,
                notBefore: Helper.GetCurrentDateTime(),
                expires: Helper.GetCurrentDateTime().AddDays(1),
                signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }

        private List<Claim> GetClaims(User user)
            => new List<Claim>
            {
                new Claim(nameof(User.Id), user.Id.ToString()),
                new Claim(nameof(User.PhoneNumber), user.PhoneNumber),
                new Claim(nameof(User.Role), user.Role.ToString())
            };
    }
}
