using AutoMapper;
using Facturacion.DTOs;
using Facturacion.Models;

namespace Facturacion.Mappers
{
  public class ArticleMapper : Profile
  {
    public ArticleMapper()
    {
      CreateMap<Article, ArticleDto>();

      CreateMap<CreateArticleDto, Article>()
        .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.IsAvailable ?? true));
    }
  }
}
