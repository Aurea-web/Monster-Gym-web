using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonsterGym.Data;
using MonsterGym.Models;
using MonsterGym.Services;

namespace MonsterGym.Controllers;

[Authorize(Roles = Rol.Personal)]
public class ClienteController : Controller
{
    private readonly ApplicationDbContext _context;

    public ClienteController(ApplicationDbContext context) => _context = context;

    public IActionResult Index(string estado = "activos")
    {
        var query = _context.Clientes.AsQueryable();

        if (estado.Equals("activos", StringComparison.OrdinalIgnoreCase))
            query = query.Where(c => c.Activo);
        else if (estado.Equals("inactivos", StringComparison.OrdinalIgnoreCase))
            query = query.Where(c => !c.Activo);

        ViewBag.Estado = estado.ToLowerInvariant();
        return View(query.OrderBy(c => c.Nombre).ThenBy(c => c.Apellido).ToList());
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Cliente item)
    {
        item.Correo = item.Correo?.Trim();
        if (CorreoHelper.EnUso(_context, item.Correo))
            ModelState.AddModelError(nameof(Cliente.Correo), "Este correo ya está registrado");

        if (ModelState.IsValid)
        {
            item.Contrasena = PasswordHelper.Hash(item.Contrasena);
            _context.Clientes.Add(item);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(item);
    }

    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id == null) return NotFound();
        var item = _context.Clientes.FirstOrDefault(x => x.Id == id);
        if (item == null) return NotFound();
        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Cliente item)
    {
        if (id != item.Id) return NotFound();

        var clienteExistente = _context.Clientes.AsNoTracking().FirstOrDefault(x => x.Id == id);
        if (clienteExistente == null) return NotFound();

        // Si la contraseña queda en blanco, se conserva el hash anterior
        var cambiaContrasena = !string.IsNullOrEmpty(item.Contrasena);
        if (!cambiaContrasena)
        {
            ModelState.Remove("Contrasena");
            item.Contrasena = clienteExistente.Contrasena;
        }

        item.Correo = item.Correo?.Trim();
        if (CorreoHelper.EnUso(_context, item.Correo, excluirClienteId: id))
            ModelState.AddModelError(nameof(Cliente.Correo), "Este correo ya está registrado");

        if (ModelState.IsValid)
        {
            if (cambiaContrasena) item.Contrasena = PasswordHelper.Hash(item.Contrasena);
            _context.Clientes.Update(item);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(item);
    }
}





