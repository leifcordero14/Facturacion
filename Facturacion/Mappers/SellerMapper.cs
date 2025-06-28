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

      CreateMap<CreateSellerDto, Seller>()
        .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive ?? true))
        .ForMember(dest => dest.CommissionPercentage, opt => opt.MapFrom(src => src.CommissionPercentage ?? 0));
    }
  }
}
