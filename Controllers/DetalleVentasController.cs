﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tienda.Models;

namespace Tienda.Controllers
{
    public class DetalleVentasController : Controller
    {
        private readonly TiendaContext _context;

        public DetalleVentasController(TiendaContext context)
        {
            _context = context;
        }

        // GET: DetallesVentas
        public async Task<IActionResult> Index(string filter)
        {
            var tienda259Context = _context.DetalleVentas.Include(v => v.Producto).Include(d => d.Venta).OrderBy(o => o.VentaId);
            if (!String.IsNullOrEmpty(filter))
                return View(await _context.DetalleVentas.Include(v => v.Producto).Include(d => d.Venta)
                    .Where(x => x.VentaId == Convert.ToInt32(filter)).OrderBy(o => o.VentaId).ToListAsync());

            return View(await tienda259Context.ToListAsync());
        }
        //// GET: DetalleVentas
        //public async Task<IActionResult> Index()
        //{
        //    var tiendaContext = _context.DetalleVentas.Include(d => d.Producto).Include(d => d.Venta);
        //    return View(await tiendaContext.ToListAsync());
        //}

        // GET: DetalleVentas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DetalleVentas == null)
            {
                return NotFound();
            }

            var detalleVenta = await _context.DetalleVentas
                .Include(d => d.Producto)
                .Include(d => d.Venta)
                .FirstOrDefaultAsync(m => m.DetalleVentaId == id);
            if (detalleVenta == null)
            {
                return NotFound();
            }

            return View(detalleVenta);
        }

        // GET: DetalleVentas/Create
        public IActionResult Create()
        {
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "NombreProducto");
            ViewData["VentaId"] = new SelectList(_context.Ventas, "VentaId", "Factura");
            return View();
        }

        // POST: DetalleVentas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DetalleVentaId,VentaId,ProductoId,Cantidad,Valor")] DetalleVenta detalleVenta)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(detalleVenta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "NombreProducto", detalleVenta.ProductoId);
            ViewData["VentaId"] = new SelectList(_context.Ventas, "VentaId", "Factura", detalleVenta.VentaId);
            return View(detalleVenta);
        }

        // GET: DetalleVentas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DetalleVentas == null)
            {
                return NotFound();
            }

            var detalleVenta = await _context.DetalleVentas.FindAsync(id);
            if (detalleVenta == null)
            {
                return NotFound();
            }
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "NombreProducto", detalleVenta.ProductoId);
            ViewData["VentaId"] = new SelectList(_context.Ventas, "VentaId", "Factura", detalleVenta.VentaId);
            return View(detalleVenta);
        }

        // POST: DetalleVentas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DetalleVentaId,VentaId,ProductoId,Cantidad,Valor")] DetalleVenta detalleVenta)
        {
            if (id != detalleVenta.DetalleVentaId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalleVenta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleVentaExists(detalleVenta.DetalleVentaId))
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
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "NombreProducto", detalleVenta.ProductoId);
            ViewData["VentaId"] = new SelectList(_context.Ventas, "VentaId", "Factura", detalleVenta.VentaId);
            return View(detalleVenta);
        }

        // GET: DetalleVentas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DetalleVentas == null)
            {
                return NotFound();
            }

            var detalleVenta = await _context.DetalleVentas
                .Include(d => d.Producto)
                .Include(d => d.Venta)
                .FirstOrDefaultAsync(m => m.DetalleVentaId == id);
            if (detalleVenta == null)
            {
                return NotFound();
            }

            return View(detalleVenta);
        }

        // POST: DetalleVentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DetalleVentas == null)
            {
                return Problem("Entity set 'TiendaContext.DetalleVentas'  is null.");
            }
            var detalleVenta = await _context.DetalleVentas.FindAsync(id);
            if (detalleVenta != null)
            {
                _context.DetalleVentas.Remove(detalleVenta);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleVentaExists(int id)
        {
          return (_context.DetalleVentas?.Any(e => e.DetalleVentaId == id)).GetValueOrDefault();
        }
    }
}
