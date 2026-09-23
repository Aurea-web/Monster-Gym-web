using System.ComponentModel.DataAnnotations;

namespace MonsterGym.Models;

public class Cargo
{
    public int Id { get; set; }
    [Required] public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
