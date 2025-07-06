using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Depo.Models;
using Microsoft.AspNetCore.Authorization;

namespace Depo.Controllers
{
    [Authorize]
    public class FabricsController : Controller
    {
        private readonly WarehouseContext _context;

        public FabricsController(WarehouseContext context)
        {
            _context = context;
        }

        // GET: Fabrics
        public async Task<IActionResult> Index()
        {
            var warehouseContext = _context.Fabrics.Include(f => f.Color).Include(f => f.Location).Include(f => f.Quality);
            return View(await warehouseContext.ToListAsync());
        }

        // GET: Fabrics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fabric = await _context.Fabrics
                .Include(f => f.Color)
                .Include(f => f.Location)
                .Include(f => f.Quality)
                .FirstOrDefaultAsync(m => m.FabricId == id);
            if (fabric == null)
            {
                return NotFound();
            }

            return View(fabric);
        }

        // GET: Fabrics/Create
        public IActionResult Create()
        {
            ViewData["ColorId"] = new SelectList(_context.Colors, "ColorId", "ColorName");
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Section");
            ViewData["QualityId"] = new SelectList(_context.Qualities, "QualityId", "QualityName");
            return View();
        }

        // POST: Fabrics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FabricId,Name,Tone,QualityId,ColorId,LocationId")] Fabric fabric)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fabric);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ColorId"] = new SelectList(_context.Colors, "ColorId", "ColorName", fabric.ColorId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Section", fabric.LocationId);
            ViewData["QualityId"] = new SelectList(_context.Qualities, "QualityId", "QualityName", fabric.QualityId);
            return View(fabric);
        }

        // GET: Fabrics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fabric = await _context.Fabrics.FindAsync(id);
            if (fabric == null)
            {
                return NotFound();
            }
            ViewData["ColorId"] = new SelectList(_context.Colors, "ColorId", "ColorName", fabric.ColorId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Section", fabric.LocationId);
            ViewData["QualityId"] = new SelectList(_context.Qualities, "QualityId", "QualityName", fabric.QualityId);
            return View(fabric);
        }

        // POST: Fabrics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FabricId,Name,Tone,QualityId,ColorId,LocationId")] Fabric fabric)
        {
            if (id != fabric.FabricId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fabric);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FabricExists(fabric.FabricId))
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
            ViewData["ColorId"] = new SelectList(_context.Colors, "ColorId", "ColorId", fabric.ColorId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Section", fabric.LocationId);
            ViewData["QualityId"] = new SelectList(_context.Qualities, "QualityId", "QualityName", fabric.QualityId);
            return View(fabric);
        }

        // GET: Fabrics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fabric = await _context.Fabrics
                .Include(f => f.Color)
                .Include(f => f.Location)
                .Include(f => f.Quality)
                .FirstOrDefaultAsync(m => m.FabricId == id);
            if (fabric == null)
            {
                return NotFound();
            }

            return View(fabric);
        }

        // POST: Fabrics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fabric = await _context.Fabrics.FindAsync(id);
            if (fabric != null)
            {
                _context.Fabrics.Remove(fabric);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FabricExists(int id)
        {
            return _context.Fabrics.Any(e => e.FabricId == id);
        }
    }
}
