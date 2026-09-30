using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonsterGym.Data;
using MonsterGym.Models;
using MonsterGym.Services;

namespace MonsterGym.Controllers;

public class CuentaController : Controller
{
    private readonly ApplicationDbContext _context;

    public CuentaController(ApplicationDbContext context) => _context = context;

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Login()
    {
        if (User.Identity?.IsAuthenticated == true)
            return RedirectAPantallaInicial();
        return View(new LoginViewModel());
    }

    [AllowAnonymous]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var correo = model.Correo.Trim();
        var (claims, rol) = Autenticar(correo, model.Contrasena);

        if (claims == null)
        {
            ModelState.Clear();
            ModelState.AddModelError(string.Empty, "Datos no válidos");
            model.Contrasena = string.Empty;
            return View(model);
        }

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity));

        return RedirectAPantallaInicial(rol);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction(nameof(Login));
    }

    [HttpGet]
    public IActionResult AccesoDenegado() => View();

    // Busca primero entre empleados (rol según su cargo) y luego entre clientes.
    private (List<Claim>? claims, string? rol) Autenticar(string correo, string contrasena)
    {
        var empleado = _context.Empleados
            .Include(e => e.Cargo)
            .FirstOrDefault(e => e.Correo == correo);

        if (empleado != null)
        {
            var rolEmpleado = Rol.DesdeCargo(empleado.Cargo?.Nombre);
            if (empleado.Activo && rolEmpleado != null && PasswordHelper.Verificar(contrasena, empleado.Contrasena))
                return (Claims(empleado.Id, $"{empleado.Nombre} {empleado.Apellido}", correo, rolEmpleado), rolEmpleado);
            return (null, null);
        }

        var cliente = _context.Clientes.FirstOrDefault(c => c.Correo == correo);
        if (cliente != null && cliente.Activo && PasswordHelper.Verificar(contrasena, cliente.Contrasena))
            return (Claims(cliente.Id, $"{cliente.Nombre} {cliente.Apellido}", correo, Rol.Cliente), Rol.Cliente);

        return (null, null);
    }

    private static List<Claim> Claims(int id, string nombre, string correo, string rol) => new()
    {
        new Claim(ClaimTypes.NameIdentifier, id.ToString()),
        new Claim(ClaimTypes.Name, nombre),
        new Claim(ClaimTypes.Email, correo),
        new Claim(ClaimTypes.Role, rol)
    };

    private IActionResult RedirectAPantallaInicial(string? rol = null)
    {
        rol ??= User.FindFirstValue(ClaimTypes.Role);
        return rol == Rol.Cliente
            ? RedirectToAction("Index", "MiCuenta")
            : RedirectToAction("Index", "Home");
    }
}