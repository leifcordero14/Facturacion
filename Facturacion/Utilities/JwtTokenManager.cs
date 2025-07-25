using Facturacion.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Facturacion.Utilities
{
  public class JwtTokenManager(IConfiguration configuration)
  {
    private readonly IConfiguration _configuration = configuration;
    public string CreateToken(User user)
    {
      var claim = new List<Claim>()
      {
        new(ClaimTypes.Email, user.Email),
        new(ClaimTypes.NameIdentifier, user.Id.ToString())
      };

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Token")!));

      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

      var tokenDescriptor = new JwtSecurityToken(
        issuer: _configuration.GetValue<string>("Issuer"),
        audience: _configuration.GetValue<string>("Audience"),
        claims: claim,
        expires: DateTime.Now.AddDays(1),
        signingCredentials: creds
        );

      return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
  }
}
