namespace Facturacion.DTOs
{
  public class UpdateArticleDto
  {
    public required string Description { get; set; }
    public decimal UnitPrice { get; set; }
    public bool IsAvailable { get; set; }
  }
}
