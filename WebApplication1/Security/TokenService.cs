using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebApplication1.Entities;

namespace WebApplication1.Security
{
    public  class TokenService(IOptions<JwtTokenOptions>options , UserManager<ApplicationUser>userManager)
    {
        public  async Task<string> GenerateTokenAsync(string Id)
        {
            var jwtoptions = options.Value;
            var user = await userManager.FindByIdAsync(Id.ToString());

            if (user == null)
            {
                return "user not found!";
            }
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            };
            ICollection<string> roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtoptions.key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            var accessToken = new JwtSecurityToken(
                issuer: jwtoptions.issuer,
                audience: jwtoptions.audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(jwtoptions.lifetime),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(accessToken);

        }
    }
}
