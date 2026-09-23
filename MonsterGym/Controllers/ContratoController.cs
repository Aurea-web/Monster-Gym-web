using Microsoft.AspNetCore.Mvc;
using MonsterGym.Data;
using MonsterGym.Models;

namespace MonsterGym.Controllers;

public class ContratoController : Controller
{
    private readonly ApplicationDbContext _context;

    public ContratoController(ApplicationDbContext context) => _context = context;

    public IActionResult Index()
    {
        var items = _context.Contratos.ToList();
        return View(items);
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Contrato item)
    {
        if (ModelState.IsValid)
        {
            _context.Contratos.Add(item);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(item);
    }

    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id == null) return NotFound();
        var item = _context.Contratos.FirstOrDefault(x => x.Id == id);
        if (item == null) return NotFound();
        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Contrato item)
    {
        if (id != item.Id) return NotFound();
        if (ModelState.IsValid)
        {
            _context.Contratos.Update(item);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(item);
    }
}
