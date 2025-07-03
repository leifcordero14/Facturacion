using Facturacion.DTOs;
using Facturacion.Utilities;
using FluentValidation;

namespace Facturacion.Validators
{
  public class UpdateClientValidator : AbstractValidator<UpdateClientDto>
  {
    public UpdateClientValidator()
    {
      RuleFor(c => c.BusinessName)
        .NotEmpty()
        .WithMessage("El nombre comercial o razón social es obligatorio")
        .MaximumLength(100)
        .WithMessage("El nombre comercial o razón social no puede exceder los 100 caracteres");

      RuleFor(c => c.IdentificationNumber)
        .NotEmpty()
        .WithMessage("El RNC o cédula es obligatorio")
        .Must(IdentificationNumberHelper.IsValidCedulaOrRNC)
        .WithMessage("El RNC o cédula es inválido");

      RuleFor(c => c.LedgerAccount)
        .NotEmpty()
        .WithMessage("La cuenta contable es obligatoria");
    }
  }
}
