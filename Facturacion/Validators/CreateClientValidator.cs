using Facturacion.Data;
using Facturacion.DTOs;
using Facturacion.Utilities;
using FluentValidation;

namespace Facturacion.Validators
{
  public class CreateClientValidator : AbstractValidator<CreateClientDto>
  {
    private readonly ApplicationDbContext _context;
    public CreateClientValidator(ApplicationDbContext context)
    {
      _context = context;

      RuleFor(c => c.BusinessName)
        .NotEmpty()
        .WithMessage("El nombre comercial o razón social es obligatorio")
        .MaximumLength(100)
        .WithMessage("El nombre comercial o razón social no puede exceder los 100 caracteres");

      RuleFor(c => c.IdentificationNumber)
        .NotEmpty()
        .WithMessage("El RNC o cédula es obligatorio")
        .Must(IdentificationNumberHelper.IsValidCedulaOrRNC)
        .WithMessage("El RNC o cédula es inválido")
        .MustAsync(async (id, cancellationToken) =>
          !await IdentificationNumberHelper.AlreadyExists(_context, id, cancellationToken))
        .WithMessage("El RNC o cédula ya está registrado");

      RuleFor(c => c.LedgerAccount)
        .NotEmpty()
        .WithMessage("La cuenta contable es obligatoria");
    }
  }
}
