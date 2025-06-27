namespace Facturacion.DTOs
{
  public class CreateArticleDto
  {
    public required string Description { get; set; }
    public decimal UnitPrice { get; set; }
    public bool? IsAvailable { get; set; }
  }
}
