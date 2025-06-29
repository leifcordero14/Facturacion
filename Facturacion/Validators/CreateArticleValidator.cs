using Facturacion.DTOs;
using FluentValidation;

namespace Facturacion.Validators
{
  public class CreateArticleValidator : AbstractValidator<CreateArticleDto>
  {
    public CreateArticleValidator()
    {
      RuleFor(a => a.Description)
        .NotEmpty()
        .WithMessage("La descripción es obligatoria")
        .MaximumLength(100)
        .WithMessage("La descripción no puede exceder los 100 caracteres");

      RuleFor(a => a.UnitPrice)
        .GreaterThanOrEqualTo(0)
        .WithMessage("El precio unitario no puede ser negativo");
    }
  }
}
