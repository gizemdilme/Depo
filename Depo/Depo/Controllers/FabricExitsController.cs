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
    public class FabricExitsController : Controller
    {
        private readonly WarehouseContext _context;

        public FabricExitsController(WarehouseContext context)
        {
            _context = context;
        }

        // GET: FabricExits
        public async Task<IActionResult> Index()
        {
            var warehouseContext = _context.FabricExits.Include(f => f.Fabric).Include(f => f.Location);
            return View(await warehouseContext.ToListAsync());
        }

        // GET: FabricExits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fabricExit = await _context.FabricExits
                .Include(f => f.Fabric)
                .Include(f => f.Location)
                .FirstOrDefaultAsync(m => m.ExitId == id);
            if (fabricExit == null)
            {
                return NotFound();
            }

            return View(fabricExit);
        }

        // GET: FabricExits/Create
        public IActionResult Create()
        {
            ViewData["FabricId"] = new SelectList(_context.Fabrics, "FabricId", "FabricId");
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId");
            return View();
        }

        // POST: FabricExits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExitId,FabricId,Date,Quantity,LocationId")] FabricExit fabricExit)
        {
            if (fabricExit != null)
            {

                _context.Add(fabricExit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // ModelState hatalarını loglayın
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        Console.WriteLine($"Property: {state.Key}, Error: {error.ErrorMessage}");
                    }
                }
            }
            

            ViewData["FabricId"] = new SelectList(_context.Fabrics, "FabricId", "FabricId", fabricExit.FabricId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", fabricExit.LocationId);
            return View(fabricExit);
        }


        // GET: FabricExits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fabricExit = await _context.FabricExits.FindAsync(id);
            if (fabricExit == null)
            {
                return NotFound();
            }
            ViewData["FabricId"] = new SelectList(_context.Fabrics, "FabricId", "FabricId", fabricExit.FabricId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", fabricExit.LocationId);
            return View(fabricExit);
        }

        // POST: FabricExits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExitId,FabricId,Date,Quantity,LocationId")] FabricExit fabricExit)
        {
            if (id != fabricExit.ExitId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fabricExit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FabricExitExists(fabricExit.ExitId))
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
            ViewData["FabricId"] = new SelectList(_context.Fabrics, "FabricId", "FabricId", fabricExit.FabricId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", fabricExit.LocationId);
            return View(fabricExit);
        }

        // GET: FabricExits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fabricExit = await _context.FabricExits
                .Include(f => f.Fabric)
                .Include(f => f.Location)
                .FirstOrDefaultAsync(m => m.ExitId == id);
            if (fabricExit == null)
            {
                return NotFound();
            }

            return View(fabricExit);
        }

        // POST: FabricExits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fabricExit = await _context.FabricExits.FindAsync(id);
            if (fabricExit != null)
            {
                _context.FabricExits.Remove(fabricExit);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FabricExitExists(int id)
        {
            return _context.FabricExits.Any(e => e.ExitId == id);
        }
    }
}
