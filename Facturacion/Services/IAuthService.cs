using Facturacion.DTOs;
using Facturacion.Models;

namespace Facturacion.Services
{
  public interface IAuthService
  {
    Task<User?> Register(CreateUserDto createUserDto);
    Task<TokenResponseDto?> Login(CreateUserDto createUserDto);
    Task<bool> UpdatePassword(int id, UpdatePasswordDto updatePasswordDto);
    Task<TokenResponseDto?> GetTokenResponse(RefreshTokenRequestDto request);
    Task<bool> Logout(int userId);
  }
}
