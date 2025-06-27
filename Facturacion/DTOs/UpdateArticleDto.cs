namespace Facturacion.DTOs
{
  public class UpdateArticleDto
  {
    public string? Description { get; set; }
    public decimal? UnitPrice { get; set; }
    public bool? IsAvailable { get; set; }
  }
}
