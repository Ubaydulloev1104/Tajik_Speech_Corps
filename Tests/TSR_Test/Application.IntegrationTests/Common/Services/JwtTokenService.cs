using Application.IntegrationTests.Common.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Application.IntegrationTests.Common.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string CreateTokenByClaims(IList<Claim> claims)
        {
            SymmetricSecurityKey key = new(Encoding.UTF8
                .GetBytes(_configuration.GetSection("JWT")["Secret"]!));

            SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha512Signature);

            JwtSecurityToken token = new(
                claims: claims,
                expires: DateTime.Now.AddDays(int.Parse(_configuration.GetSection("JWT")["ExpireDayFromNow"]!)),
                signingCredentials: creds);

            string jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
