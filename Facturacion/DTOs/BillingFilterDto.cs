namespace Facturacion.DTOs
{
  public class BillingFilterDto
  {
    public int? ArticleId { get; set; }
    public int? SellerId { get; set; }
    public int? ClientId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
  }
}
