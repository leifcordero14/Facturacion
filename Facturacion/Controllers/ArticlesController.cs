using Facturacion.DTOs;
using Facturacion.Models;
using Facturacion.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Facturacion.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ArticlesController(IRepository<Article> repository) : ControllerBase
  {
    private readonly IRepository<Article> _repository = repository;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ArticleDto>>> GetAll()
    {
      var articles = await _repository.GetAll();

      var articleDtos = articles.Select(a => new ArticleDto
      {
        Id = a.Id,
        Description = a.Description,
        UnitPrice = a.UnitPrice,
        IsAvailable = a.IsAvailable,
      }).ToList();

      return Ok(articleDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ArticleDto>> GetById(int id)
    {
      var article = await _repository.GetById(id);
      
      if (article == null) return NotFound(new { Message = "No se encontró el artículo" });
      
      var articleDto = new ArticleDto
      {
        Id = article.Id,
        Description = article.Description,
        UnitPrice = article.UnitPrice,
        IsAvailable = article.IsAvailable,
      };
      return Ok(articleDto);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateArticleDto createArticleDto)
    {
      var article = new Article
      {
        Description = createArticleDto.Description,
        UnitPrice = createArticleDto.UnitPrice,
        IsAvailable = createArticleDto.IsAvailable ?? true,
      };

      await _repository.Create(article);
      await _repository.Save();

      var articleDto = new ArticleDto
      {
        Id = article.Id,
        Description = article.Description,
        UnitPrice = article.UnitPrice,
        IsAvailable = article.IsAvailable,
      };

      return CreatedAtAction(
        nameof(GetById),
        new { id = article.Id }, 
        new { Message = "Artículo creado exitosamente" }
       );
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateArticleDto updateArticleDto)
    {
      var article = await _repository.GetById(id);

      if (article == null) return NotFound(new { Message = "No se encontró el artículo" });

      article.Description = updateArticleDto.Description ?? article.Description;
      article.UnitPrice = updateArticleDto.UnitPrice ?? article.UnitPrice;
      article.IsAvailable = updateArticleDto.IsAvailable ?? article.IsAvailable;

      _repository.Update(article);
      await _repository.Save();

      return Ok(new { Message = "Artículo actualizado exitosamente" });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
      var article = await _repository.GetById(id);

      if (article == null) return NotFound(new { Message = "No se encontró el artículo" });

      _repository.Delete(article);
      await _repository.Save();

      return Ok(new { Message = "Artículo eliminado exitosamente" });
    }
  }
}
