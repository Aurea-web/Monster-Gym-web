using Microsoft.AspNetCore.Mvc;
using MonsterGym.Data;
using MonsterGym.Models;

namespace MonsterGym.Controllers;

public class EmpleadoController : Controller
{
    private readonly ApplicationDbContext _context;

    public EmpleadoController(ApplicationDbContext context) => _context = context;

    public IActionResult Index()
    {
        var items = _context.Empleados.ToList();
        return View(items);
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Empleado item)
    {
        if (id != item.Id) return NotFound();
        if (ModelState.IsValid)
        {
            _context.Empleados.Update(item);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(item);
    }
}
