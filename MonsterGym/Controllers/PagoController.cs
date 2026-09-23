using Microsoft.AspNetCore.Mvc;
using MonsterGym.Data;
using MonsterGym.Models;

namespace MonsterGym.Controllers;

public class PagoController : Controller
{
    private readonly ApplicationDbContext _context;

    public PagoController(ApplicationDbContext context) => _context = context;

    public IActionResult Index()
    {
        var items = _context.Pagos.ToList();
        return View(items);
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Pago item)
    {
        if (ModelState.IsValid)
        {
            _context.Pagos.Add(item);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(item);
    }

    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id == null) return NotFound();
        var item = _context.Pagos.FirstOrDefault(x => x.Id == id);
        if (item == null) return NotFound();
        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Pago item)
    {
        if (id != item.Id) return NotFound();
        if (ModelState.IsValid)
        {
            _context.Pagos.Update(item);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(item);
    }
}
