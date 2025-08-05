namespace Facturacion.DTOs
{
  public class BillingDto
  {
    public int Id { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public required string Comment { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal Amount => UnitPrice * Quantity;
    public int? AccountingEntryId { get; set; }
    public required ArticleDto Article { get; set; }
    public required ClientDto Client { get; set; }
    public required SellerDto Seller { get; set; }
  }
}
