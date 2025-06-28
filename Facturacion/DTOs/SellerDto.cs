namespace Facturacion.DTOs
{
  public class SellerDto
  {
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public byte CommissionPercentage { get; set; }
    public bool IsActive { get; set; }
  }
}
