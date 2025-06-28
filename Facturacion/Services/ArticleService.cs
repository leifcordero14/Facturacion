using AutoMapper;
using Facturacion.DTOs;
using Facturacion.Models;
using Facturacion.Repositories;

namespace Facturacion.Services
{
  public class ArticleService(IRepository<Article> repository, IMapper mapper) : IService<ArticleDto, CreateArticleDto, UpdateArticleDto>
  {
    private readonly IRepository<Article> _repository = repository;
    private readonly IMapper _mapper = mapper;
    public async Task<IEnumerable<ArticleDto>> GetAll()
    {
      var articles = await _repository.GetAll();
      var articleDtos = _mapper.Map<List<ArticleDto>>(articles);
      return articleDtos;
    }
    public async Task<ArticleDto?> GetById(int id)
    {
      var article = await _repository.GetById(id);
      if (article == null) return null;
      var articleDto = _mapper.Map<ArticleDto>(article);
      return articleDto;
    }
    public async Task<ArticleDto> Create(CreateArticleDto createArticleDto)
    {
      var article = _mapper.Map<Article>(createArticleDto);
      await _repository.Create(article);
      await _repository.Save();
      var articleDto = _mapper.Map<ArticleDto>(article);
      return articleDto;
    }
    public async Task Update(int id, UpdateArticleDto updateArticleDto)
    {
      var article = await _repository.GetById(id);
      if (article == null) throw new Exception("No se encontró el artículo");

      article.Description = updateArticleDto.Description ?? article.Description;
      article.UnitPrice = updateArticleDto.UnitPrice ?? article.UnitPrice;
      article.IsAvailable = updateArticleDto.IsAvailable ?? article.IsAvailable;

      _repository.Update(article);
      await _repository.Save();
      return;
    }
    public async Task Delete(int id)
    {
      var article = await _repository.GetById(id);
      if (article == null) throw new Exception("No se encontró el artículo");
      _repository.Delete(article);
      await _repository.Save();
      return;
    }
  }
}
