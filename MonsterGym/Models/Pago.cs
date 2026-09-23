using System.ComponentModel.DataAnnotations;

namespace MonsterGym.Models;

public class Pago
{
    public int Id { get; set; }
    public int ContratoId { get; set; }
    [Range(0, double.MaxValue)] public decimal Monto { get; set; }
    [Required] public string MetodoPago { get; set; } = string.Empty;
    public DateTime FechaPago { get; set; }
    public string? Comprobante { get; set; }
    public Contrato? Contrato { get; set; }
}
