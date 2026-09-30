using MonsterGym.Models;
using MonsterGym.Services;

namespace MonsterGym.Data;

public static class DbInitializer
{
    // Crea los cargos base y, si la configuración lo indica, el primer administrador.
    public static void Seed(ApplicationDbContext db, IConfiguration config)
    {
        var cargoAdmin = db.Cargos.FirstOrDefault(c => c.Nombre == Rol.Administrador);
        if (cargoAdmin == null)
        {
            cargoAdmin = new Cargo { Nombre = Rol.Administrador, Descripcion = "Acceso total al sistema" };
            db.Cargos.Add(cargoAdmin);
        }

        if (!db.Cargos.Any(c => c.Nombre == Rol.Recepcionista))
            db.Cargos.Add(new Cargo { Nombre = Rol.Recepcionista, Descripcion = "Clientes, membresías, contratos y pagos" });

        db.SaveChanges();

        var correo = config["Seed:AdminCorreo"];
        var clave = config["Seed:AdminContrasena"];
        if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(clave)) return;

        if (db.Empleados.Any(e => e.CargoId == cargoAdmin.Id)) return;
        if (CorreoHelper.EnUso(db, correo)) return;

        db.Empleados.Add(new Empleado
        {
            Nombre = "Administrador",
            Apellido = "Sistema",
            Correo = correo.Trim(),
            Contrasena = PasswordHelper.Hash(clave),
            CargoId = cargoAdmin.Id,
            Activo = true
        });
        db.SaveChanges();
    }
}