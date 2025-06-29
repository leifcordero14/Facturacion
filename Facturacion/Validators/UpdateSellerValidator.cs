using Facturacion.DTOs;
using FluentValidation;

namespace Facturacion.Validators
{
  public class UpdateSellerValidator : AbstractValidator<UpdateSellerDto>
  {
    public UpdateSellerValidator()
    {
      RuleFor(s => s.FirstName)
        .NotEmpty()
        .WithMessage("El nombre es obligatorio");

      RuleFor(s => s.LastName)
        .NotEmpty()
        .WithMessage("El apellido es obligatorio");

      RuleFor(s => s.CommissionPercentage)
        .InclusiveBetween(0, 100)
        .WithMessage("El porcentaje de comisión debe estar entre 0 y 100");
    }
  }
}
