using System.ComponentModel.DataAnnotations;

namespace MonsterGym.Models;

public class Pago
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El contrato es obligatorio")]
    public int ContratoId { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0")]
    public decimal Monto { get; set; }

    [Required(ErrorMessage = "El método de pago es obligatorio")]
    [StringLength(50, ErrorMessage = "El método de pago no puede superar los 50 caracteres")]
    public string MetodoPago { get; set; } = string.Empty;

    [Required(ErrorMessage = "La fecha de pago es obligatoria")]
    public DateTime FechaPago { get; set; }

    [StringLength(255, ErrorMessage = "El comprobante no puede superar los 255 caracteres")]
    public string? Comprobante { get; set; }

    public Contrato? Contrato { get; set; }
}
