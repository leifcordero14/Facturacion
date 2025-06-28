namespace Facturacion.Models
{
  public class Seller
  {
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public byte CommissionPercentage { get; set; }
    public bool IsActive { get; set; }
  }
}
