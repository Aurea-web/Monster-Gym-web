using Microsoft.AspNetCore.Mvc;
using MonsterGym.Data;
using MonsterGym.Models;
using Microsoft.AspNetCore.Authorization;   

namespace MonsterGym.Controllers;

[Authorize(Roles = Rol.Personal)]
public class MembresiaController : Controller
{
    private readonly ApplicationDbContext _context;

    public MembresiaController(ApplicationDbContext context) => _context = context;

    public IActionResult Index(string estado = "activos")
    {
        var query = _context.Membresias.AsQueryable();

        if (estado.Equals("activos", StringComparison.OrdinalIgnoreCase))
            query = query.Where(m => m.Activa);
        else if (estado.Equals("inactivos", StringComparison.OrdinalIgnoreCase))
            query = query.Where(m => !m.Activa);

        ViewBag.Estado = estado.ToLowerInvariant();
        return View(query.OrderBy(m => m.Nombre).ToList());
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Membresia item)
    {
        if (ModelState.IsValid)
        {
            _context.Membresias.Add(item);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(item);
    }

    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id == null) return NotFound();
        var item = _context.Membresias.FirstOrDefault(x => x.Id == id);
        if (item == null) return NotFound();
        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Membresia item)
    {
        if (id != item.Id) return NotFound();
        if (ModelState.IsValid)
        {
            _context.Membresias.Update(item);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(item);
    }
}
