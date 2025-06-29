using FluentValidation.Results;

namespace Facturacion.Utilities
{
  public class ValidationResultHelper
  {
    public static IEnumerable<string> GetErrorMessages(ValidationResult validationResult)
    {
      return validationResult.Errors.Select(error => error.ErrorMessage);
    }
  }
}
