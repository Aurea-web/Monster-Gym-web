using System.Security.Cryptography;
using System.Text;

namespace MonsterGym.Services;

public static class PasswordHelper
{
    // SHA-256 en hexadecimal minúscula (64 caracteres, cabe en nvarchar(255))
    public static string Hash(string contrasena) =>
        Convert.ToHexStringLower(SHA256.HashData(Encoding.UTF8.GetBytes(contrasena)));

    public static bool Verificar(string contrasenaIngresada, string? hashGuardado)
    {
        if (string.IsNullOrEmpty(hashGuardado)) return false;
        var a = Encoding.UTF8.GetBytes(Hash(contrasenaIngresada));
        var b = Encoding.UTF8.GetBytes(hashGuardado.Trim().ToLowerInvariant());
        return CryptographicOperations.FixedTimeEquals(a, b);
    }
}