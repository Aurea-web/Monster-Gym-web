using System.ComponentModel.DataAnnotations;

namespace MonsterGym.Models;

public class Membresia
{
    public int Id { get; set; }
    [Required] public string Nombre { get; set; } = string.Empty;
    [Range(0, double.MaxValue)] public decimal Precio { get; set; }
    public int DiasVigencia { get; set; }
    public string? Beneficios { get; set; }
    public bool Activa { get; set; } = true;
    public ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();
}
