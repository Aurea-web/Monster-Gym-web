using Microsoft.AspNetCore.Mvc;
using MonsterGym.Data;
using MonsterGym.Models;

namespace MonsterGym.Controllers;

public class ClienteController : Controller
{
    private readonly ApplicationDbContext _context;

    public ClienteController(ApplicationDbContext context) => _context = context;

    public IActionResult Index()
    {
        var items = _context.Clientes.ToList();
        return View(items);
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Cliente item)
    {
        if (id != item.Id) return NotFound();
        if (ModelState.IsValid)
        {
            _context.Clientes.Update(item);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(item);
    }
}
