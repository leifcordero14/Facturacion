using FluentValidation.Results;

namespace Facturacion.Utilities
{
  public interface IValidationResultHelper
  {
    IEnumerable<string> GetErrorMessages(ValidationResult vaidationResult);
  }
}
