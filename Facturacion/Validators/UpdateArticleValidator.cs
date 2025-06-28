using Facturacion.DTOs;
using FluentValidation;

namespace Facturacion.Validators
{
  public class UpdateArticleValidator : AbstractValidator<UpdateArticleDto>
  {
    public UpdateArticleValidator()
    {
      RuleFor(a => a.Description)
        .NotEmpty()
        .MaximumLength(100)
        .WithMessage("La descripción no puede exceder los 100 caracteres")
        .When(a => a.Description != null);

      RuleFor(a => a.UnitPrice)
        .GreaterThanOrEqualTo(0)
        .WithMessage("El precio unitario no puede ser un número negativo")
        .When(a => a.UnitPrice.HasValue);

      RuleFor(a => a.IsAvailable)
        .Must(v => v == true || v == false)
        .WithMessage("Valor inválido para disponibilidad")
        .When(a => a.IsAvailable.HasValue);
    }
  }
}
