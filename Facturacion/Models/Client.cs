namespace Facturacion.Models
{
  public class Client
  {
    public int Id { get; set; }
    public required string BusinessName { get; set; }
    public required string IdentificationNumber { get; set; }
    public required string LedgerAccount { get; set; }
    public bool IsActive { get; set; }
  }
}
