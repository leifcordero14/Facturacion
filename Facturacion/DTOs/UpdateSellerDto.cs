namespace Facturacion.DTOs
{
  public class UpdateSellerDto
  {
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public byte? CommissionPercentage { get; set; }
    public bool? IsActive { get; set; }
  }
}
