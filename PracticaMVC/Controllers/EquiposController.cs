using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PracticaMVC.Models;

namespace PracticaMVC.Controllers
{
    public class EquiposController : Controller
    {
        private readonly equiposDbContext _context;

        public EquiposController(equiposDbContext context)
        {
            _context = context;
        }

        // GET: Equipos
        public async Task<IActionResult> Index()
        {
              return _context.equipos != null ? 
                          View(await _context.equipos.ToListAsync()) :
                          Problem("Entity set 'equiposDbContext.equipos'  is null.");
        }

        // GET: Equipos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.equipos == null)
            {
                return NotFound();
            }

            var equipos = await _context.equipos
                .FirstOrDefaultAsync(m => m.id_equipos == id);
            if (equipos == null)
            {
                return NotFound();
            }

            return View(equipos);
        }

        // GET: Equipos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Equipos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_equipos,nombre,descripcion,tipo_equipo_id,marca_id,modelo,anio_compra,costo,vida_util,estado_equipo_id,estado")] equipos equipos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equipos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(equipos);
        }

        // GET: Equipos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.equipos == null)
            {
                return NotFound();
            }

            var equipos = await _context.equipos.FindAsync(id);
            if (equipos == null)
            {
                return NotFound();
            }
            return View(equipos);
        }

        // POST: Equipos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_equipos,nombre,descripcion,tipo_equipo_id,marca_id,modelo,anio_compra,costo,vida_util,estado_equipo_id,estado")] equipos equipos)
        {
            if (id != equipos.id_equipos)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!equiposExists(equipos.id_equipos))
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
            return View(equipos);
        }

        // GET: Equipos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.equipos == null)
            {
                return NotFound();
            }

            var equipos = await _context.equipos
                .FirstOrDefaultAsync(m => m.id_equipos == id);
            if (equipos == null)
            {
                return NotFound();
            }

            return View(equipos);
        }

        // POST: Equipos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.equipos == null)
            {
                return Problem("Entity set 'equiposDbContext.equipos'  is null.");
            }
            var equipos = await _context.equipos.FindAsync(id);
            if (equipos != null)
            {
                _context.equipos.Remove(equipos);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool equiposExists(int id)
        {
          return (_context.equipos?.Any(e => e.id_equipos == id)).GetValueOrDefault();
        }
    }
}
