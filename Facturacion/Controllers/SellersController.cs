using Facturacion.DTOs;
using Facturacion.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Facturacion.Utilities;

namespace Facturacion.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class SellersController(
    IService<SellerDto, CreateSellerDto, UpdateSellerDto> sellerService,
    IValidationResultHelper validationResultHelper,
    IValidator<CreateSellerDto> createValidator,
    IValidator<UpdateSellerDto> updateValidator) : ControllerBase
  {
    private readonly IService<SellerDto, CreateSellerDto, UpdateSellerDto> _sellerService = sellerService;
    private readonly IValidationResultHelper _validationResultHelper = validationResultHelper;
    private readonly IValidator<CreateSellerDto> _createValidator = createValidator;
    private readonly IValidator<UpdateSellerDto> _updateValidator = updateValidator;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SellerDto>>> GetAll()
    {
      var sellerDtos = await _sellerService.GetAll();
      return Ok(sellerDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SellerDto>> GetById(int id)
    {
      var sellerDto = await _sellerService.GetById(id);
      if (sellerDto == null) return NotFound(new { Message = "No se encontró el vendedor" });
      return Ok(sellerDto);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSellerDto createSellerDto)
    {
      var validationResult = await _createValidator.ValidateAsync(createSellerDto);
      if (!validationResult.IsValid) 
      {
        return BadRequest(new { Errors = _validationResultHelper.GetErrorMessages(validationResult) });
      }
      var sellerDto = await _sellerService.Create(createSellerDto);

      return CreatedAtAction(
        nameof(GetById),
        new { id = sellerDto.Id },
        new { Message = "Vendedor creado exitosamente" }
       );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateSellerDto updateSellerDto)
    {
      var validationResult = await _updateValidator.ValidateAsync(updateSellerDto);
      if (!validationResult.IsValid)
      {
        return BadRequest(new { Errors = _validationResultHelper.GetErrorMessages(validationResult) });
      }
      var seller = await _sellerService.GetById(id);
      if (seller == null) return NotFound(new { Message = "No se encontró el vendedor" });
      await _sellerService.Update(id, updateSellerDto);
      return Ok(new { Message = "Vendedor actualizado exitosamente" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      var sellerDto = await _sellerService.GetById(id);
      if (sellerDto == null) return NotFound(new { Message = "No se encontró el vendedor" });
      await _sellerService.Delete(sellerDto.Id);
      return Ok(new { Message = "Vendedor eliminado exitosamente" });
    }
  }
}
