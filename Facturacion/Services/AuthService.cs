using Facturacion.DTOs;
using Facturacion.Models;
using Facturacion.Repositories;
using Facturacion.Utilities;

namespace Facturacion.Services
{
  public class AuthService(
    IAuthRepository repository,
    EntityExistenceChecker checker,
    PasswordHashManager passwordManager,
    JwtTokenManager tokenManager) : IAuthService
  {
    private readonly IAuthRepository _repository = repository;
    private readonly EntityExistenceChecker _checker = checker;
    private readonly PasswordHashManager _passwordManager = passwordManager;
    private readonly JwtTokenManager _tokenManager = tokenManager;
    public async Task<User?> Register(CreateUserDto createUserDto)
    {
      var userExists = await _checker.UserExists(createUserDto.Email);
      
      if (userExists) return null;

      var user = new User();
      var hashedPassword = _passwordManager.HashPassword(user, createUserDto.Password);

      user.Email = createUserDto.Email;
      user.PasswordHash = hashedPassword;

      await _repository.Create(user);
      await _repository.Save();

      return user;
    }
    public async Task<string?> Login(CreateUserDto createUserDto)
    {
      var user = await _repository.GetByEmail(createUserDto.Email);

      if (user == null) return null;

      if (!_passwordManager.IsValidPassword(user, user.PasswordHash, createUserDto.Password))
      {
        return null;
      }

      return _tokenManager.CreateToken(user);
    }
    public async Task<bool> UpdatePassword(int userId, UpdatePasswordDto updateDto)
    {
      var user = await _repository.GetById(userId);

      if (user == null) return false;

      if (!_passwordManager.IsValidPassword(user, user.PasswordHash, updateDto.CurrentPassword)) return false;

      var newHashedPassword = _passwordManager.HashPassword(user, updateDto.NewPassword);
      user.PasswordHash = newHashedPassword;

      _repository.Update(user);
      await _repository.Save();

      return true;
    }
  }
}
