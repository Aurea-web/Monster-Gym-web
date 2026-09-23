using System.ComponentModel.DataAnnotations;

namespace MonsterGym.Models;

public class Empleado
{
    public int Id { get; set; }
    [Required] public string Nombre { get; set; } = string.Empty;
    [Required] public string Apellido { get; set; } = string.Empty;
    public string? Documento { get; set; }
    public string? Telefono { get; set; }
    public string? Correo { get; set; }
    public int CargoId { get; set; }
    public bool Activo { get; set; }
    public Cargo? Cargo { get; set; }
}
