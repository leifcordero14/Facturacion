using AutoMapper;
using Facturacion.DTOs;
using Facturacion.Models;

namespace Facturacion.Mappers
{
  public class SellerMapper : Profile
  {
    public SellerMapper()
    {
      CreateMap<Seller, SellerDto>();

      CreateMap<CreateSellerDto, Seller>();
    }
  }
}
