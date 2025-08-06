namespace Facturacion.DTOs
{
  public class CreateBillingDto
  {
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public int ArticleId { get; set; }
    public int ClientId { get; set; }
    public int SellerId { get; set; }
  }
}
