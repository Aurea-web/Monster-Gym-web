using Microsoft.AspNetCore.Mvc;
using MonsterGym.Data;
using MonsterGym.Models;
using Microsoft.AspNetCore.Authorization;   // agregar arriba
namespace MonsterGym.Controllers;

[Authorize(Roles = Rol.Administrador)]
public class CargoController : Controller
{
    private readonly ApplicationDbContext _context;

    public CargoController(ApplicationDbContext context) => _context = context;

    public IActionResult Index()
    {
        var items = _context.Cargos.ToList();
        return View(items);
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Cargo item)
    {
        if (ModelState.IsValid)
        {
            _context.Cargos.Add(item);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(item);
    }

    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id == null) return NotFound();
        var item = _context.Cargos.FirstOrDefault(x => x.Id == id);
        if (item == null) return NotFound();
        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Cargo item)
    {
        if (id != item.Id) return NotFound();
        if (ModelState.IsValid)
        {
            _context.Cargos.Update(item);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(item);
    }
}
