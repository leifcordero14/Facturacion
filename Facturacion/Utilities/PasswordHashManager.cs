using Facturacion.Models;
using Microsoft.AspNetCore.Identity;

namespace Facturacion.Utilities
{
  public class PasswordHashManager
  {
    private readonly PasswordHasher<User> _passwordHasher = new();
    public string HashPassword(User user, string password)
    {
      var hashedPassword = _passwordHasher.HashPassword(user, password);
      return hashedPassword;
    }
    public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string password)
    {
      var result = _passwordHasher.VerifyHashedPassword(user, hashedPassword, password);
      return result;
    }
    public bool IsValidPassword(User user, string hashedPassword, string password)
    {
      return VerifyHashedPassword(user, hashedPassword, password) != PasswordVerificationResult.Failed;
    }
  }
}
