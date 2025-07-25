using Facturacion.DTOs;
using Facturacion.Models;

namespace Facturacion.Services
{
  public interface IAuthService
  {
    Task<User?> Register(CreateUserDto createUserDto);
    Task<string?> Login(CreateUserDto createUserDto);
    Task<bool> UpdatePassword(int id, UpdatePasswordDto updatePasswordDto);
  }
}
