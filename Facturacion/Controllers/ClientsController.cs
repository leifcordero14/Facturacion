using Facturacion.DTOs;
using Facturacion.Services;
using Facturacion.Utilities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Facturacion.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ClientsController(
    IService<ClientDto, CreateClientDto, UpdateClientDto> clientService,
    IValidationResultHelper validationResultHelper,
    IValidator<CreateClientDto> createValidator,
    IValidator<UpdateClientDto> updateValidator) : ControllerBase
  {
    private readonly IService<ClientDto, CreateClientDto, UpdateClientDto> _clientService = clientService;
    private readonly IValidationResultHelper _validationResultHelper = validationResultHelper;
    private readonly IValidator<CreateClientDto> _createValidator = createValidator;
    private readonly IValidator<UpdateClientDto> _updateValidator = updateValidator;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClientDto>>> GetAll()
    {
      var clientDtos = await _clientService.GetAll();
      return Ok(clientDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClientDto>> GetById(int id)
    {
      var clientDto = await _clientService.GetById(id);
      if (clientDto == null) return NotFound(new { Message = "No se encontró el cliente" });
      return Ok(clientDto);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateClientDto createClientDto)
    {
      var validationResult = await _createValidator.ValidateAsync(createClientDto);
      if (!validationResult.IsValid)
      {
        return BadRequest(new { Errors = _validationResultHelper.GetErrorMessages(validationResult) });
      }
      var clientDto = await _clientService.Create(createClientDto);

      return CreatedAtAction(
        nameof(GetById),
        new { id = clientDto.Id },
        new { Message = "Cliente creado exitosamente" }
       );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateClientDto updateClientDto)
    {
      var validationResult = await _updateValidator.ValidateAsync(updateClientDto);
      if (!validationResult.IsValid)
      {
        return BadRequest(new { Errors = _validationResultHelper.GetErrorMessages(validationResult) });
      }
      var clientDto = await _clientService.GetById(id);
      if (clientDto == null) return NotFound(new { Message = "No se encontró el cliente" });
      await _clientService.Update(id, updateClientDto);
      return Ok(new { Message = "Cliente actualizado exitosamente" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      var clientDto = await _clientService.GetById(id);
      if (clientDto == null) return NotFound(new { Message = "No se encontró el cliente" });
      await _clientService.Delete(clientDto.Id);
      return Ok(new { Message = "Cliente eliminado exitosamente" });
    }
  }
}
