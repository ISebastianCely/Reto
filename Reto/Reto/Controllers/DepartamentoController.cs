using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reto.Models;

namespace Reto.Controllers
{
    public class DepartamentoController : Controller
    {
        private readonly RetoContext _context;

        //Constructor
        public DepartamentoController(RetoContext context)
        {
            _context = context;
        }
        //Manipulacion de la base de datos de forma asincrona
        public async Task<IActionResult> Index()
        {
            return View(await _context.Departamentos.ToListAsync());
        }
    }
}
