using Facturacion.DTOs;
using Facturacion.Services;
using Facturacion.Utilities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Facturacion.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ArticlesController(
    IService<ArticleDto, CreateArticleDto, UpdateArticleDto> articleService,
    IValidationResultHelper validationResultHelper,
    IValidator<CreateArticleDto> createValidator, 
    IValidator<UpdateArticleDto> updateValidator) : ControllerBase
  {
    private readonly IService<ArticleDto, CreateArticleDto, UpdateArticleDto> _articleService = articleService;
    private readonly IValidationResultHelper _validationResultHelper = validationResultHelper;
    private readonly IValidator<CreateArticleDto> _createValidator = createValidator;
    private readonly IValidator<UpdateArticleDto> _updateValidator = updateValidator;

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
      var validationResult = await _createValidator.ValidateAsync(createArticleDto);
      if (!validationResult.IsValid)
      {
        return BadRequest(new { Errors = _validationResultHelper.GetErrorMessages(validationResult) });
      }
      var articleDto = await _articleService.Create(createArticleDto);

      return CreatedAtAction(
        nameof(GetById),
        new { id = articleDto.Id },
        new { Message = "Artículo creado exitosamente" }
       );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateArticleDto updateArticleDto)
    {
      var validationResult = await _updateValidator.ValidateAsync(updateArticleDto);
      if (!validationResult.IsValid)
      {
        return BadRequest(new { Errors = _validationResultHelper.GetErrorMessages(validationResult) });
      }
      var article = await _articleService.GetById(id);
      if (article == null) return NotFound(new { Message = "No se encontró el artículo" });
      await _articleService.Update(id, updateArticleDto);
      return Ok(new { Message = "Artículo actualizado exitosamente" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      var articleDto = await _articleService.GetById(id);
      if (articleDto == null) return NotFound(new { Message = "No se encontró el artículo" });
      await _articleService.Delete(articleDto.Id);
      return Ok(new { Message = "Artículo eliminado exitosamente" });
    }
  }
}
