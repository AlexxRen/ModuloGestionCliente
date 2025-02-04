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

        // GET: Transaccions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaccion = await _context.Transaccions
                .Include(t => t.IdClienteNavigation)
                .FirstOrDefaultAsync(m => m.IdTransaccion == id);
            if (transaccion == null)
            {
                return NotFound();
            }

            return View(transaccion);
        }

        // GET: Transaccions/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "Contrasea");
            return View();
        }

        // POST: Transaccions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTransaccion,FechaTransaccion,Monto,Estado,IdOrigenCli,IdCliente")] Transaccion transaccion)
        {
            // Validar existencia de clientes
            var clienteDestino = await _context.Clientes.FindAsync(transaccion.IdCliente);
            var clienteOrigen = await _context.Clientes.FindAsync(transaccion.IdOrigenCli);

            if (clienteDestino == null || clienteOrigen == null)
            {
                ModelState.AddModelError(string.Empty, "Uno o ambos clientes no existen");
            }
            else if (clienteDestino.TotalCuenta < transaccion.Monto)
            {
                ModelState.AddModelError("Monto", "Saldo insuficiente");
            }

            if (ModelState.IsValid)
            {
                // Actualizar saldos
                clienteDestino.TotalCuenta -= transaccion.Monto;
                clienteOrigen.TotalCuenta += transaccion.Monto; // Si es una transferencia

                _context.Add(transaccion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "Nombres", transaccion.IdCliente);
            return View(transaccion);

        }


        // GET: Transaccions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaccion = await _context.Transaccions.FindAsync(id);
            if (transaccion == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "Contrasea", transaccion.IdCliente);
            return View(transaccion);
        }

        // POST: Transaccions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTransaccion,FechaTransaccion,Monto,Estado,IdOrigenCli,IdCliente")] Transaccion transaccion)
        {
            if (id != transaccion.IdTransaccion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaccion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransaccionExists(transaccion.IdTransaccion))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "Contrasea", transaccion.IdCliente);
            return View(transaccion);
        }

        // GET: Transaccions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaccion = await _context.Transaccions
                .Include(t => t.IdClienteNavigation)
                .FirstOrDefaultAsync(m => m.IdTransaccion == id);
            if (transaccion == null)
            {
                return NotFound();
            }

            return View(transaccion);
        }

        // POST: Transaccions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaccion = await _context.Transaccions.FindAsync(id);
            if (transaccion != null)
            {
                _context.Transaccions.Remove(transaccion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransaccionExists(int id)
        {
            return _context.Transaccions.Any(e => e.IdTransaccion == id);
        }
    }
}
