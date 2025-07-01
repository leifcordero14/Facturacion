namespace Facturacion.DTOs
{
  public class ClientDto
  {
    public int Id { get; set; }
    public required string BussinesName { get; set; }
    public required string IdentificationNumber { get; set; }
    public required string LedgerAccount { get; set; }
    public bool IsActive { get; set; }
  }
}
