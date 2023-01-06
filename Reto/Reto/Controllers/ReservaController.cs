using Reto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Reto.Models.ViewModels;
using Rotativa.AspNetCore;

namespace Reto.Controllers
{
    public class ReservaController : Controller
    {
        private readonly RetoContext _context;

        //Constructor
        public ReservaController(RetoContext context)
        {
            _context = context;
        }
        //Manipulacion de la base de datos de forma asincrona
        public async Task<IActionResult> Index(string buscar)
        {
            var reservas = from reserva in _context.Reservas select reserva;

            if (!String.IsNullOrEmpty(buscar)) {
                reservas = reservas.Where(s => s.ReservaId.ToString()!.Contains(buscar));
            }
            return View(await reservas.ToListAsync());
        }

        //GET
        public IActionResult Create()
        {
            ViewData["Clientes"] = new SelectList(_context.Clientes, "ClienteId", "Nombre");
            ViewData["Motivos"] = new SelectList(_context.Motivos, "MotivoId", "Tipo");
            return View();
        }

        //POST y validacion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReservaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var resv = new Reserva()
                {
					ReservaId = Int32.Parse(model.ReservaId.ToString()),
                    ClienteId = Int32.Parse(model.ClienteId.ToString()),
					Fecha = model.Fecha,
                    Cantidad = model.Cantidad,
                    MotivoId = Int32.Parse(model.MotivoId.ToString()),
                    Observaciones = model.Observaciones,
                    Estado = model.Estado,
				};
				_context.Add(resv);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            ViewData["Clientes"] = new SelectList(_context.Clientes, "ClienteId", "Nombre");
            ViewData["Motivos"] = new SelectList(_context.Motivos, "MotivoId", "MotivoId");
            return View();
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var resv = await _context.Reservas.FindAsync(id);
            if (resv == null)
            {
                return NotFound();
            }
            ViewData["Clientes"] = new SelectList(_context.Clientes, "ClienteId", "Nombre", resv.ClienteId);
            ViewData["Motivos"] = new SelectList(_context.Motivos, "MotivoId", "Tipo", resv.MotivoId);
            return View(resv);
        }

        // POST: Clientes/Edit/5

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("ReservaId,ClienteId,Fecha,Cantidad,MotivoId,Observaciones,Estados")] Reserva resv)
        {
            _context.Update(resv);
            await _context.SaveChangesAsync();
            ViewData["Clientes"] = new SelectList(_context.Clientes, "ClienteId", "Nombre", resv.ClienteId);
            ViewData["Motivos"] = new SelectList(_context.Motivos, "MotivoId", "Tipo", resv.MotivoId);
            return RedirectToAction(nameof(Index));
        }

        // GET: Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reservas == null)
            {
                return NotFound();
            }

            var resv = await _context.Reservas
                .Include(c => c.Cliente)
                .Include(c => c.Motivo)
                .FirstOrDefaultAsync(m => m.ReservaId == id);
            if (resv == null)
            {
                return NotFound();
            }

            return View(resv);
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reservas == null)
            {
                return Problem("Entity set 'RetoContext.Clientes'  is null.");
            }
            var resv = await _context.Reservas.FindAsync(id);
            if (resv != null)
            {
                _context.Reservas.Remove(resv);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Reservas.Any(e => e.ReservaId == id);
        }
		//Imprimir
		public IActionResult Imprimir(int idreserva, [Bind("ReservaId,ClienteId,Fecha,Cantidad,MotivoId,Observaciones,Estados")] Reserva resv)
		{
			//LÓGICA HACIA BASE DE DATOS
			JoinViewModel modelo = _context.Reservas.Include(dv => dv.Cliente).Where(v => v.ReservaId == idreserva)
				.Select(v => new JoinViewModel()
				{
					ReservaId = Int32.Parse(v.ReservaId.ToString()),
					ClienteId = Int32.Parse(v.ClienteId.ToString()),
                    ClienteNombre = v.Cliente.Nombre,
                    Ciudad = v.Cliente.Ciudad.Nombre,
					Fecha = v.Fecha,
					Cantidad = v.Cantidad,
					MotivoId = v.Motivo.Tipo,
					Observaciones = v.Observaciones,
					Estado = (bool)v.Estado,
				}).FirstOrDefault();

			ViewData["Clientes"] = new SelectList(_context.Clientes, "ClienteId", "Nombre", resv.ClienteId);
			ViewData["Motivos"] = new SelectList(_context.Motivos, "MotivoId", "Tipo", resv.MotivoId);

			return new ViewAsPdf("Imprimir", modelo)
			{
				FileName = $"Reserva.pdf",
				PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
				PageSize = Rotativa.AspNetCore.Options.Size.A4
			};
		}
	}
}
