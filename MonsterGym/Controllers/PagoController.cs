using Microsoft.AspNetCore.Authorization;   // agregar arriba
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MonsterGym.Data;
using MonsterGym.Models;
namespace MonsterGym.Controllers;

[Authorize(Roles = Rol.Personal)]
public class PagoController : Controller
{
    private readonly ApplicationDbContext _context;

    public PagoController(ApplicationDbContext context) => _context = context;


    [HttpGet]
    public IActionResult Index()
    {
        var items = _context.Pagos.Include(x=> x.Contrato).ThenInclude(x => x.Cliente).ToList();
        return View(items);
    }

    [HttpGet]
    public IActionResult Create()
    {
        CargarListas();
        return View();
    }

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

        CargarListas(item.ContratoId);
        return View(item);
    }

    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id == null) return NotFound();

        var item = _context.Pagos.FirstOrDefault(x => x.Id == id);
        if (item == null) return NotFound();

        CargarListas(item.ContratoId);
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

        CargarListas(item.ContratoId);
        return View(item);
    }

    private void CargarListas(int? clienteId = null)
    {

        var pagos = _context.Contratos 
            .Include(x => x.Cliente)
            .Include(x => x.Membresia)
            .Where(c => c.Cliente.Activo || c.Cliente.Id == clienteId)
            .OrderBy(c => c.Cliente.Nombre)
            .ThenBy(c => c.Cliente.Apellido)
            .ToList();

        var data = new SelectList(pagos.Select(p=>new { Value = p.Id, Text = p.Cliente.Nombre + " " + p.Cliente.Apellido }), "Value","Text");

        ViewBag.Contratos = data;
    }
}