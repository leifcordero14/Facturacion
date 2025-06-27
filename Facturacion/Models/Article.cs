namespace Facturacion.Models
{
  public class Article
  {
    public int Id { get; set; }
    public required string Description { get; set; }
    public decimal UnitPrice { get; set; }
    public bool IsAvailable { get; set; }
  }
}
