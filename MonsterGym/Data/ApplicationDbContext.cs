using Microsoft.EntityFrameworkCore;
using MonsterGym.Models;

namespace MonsterGym.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Membresia> Membresias { get; set; }
    public DbSet<Contrato> Contratos { get; set; }
    public DbSet<Empleado> Empleados { get; set; }
    public DbSet<Cargo> Cargos { get; set; }
    public DbSet<Pago> Pagos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Contrato>()
            .HasOne(c => c.Cliente)
            .WithMany(c => c.Contratos)
            .HasForeignKey(c => c.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Contrato>()
            .HasOne(c => c.Membresia)
            .WithMany(m => m.Contratos)
            .HasForeignKey(c => c.MembresiaId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Empleado>()
            .HasOne(e => e.Cargo)
            .WithMany(c => c.Empleados)
            .HasForeignKey(e => e.CargoId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Pago>()
            .HasOne(p => p.Contrato)
            .WithMany(c => c.Pagos)
            .HasForeignKey(p => p.ContratoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
