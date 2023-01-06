using Reto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Reto.Models.ViewModels;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Reto.Controllers
{
	public class ClienteController : Controller
	{
		private readonly RetoContext _context;

		//Constructor
		public ClienteController(RetoContext context)
		{
			_context = context;
		}

		//Manipulacion de la base de datos de forma asincrona
		public async Task<IActionResult> Index(string buscar)
		{
			var clientes = from cliente in _context.Clientes select cliente;
			if (!String.IsNullOrEmpty(buscar))
			{
				clientes = clientes.Where(s => s.ClienteId.ToString()!.Contains(buscar));
			}

			return View(await clientes.ToListAsync());
		}

        public DataSet Consultar(string strSQL)
        {
            string strconn = "Server=SEBASTIAN\\WINCC; Database=Reto; Trusted_Connection=True; TrustServerCertificate=True;";
            SqlConnection con = new SqlConnection(strconn);
            con.Open();
            SqlCommand cmd = new SqlCommand(strSQL, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            return ds;
        }

		//GET
		public IActionResult Create()
		{
			ViewData["Departamentos"] = new SelectList(_context.Departamentos, "DepartamentoId", "Nombre");
            //SelectList DepID = (SelectList)ViewData["Departamentos"];
            //DataSet CityID = Consultar("SELECT * FROM Ciudad WHERE Departamento_ID=" + DepID);
			ViewData["Ciudades"] = new SelectList(_context.Ciudads, "CiudadId", "Nombre");
			return View();
		}

		//POST y validacion
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ClienteViewModel model)
		{
			if (ModelState.IsValid)
			{
				var cli = new Cliente()
				{
					ClienteId = model.ClienteId,
					Nombre = model.Nombre,
					Teléfono = model.Telefono,
					Correo = model.Correo,
					Edad = model.Edad,
					DepartamentoId = Int32.Parse(model.DepartamentoId.ToString()),
					CiudadId = Int32.Parse(model.CiudadId.ToString()),
				};
				_context.Add(cli);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["Departamentos"] = new SelectList(_context.Departamentos, "DepartamentoId", "Nombre");
            ViewData["Ciudades"] = new SelectList(_context.Ciudads, "CiudadId", "Nombre");
			return View();
		}

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            ViewData["Ciudades"] = new SelectList(_context.Ciudads, "CiudadId", "Nombre", cliente.CiudadId);
            ViewData["Departamentos"] = new SelectList(_context.Departamentos, "DepartamentoId", "Nombre", cliente.DepartamentoId);
            return View(cliente);
        }

        // POST: Clientes/Edit/5

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("ClienteId,Nombre,Teléfono,Correo,Edad,DepartamentoId,CiudadId")] Cliente cliente)
        {

                _context.Update(cliente);
                await _context.SaveChangesAsync();
            ViewData["Ciudades"] = new SelectList(_context.Ciudads, "CiudadId", "Nombre", cliente.CiudadId);
            ViewData["Departamentos"] = new SelectList(_context.Departamentos, "DepartamentoId", "Nombre", cliente.DepartamentoId);
            return RedirectToAction(nameof(Index));


           
            //return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.Ciudad)
                .Include(c => c.Departamento)
                .FirstOrDefaultAsync(m => m.ClienteId == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clientes == null)
            {
                return Problem("Entity set 'RetoContext.Clientes'  is null.");
            }
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.ClienteId == id);
        }
    }
}
