﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AzureMvc.Data;
using AzureMvc.Models;

namespace AzureMvc.Controllers
{
    public class EstadoesController : Controller
    {
        private readonly AzureMvcContext _context;

        public EstadoesController(AzureMvcContext context)
        {
            _context = context;
        }

        // GET: Estadoes
        public async Task<IActionResult> Index()
        {
            var azureMvcContext = _context.Estados.Include(e => e.Pais);
            return View(await azureMvcContext.ToListAsync());
        }

        // GET: Estadoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Estados == null)
            {
                return NotFound();
            }

            var estado = await _context.Estados
                .Include(e => e.Pais)
                .FirstOrDefaultAsync(m => m.EstadoId == id);
            if (estado == null)
            {
                return NotFound();
            }

            return View(estado);
        }

        // GET: Estadoes/Create
        public IActionResult Create()
        {
            ViewData["PaisId"] = new SelectList(_context.Paises, "PaisId", "PaisId");
            return View();
        }

        // POST: Estadoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EstadoId,Nome,ImagemEstado,PaisId")] Estado estado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaisId"] = new SelectList(_context.Paises, "PaisId", "PaisId", estado.PaisId);
            return View(estado);
        }

        // GET: Estadoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Estados == null)
            {
                return NotFound();
            }

            var estado = await _context.Estados.FindAsync(id);
            if (estado == null)
            {
                return NotFound();
            }
            ViewData["PaisId"] = new SelectList(_context.Paises, "PaisId", "PaisId", estado.PaisId);
            return View(estado);
        }

        // POST: Estadoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EstadoId,Nome,ImagemEstado,PaisId")] Estado estado)
        {
            if (id != estado.EstadoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstadoExists(estado.EstadoId))
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
            ViewData["PaisId"] = new SelectList(_context.Paises, "PaisId", "PaisId", estado.PaisId);
            return View(estado);
        }

        // GET: Estadoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Estados == null)
            {
                return NotFound();
            }

            var estado = await _context.Estados
                .Include(e => e.Pais)
                .FirstOrDefaultAsync(m => m.EstadoId == id);
            if (estado == null)
            {
                return NotFound();
            }

            return View(estado);
        }

        // POST: Estadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Estados == null)
            {
                return Problem("Entity set 'AzureMvcContext.Estados'  is null.");
            }
            var estado = await _context.Estados.FindAsync(id);
            if (estado != null)
            {
                _context.Estados.Remove(estado);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstadoExists(int id)
        {
          return _context.Estados.Any(e => e.EstadoId == id);
        }
    }
}