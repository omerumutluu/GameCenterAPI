using GameCenterAPI.Application.Abstraction.Token;
using GameCenterAPI.Domain.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GameCenterAPI.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }

        public Application.DTOs.Token CreateToken(AppUser user)
        {
            Application.DTOs.Token Token = new();

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            Token.Expiration = DateTime.Now.AddMinutes(1);

            JwtSecurityToken securityToken = new(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: Token.Expiration,
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials,
                claims: SetClaims(user)
            );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            Token.AccessToken = tokenHandler.WriteToken(securityToken);
            Token.RefreshToken = CreateRefreshToken();

            return Token;
        }

        IEnumerable<Claim> SetClaims(AppUser user)
        {
            var claims = new List<Claim>();

            claims.Add(new(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new(ClaimTypes.Email, user.Email));
            claims.Add(new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"));

            if (user.OperationClaims != null)
                foreach (var operationClaim in user.OperationClaims)
                    claims.Add(new(ClaimTypes.Role, operationClaim));


            return claims;
        }
    }
}
