using Microsoft.AspNetCore.Mvc;
using MonsterGym.Data;
using MonsterGym.Models;
using System.Diagnostics;

namespace MonsterGym.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context) => _context = context;

    public IActionResult Index()
    {
        ViewBag.Clientes = _context.Clientes.ToList();
        ViewBag.Membresias = _context.Membresias.ToList();
        ViewBag.Contratos = _context.Contratos.ToList();
        ViewBag.Empleados = _context.Empleados.ToList();
        ViewBag.Cargos = _context.Cargos.ToList();
        ViewBag.Pagos = _context.Pagos.ToList();
        return View();
    }

    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
