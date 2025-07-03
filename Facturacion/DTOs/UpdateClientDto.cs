namespace Facturacion.DTOs
{
  public class UpdateClientDto
  {
    public required string BusinessName { get; set; }
    public required string IdentificationNumber { get; set; }
    public required string LedgerAccount { get; set; }
    public bool IsActive { get; set; }
  }
}
