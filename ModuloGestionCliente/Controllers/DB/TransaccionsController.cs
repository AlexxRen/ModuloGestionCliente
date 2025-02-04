using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ModuloGestionCliente.Models.DB;

namespace ModuloGestionCliente.Controllers.DB
{
    public class TransaccionsController : Controller
    {
        private readonly ProyectContext _context;

        public TransaccionsController(ProyectContext context)
        {
            _context = context;
        }

        // GET: Transaccions
        public async Task<IActionResult> Index()
        {
            var proyectContext = _context.Transaccions.Include(t => t.IdClienteNavigation);
            return View(await proyectContext.ToListAsync());
        }

        // GET: Transaccions/Create
        public IActionResult Create()
        {
            var transaccion = new Transaccion
            {
                Estado = "Pendiente",
                FechaTransaccion = DateTime.Now
            };
            return View(transaccion);
        }

        // POST: Transaccions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(decimal CuentaOrigen, decimal CuentaDestino, decimal Monto)
        {
            try
            {
                var clienteOrigen = await _context.Clientes.FirstOrDefaultAsync(c => c.Cuenta == CuentaOrigen);
                var clienteDestino = await _context.Clientes.FirstOrDefaultAsync(c => c.Cuenta == CuentaDestino);

                if (clienteOrigen == null || clienteDestino == null)
                {
                    ModelState.AddModelError("", "Una de las cuentas no existe.");
                    return View();
                }

                if (clienteOrigen.TotalCuenta < Monto)
                {
                    ModelState.AddModelError("", "Saldo insuficiente.");
                    return View();
                }

                var transaccion = new Transaccion
                {
                    FechaTransaccion = DateTime.Now,
                    Monto = Monto,
                    Estado = "Pendiente",
                    IdOrigenCli = clienteOrigen.IdCliente,
                    IdCliente = clienteDestino.IdCliente
                };

                _context.Transaccions.Add(transaccion);

                clienteOrigen.TotalCuenta -= Monto;
                clienteDestino.TotalCuenta += Monto;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al guardar cambios: " + ex.InnerException?.Message);
                return View();
            }
        }

    }
}
