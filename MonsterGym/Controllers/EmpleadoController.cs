using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MonsterGym.Data;
using MonsterGym.Models;
using MonsterGym.Services;

namespace MonsterGym.Controllers;

[Authorize(Roles = Rol.Administrador)]
public class EmpleadoController : Controller
{
    private readonly ApplicationDbContext _context;

    public EmpleadoController(ApplicationDbContext context) => _context = context;

    public IActionResult Index(string estado = "activos")
    {
        var query = _context.Empleados
            .Include(e => e.Cargo)
            .AsQueryable();

        if (estado.Equals("activos", StringComparison.OrdinalIgnoreCase))
            query = query.Where(e => e.Activo);
        else if (estado.Equals("inactivos", StringComparison.OrdinalIgnoreCase))
            query = query.Where(e => !e.Activo);

        ViewBag.Estado = estado.ToLowerInvariant();
        return View(query.OrderBy(e => e.Nombre).ThenBy(e => e.Apellido).ToList());
    }

    [HttpGet]
    public IActionResult Create()
    {
        CargarCargos();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Empleado item)
    {
        item.Correo = item.Correo?.Trim();
        if (CorreoHelper.EnUso(_context, item.Correo))
            ModelState.AddModelError(nameof(Empleado.Correo), "Este correo ya está registrado");

        if (ModelState.IsValid)
        {
            item.Contrasena = PasswordHelper.Hash(item.Contrasena);
            _context.Empleados.Add(item);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        CargarCargos(item.CargoId);
        return View(item);
    }

    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id == null) return NotFound();
        var item = _context.Empleados.FirstOrDefault(x => x.Id == id);
        if (item == null) return NotFound();
        CargarCargos(item.CargoId);
        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Empleado item)
    {
        if (id != item.Id) return NotFound();

        var empleadoExistente = _context.Empleados.AsNoTracking().FirstOrDefault(x => x.Id == id);
        if (empleadoExistente == null) return NotFound();

        // Si la contraseña queda en blanco, se conserva el hash anterior
        var cambiaContrasena = !string.IsNullOrEmpty(item.Contrasena);
        if (!cambiaContrasena)
        {
            ModelState.Remove("Contrasena");
            item.Contrasena = empleadoExistente.Contrasena;
        }

        item.Correo = item.Correo?.Trim();
        if (CorreoHelper.EnUso(_context, item.Correo, excluirEmpleadoId: id))
            ModelState.AddModelError(nameof(Empleado.Correo), "Este correo ya está registrado");

        if (ModelState.IsValid)
        {
            if (cambiaContrasena) item.Contrasena = PasswordHelper.Hash(item.Contrasena);
            _context.Empleados.Update(item);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        CargarCargos(item.CargoId);
        return View(item);
    }

    private void CargarCargos(int? cargoId = null)
    {
        ViewBag.Cargos = new SelectList(
            _context.Cargos.OrderBy(c => c.Nombre).ToList(),
            nameof(Cargo.Id),
            nameof(Cargo.Nombre),
            cargoId);
    }
}