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

      CreateMap<CreateArticleDto, Article>();
    }
  }
}
