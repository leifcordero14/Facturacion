using AutoMapper;
using Facturacion.DTOs;
using Facturacion.Models;

namespace Facturacion.Mappers
{
  public class BillingMapper : Profile
  {
    public BillingMapper()
    {
      CreateMap<Billing, BillingDto>();

      CreateMap<CreateBillingDto, Billing>();

      CreateMap<Article, ArticleDto>();

      CreateMap<Client, ClientDto>();

      CreateMap<Seller, SellerDto>();
    }
  }
}
