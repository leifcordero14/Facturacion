using Facturacion.DTOs;
using Facturacion.Services;
using Facturacion.Utilities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Facturacion.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class BillingsController(
    ICreateReadService<BillingDto, CreateBillingDto> billingService,
    IFilterService<BillingDto, BillingFilterDto> billingFilterService,
    IValidationResultHelper validationResultHelper,
    IValidator<CreateBillingDto> createValidator) : ControllerBase
  {
    private readonly ICreateReadService<BillingDto, CreateBillingDto> _billingService = billingService;
    private readonly IFilterService<BillingDto, BillingFilterDto> _billingFilterService = billingFilterService;
    private readonly IValidationResultHelper _validationResultHelper = validationResultHelper;
    private readonly IValidator<CreateBillingDto> _createValidator = createValidator;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BillingDto>>> GetAll()
    {
      var billingDtos = await _billingService.GetAll();
      return Ok(billingDtos);
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<BillingDto>>> GetByFilter(
      [FromQuery] int? articleId,
      [FromQuery] int? sellerId,
      [FromQuery] int? clientId,
      [FromQuery] DateTime? startDate,
      [FromQuery] DateTime? endDate)
    {
      var filter = new BillingFilterDto
      {
        ArticleId = articleId,
        SellerId = sellerId,
        ClientId = clientId,
        StartDate = startDate,
        EndDate = endDate
      };
      var billingDtos = await _billingFilterService.GetByFilter(filter);
      return Ok(billingDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BillingDto>> GetById(int id)
    {
      var billingDto = await _billingService.GetById(id);
      if (billingDto == null) return NotFound(new { Message = "No se encontró la factura" });
      return Ok(billingDto);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBillingDto createBillingDto)
    {
      var validationResult = await _createValidator.ValidateAsync(createBillingDto);
      if (!validationResult.IsValid)
      {
        return BadRequest(new { Errors = _validationResultHelper.GetErrorMessages(validationResult) });
      }
      var billingDto = await _billingService.Create(createBillingDto);

      return CreatedAtAction(
        nameof(GetById),
        new { id = billingDto.Id },
        new { Message = "Factura creada exitosamente" }
       );
    }
  }
}
