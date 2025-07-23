using Facturacion.DTOs;
using Facturacion.Utilities;
using FluentValidation;

namespace Facturacion.Validators
{
  public class CreateBillingValidator : AbstractValidator<CreateBillingDto>
  {
    public CreateBillingValidator(EntityExistenceChecker checker)
    {
      RuleFor(b => b.UnitPrice)
          .GreaterThan(0)
          .WithMessage("El precio debe ser mayor a 0");

      RuleFor(b => b.Quantity)
        .GreaterThanOrEqualTo(1)
        .WithMessage("La cantidad debe ser mayor o igual a 1");

      RuleFor(x => x.ArticleId)
          .MustAsync(checker.ArticleExists)
          .WithMessage("El artículo especificado no existe");

      RuleFor(x => x.ClientId)
          .MustAsync(checker.ClientExists)
          .WithMessage("El cliente especificado no existe");

      RuleFor(x => x.SellerId)
          .MustAsync(checker.SellerExists)
          .WithMessage("El vendedor especificado no existe");
    }
  }
}
