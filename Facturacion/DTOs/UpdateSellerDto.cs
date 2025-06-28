namespace Facturacion.DTOs
{
  public class UpdateSellerDto
  {
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int? CommissionPercentage { get; set; }
    public bool? IsActive { get; set; }
  }
}
