namespace MonsterGym.Models;

public static class Rol
{
    public const string Administrador = "Administrador";
    public const string Recepcionista = "Recepcionista";
    public const string Cliente = "Cliente";

    // Roles que pueden entrar a Cliente, Membresía, Contrato y Pago
    public const string Personal = Administrador + "," + Recepcionista;

    // El rol de un empleado sale del nombre de su Cargo. Cualquier otro cargo no tiene acceso.
    public static string? DesdeCargo(string? nombreCargo)
    {
        var nombre = nombreCargo?.Trim();
        if (string.Equals(nombre, Administrador, StringComparison.OrdinalIgnoreCase)) return Administrador;
        if (string.Equals(nombre, Recepcionista, StringComparison.OrdinalIgnoreCase)) return Recepcionista;
        return null;
    }
}