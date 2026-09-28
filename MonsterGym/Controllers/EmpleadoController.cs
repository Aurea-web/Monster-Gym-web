using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonsterGym.Data;
using MonsterGym.Models;

namespace MonsterGym.Controllers;

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
    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Empleado item)
    {
        if (ModelState.IsValid)
        {
            _context.Empleados.Add(item);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(item);
    }

    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id == null) return NotFound();
        var item = _context.Empleados.FirstOrDefault(x => x.Id == id);
        if (item == null) return NotFound();
        return View(item);
    }

    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public IActionResult Edit(int id, Empleado item)
    //{
    //    if (id != item.Id) return NotFound();
    //    if (ModelState.IsValid)
    //    {
    //        _context.Empleados.Update(item);
    //        _context.SaveChanges();
    //        return RedirectToAction(nameof(Index));
    //    }
    //    return View(item);
    //}
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Empleado item)
    {
        if (id != item.Id) return NotFound();

        // 1. Buscamos el empleado original en la base de datos sin rastrearlo
        var empleadoExistente = _context.Empleados.AsNoTracking().FirstOrDefault(x => x.Id == id);

        if (empleadoExistente == null) return NotFound();

        // 2. Si el usuario dejó la contraseña en blanco, conservamos la que ya tenía
        if (string.IsNullOrEmpty(item.Contrasena))
        {
            ModelState.Remove("Contrasena");
            item.Contrasena = empleadoExistente.Contrasena;
        }

        if (ModelState.IsValid)
        {
            _context.Empleados.Update(item);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(item);
    }
}


