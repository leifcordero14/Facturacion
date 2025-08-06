using Facturacion.DTOs;
using Facturacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Facturacion.Controllers
{
  [Route("api")]
  [ApiController]
  public class AccountingEntriesController(
    IFilterService<BillingDto, BillingFilterDto> billingFilterService,
    IHttpClientFactory httpClientFactory) : ControllerBase
  {
    private readonly IFilterService<BillingDto, BillingFilterDto> _billingFilterService = billingFilterService;
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

    [HttpPost("accounting-entries")]
    public async Task<IActionResult> Create(
    [FromQuery] DateTime? startDate,
    [FromQuery] DateTime? endDate)
    {
      var billingsDtos = await _billingFilterService.GetByFilter(new BillingFilterDto
      {
        StartDate = startDate,
        EndDate = endDate
      });

      if (billingsDtos == null || !billingsDtos.Any()) return BadRequest(new { Message = "No hay facturas en ese rango de fechas" });

      var totalAmount = billingsDtos.Sum(b => b.Amount);
      var currentDate = DateTime.Now;

      var descripcion = $"Asiento de Facturación correspondiente al periodo {currentDate:yyyy-MM}";

      var debitEntry = new AccountingEntryDto
      {
        Descripcion = descripcion,
        Cuenta_Id = 13,
        TipoMovimiento = "DB",
        FechaAsiento = currentDate,
        MontoAsiento = totalAmount
      };

      var creditEntry = new AccountingEntryDto
      {
        Descripcion = descripcion,
        Cuenta_Id = 6,
        TipoMovimiento = "CR",
        FechaAsiento = currentDate,
        MontoAsiento = totalAmount
      };

      var httpClient = _httpClientFactory.CreateClient("AccountingAPI");
      var postUrl = "entradas-contables";

      var debitResponse = await httpClient.PostAsJsonAsync(postUrl, debitEntry);
      var creditResponse = await httpClient.PostAsJsonAsync(postUrl, creditEntry);

      if (!debitResponse.IsSuccessStatusCode || !creditResponse.IsSuccessStatusCode)
      {
        return StatusCode(500, new { Message = "Error al sincronizar asientos contables" });
      }

      return Ok(new { Message = "Asientos contables creados correctamente" });
    }
  }
}
