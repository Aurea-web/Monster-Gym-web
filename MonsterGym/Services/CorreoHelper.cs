using MonsterGym.Data;

namespace MonsterGym.Services;

public static class CorreoHelper
{
    // El correo identifica al usuario en el login, así que no puede repetirse
    // entre Clientes y Empleados. Los Id excluidos son el propio registro al editar (0 = ninguno).
    public static bool EnUso(ApplicationDbContext db, string? correo, int excluirClienteId = 0, int excluirEmpleadoId = 0)
    {
        if (string.IsNullOrWhiteSpace(correo)) return false;
        var c = correo.Trim();
        return db.Clientes.Any(x => x.Correo == c && x.Id != excluirClienteId)
            || db.Empleados.Any(x => x.Correo == c && x.Id != excluirEmpleadoId);
    }
}