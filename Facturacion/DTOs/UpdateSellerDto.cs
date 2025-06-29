namespace Facturacion.DTOs
{
  public class UpdateSellerDto
  {
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public int CommissionPercentage { get; set; }
    public bool IsActive { get; set; }
  }
}
