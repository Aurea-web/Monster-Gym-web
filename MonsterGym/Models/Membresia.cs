using System.ComponentModel.DataAnnotations;

namespace MonsterGym.Models;

public class Membresia
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre de la membresía es obligatorio")]
    [StringLength(15, ErrorMessage = "El nombre no puede superar los 15 caracteres")]
    public string Nombre { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
    public decimal Precio { get; set; }

    [Range(1, 365, ErrorMessage = "La vigencia debe estar entre 1 y 365 días")]
    public int DiasVigencia { get; set; }

    [StringLength(500, ErrorMessage = "Los beneficios no pueden superar los 500 caracteres")]
    public string? Beneficios { get; set; }

    public bool Activa { get; set; } = true;

    public ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();
}
