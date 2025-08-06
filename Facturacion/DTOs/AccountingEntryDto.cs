namespace Facturacion.DTOs
{
  public class AccountingEntryDto
  {
    public required string Descripcion { get; set; }
    public int Cuenta_Id { get; set; }
    public int Auxiliar_Id { get; set; } = 3;
    public required string TipoMovimiento { get; set; }
    public DateTime FechaAsiento { get; set; }
    public decimal MontoAsiento { get; set; }
  }
}
