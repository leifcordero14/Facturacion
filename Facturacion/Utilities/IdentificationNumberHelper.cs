using Facturacion.Data;
using Microsoft.EntityFrameworkCore;

namespace Facturacion.Utilities
{
  public static class IdentificationNumberHelper
  {
    public static bool IsValidCedulaOrRNC(string identificationNumber)
    {
      if (string.IsNullOrWhiteSpace(identificationNumber)) return false;
      string cleanId = identificationNumber.Replace("-", "").Replace(" ", "").Trim();
      return IsValidCedula(cleanId) || IsValidRNC(cleanId);
    }

    private static bool IsValidCedula(string cedula)
    {
      if (cedula.Length != 11 || !cedula.All(char.IsDigit)) return false;
      if (cedula.Distinct().Count() == 1) return false;

      int total = 0;
      int[] multipliers = [1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1];

      for (int i = 0; i < 11; i++)
      {
        int digit = int.Parse(cedula[i].ToString());
        int product = digit * multipliers[i];
        total += (product < 10) ? product : (product / 10) + (product % 10);
      }

      return total % 10 == 0;
    }

    private static bool IsValidRNC(string rnc)
    {
      if (rnc.Length != 9 || !rnc.All(char.IsDigit)) return false;
      if (!"145".Contains(rnc[0])) return false;

      int sum = 0;
      int[] multipliers = [7, 9, 8, 6, 5, 4, 3, 2];

      for (int i = 0; i < 8; i++)
      {
        int digito = int.Parse(rnc[i].ToString());
        sum += digito * multipliers[i];
      }

      int modulo = sum % 11;
      int verifier = int.Parse(rnc[8].ToString());

      return (modulo == 0 && verifier == 1) ||
             (modulo == 1 && verifier == 1) ||
             ((11 - modulo) == verifier);
    }

    public static async Task<bool> AlreadyExists(ApplicationDbContext context, string identificationNumber, CancellationToken cancellationToken = default)
    {
      string cleanId = identificationNumber.Replace("-", "").Replace(" ", "").Trim();
      return await context.Client.AnyAsync(c => c.IdentificationNumber == cleanId, cancellationToken);
    }
  }
}
