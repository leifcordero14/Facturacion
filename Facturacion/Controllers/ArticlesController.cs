using Facturacion.DTOs;
using Facturacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Facturacion.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ArticlesController(IService<ArticleDto, CreateArticleDto, UpdateArticleDto> articleService) : ControllerBase
  {
    private readonly IService<ArticleDto, CreateArticleDto, UpdateArticleDto> _articleService = articleService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ArticleDto>>> GetAll()
    {
      var articleDtos = await _articleService.GetAll();
      return Ok(articleDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ArticleDto>> GetById(int id)
    {
      var articleDto = await _articleService.GetById(id); 
      if (articleDto == null) return NotFound(new { Message = "No se encontró el artículo" });
      return Ok(articleDto);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateArticleDto createArticleDto)
    {
      var articleDto = await _articleService.Create(createArticleDto);

      return CreatedAtAction(
        nameof(GetById),
        new { id = articleDto.Id }, 
        new { Message = "Artículo creado exitosamente" }
       );
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateArticleDto updateArticleDto)
    {
      var article = await _articleService.GetById(id);
      if (article == null) return NotFound(new { Message = "No se encontró el artículo" });
      await _articleService.Update(id, updateArticleDto);
      return Ok(new { Message = "Artículo actualizado exitosamente" });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
      var articleDto = await _articleService.GetById(id);
      if (articleDto == null) return NotFound(new { Message = "No se encontró el artículo" });
      await _articleService.Delete(articleDto.Id);
      return Ok(new { Message = "Artículo eliminado exitosamente" });
    }
  }
}
