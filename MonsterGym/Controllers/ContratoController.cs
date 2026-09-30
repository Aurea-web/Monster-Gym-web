using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MonsterGym.Data;
using MonsterGym.Models;
using Microsoft.AspNetCore.Authorization;   
namespace MonsterGym.Controllers;

[Authorize(Roles = Rol.Personal)]
public class ContratoController : Controller
{
    private readonly ApplicationDbContext _context;

    public ContratoController(ApplicationDbContext context) => _context = context;

    public IActionResult Index(string estado = "activos")
    {
        var query = _context.Contratos
            .Include(c => c.Cliente)
            .Include(c => c.Membresia)
            .AsQueryable();

        if (estado.Equals("activos", StringComparison.OrdinalIgnoreCase))
            query = query.Where(c => c.Activo);
        else if (estado.Equals("inactivos", StringComparison.OrdinalIgnoreCase))
            query = query.Where(c => !c.Activo);

        ViewBag.Estado = estado.ToLowerInvariant();
        return View(query.OrderBy(c => c.Id).ToList());
    }

    [HttpGet]
    public IActionResult Create()
    {
        CargarListas();
        return View();
    }

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

        CargarListas(item.ClienteId, item.MembresiaId);
        return View(item);
    }

    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id == null) return NotFound();

        var item = _context.Contratos.FirstOrDefault(x => x.Id == id);
        if (item == null) return NotFound();

        CargarListas(item.ClienteId, item.MembresiaId);
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

        CargarListas(item.ClienteId, item.MembresiaId);
        return View(item);
    }

    private void CargarListas(int? clienteId = null, int? membresiaId = null)
    {
        var clientes = _context.Clientes
            .Where(c => c.Activo || c.Id == clienteId)
            .OrderBy(c => c.Nombre)
            .ThenBy(c => c.Apellido)
            .ToList();

        var membresias = _context.Membresias
            .Where(m => m.Activa || m.Id == membresiaId)
            .OrderBy(m => m.Nombre)
            .ToList();

        ViewBag.Clientes = new SelectList(
            clientes,
            nameof(Cliente.Id),
            nameof(Cliente.Nombre),
            clienteId);

        ViewBag.Membresias = new SelectList(
            membresias,
            nameof(Membresia.Id),
            nameof(Membresia.Nombre),
            membresiaId);
    } }
