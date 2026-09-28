using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonsterGym.Data;
using MonsterGym.Models;

namespace MonsterGym.Controllers;

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
        if (ModelState.IsValid)
        {
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

    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public IActionResult Edit(int id, Cliente item)
    //{
    //    if (id != item.Id) return NotFound();
    //    if (ModelState.IsValid)
    //    {
    //        _context.Clientes.Update(item);
    //        _context.SaveChanges();
    //        return RedirectToAction(nameof(Index));
    //    }
    //    return View(item);
    //}


    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Cliente item)
    {
        if (id != item.Id) return NotFound();

        // 1. Buscamos el registro original en la base de datos sin rastrearlo
        var clienteExistente = _context.Clientes.AsNoTracking().FirstOrDefault(x => x.Id == id);

        if (clienteExistente == null) return NotFound();

        // 2. Si el usuario dejó la contraseña en blanco, conservamos la anterior
        if (string.IsNullOrEmpty(item.Contrasena))
        {
            ModelState.Remove("Contrasena"); // Eliminamos el error de validación
            item.Contrasena = clienteExistente.Contrasena; // Mantenemos la contraseña actual
        }

        if (ModelState.IsValid)
        {
            _context.Clientes.Update(item);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(item);
    }
}







