namespace Facturacion.Models
{
  public class Billing
  {
    public int Id { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public required string Comment { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int ArticleId { get; set; }
    public required Article Article { get; set; }
    public int ClientId { get; set; }
    public required Client Client { get; set; }
    public int SellerId { get; set; }
    public required Seller Seller { get; set; }
  }
}
