using AuthService.Application;
using AuthService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.Infrastructure.Jwt
{
    internal class AccessTokenGenerator(ITokenOptions tokenOptions, UserManager<User> userManager) : IAccessTokenGenerator
    {
        private async Task<List<Claim>> GetClaims(User user)
        {
            var roles = await userManager.GetRolesAsync(user);

            return [
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                ..tokenOptions.Audience.Select(aud => new Claim(JwtRegisteredClaimNames.Aud, aud)),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                .. roles.Select(role => new Claim(ClaimTypes.Role, role))
            ];
        }

        public async Task<string> GenerateAsync(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey));
            JwtSecurityToken jwtSecurityToken = new(
                issuer: tokenOptions.Issuer,
                expires: DateTime.Now.AddMinutes(tokenOptions.AccessTokenValidtyPeriod),
                notBefore: DateTime.Now,
                claims: await GetClaims(user),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            );
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}
