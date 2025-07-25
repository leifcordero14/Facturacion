using Facturacion.DTOs;
using Facturacion.Models;
using Facturacion.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Facturacion.Utilities
{
  public class JwtTokenManager(IConfiguration configuration, IAuthRepository repository)
  {
    private readonly IConfiguration _configuration = configuration;
    private readonly IAuthRepository _repository = repository;
    public TokenResponseDto CreateTokenResponse(string token, string refreshToken)
    {
      return new TokenResponseDto
      {
        AccessToken = token,
        RefreshToken = refreshToken,
      };
    }
    public string CreateAccessToken(User user)
    {
      var claim = new List<Claim>()
      {
        new(ClaimTypes.Email, user.Email),
        new(ClaimTypes.NameIdentifier, user.Id.ToString())
      };

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JwtSettings:Secret")!));

      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

      var tokenDescriptor = new JwtSecurityToken(
        issuer: _configuration.GetValue<string>("JwtSettings:Issuer"),
        audience: _configuration.GetValue<string>("JwtSettings:Audience"),
        claims: claim,
        expires: DateTime.UtcNow.AddDays(1),
        signingCredentials: creds
        );

      return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
    public string CreateRefreshToken()
    {
      var randomNumber = new byte[32];
      using var rng = RandomNumberGenerator.Create();
      rng.GetBytes(randomNumber);
      return Convert.ToBase64String(randomNumber);
    }
    public async Task<string> SaveRefreshToken(User user, string refreshToken)
    {
      user.RefreshToken = refreshToken;
      user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
      _repository.Update(user);
      await _repository.Save();
      return refreshToken;
    }
    public bool IsValidRefreshToken(User user, string refreshToken)
    {
      if (user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow) return false;
      return true;
    }
  }
}
