namespace MonsterGym.Models;

public class Contrato
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public int MembresiaId { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaVencimiento { get; set; }
    public bool Activo { get; set; }
    public bool Congelado { get; set; }
    public Cliente? Cliente { get; set; }
    public Membresia? Membresia { get; set; }
    public ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
