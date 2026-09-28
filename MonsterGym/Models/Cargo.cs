using System.ComponentModel.DataAnnotations;

namespace MonsterGym.Models;

public class Cargo
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre del cargo es obligatorio")]
    [StringLength(15, ErrorMessage = "El nombre no puede superar los 15 caracteres")]
    public string Nombre { get; set; } = string.Empty;

    [StringLength(300, ErrorMessage = "La descripción no puede superar los 300 caracteres")]
    public string? Descripcion { get; set; }

    public ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
