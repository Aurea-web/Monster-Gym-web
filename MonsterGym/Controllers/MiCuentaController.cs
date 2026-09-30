using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonsterGym.Data;
using MonsterGym.Models;

namespace MonsterGym.Controllers;

[Authorize(Roles = Rol.Cliente)]
public class MiCuentaController : Controller
{
    private readonly ApplicationDbContext _context;

    public MiCuentaController(ApplicationDbContext context) => _context = context;

    // Pantalla del cliente: solo muestra sus propios datos, contratos y pagos.
    public IActionResult Index()
    {
        if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var clienteId))
            return Forbid();

        var cliente = _context.Clientes
            .AsNoTracking()
            .AsSplitQuery()
            .Include(c => c.Contratos).ThenInclude(x => x.Membresia)
            .Include(c => c.Contratos).ThenInclude(x => x.Pagos)
            .FirstOrDefault(c => c.Id == clienteId);

        if (cliente == null) return NotFound();
        return View(cliente);
    }
}