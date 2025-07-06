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
    public class QualitiesController : Controller
    {
        private readonly WarehouseContext _context;

        public QualitiesController(WarehouseContext context)
        {
            _context = context;
        }

        // GET: Qualities
        public async Task<IActionResult> Index()
        {
            return View(await _context.Qualities.ToListAsync());
        }

        // GET: Qualities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quality = await _context.Qualities
                .FirstOrDefaultAsync(m => m.QualityId == id);
            if (quality == null)
            {
                return NotFound();
            }

            return View(quality);
        }

        // GET: Qualities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Qualities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QualityId,QualityName")] Quality quality)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quality);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quality);
        }

        // GET: Qualities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quality = await _context.Qualities.FindAsync(id);
            if (quality == null)
            {
                return NotFound();
            }
            return View(quality);
        }

        // POST: Qualities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QualityId,QualityName")] Quality quality)
        {
            if (id != quality.QualityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quality);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QualityExists(quality.QualityId))
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
            return View(quality);
        }

        // GET: Qualities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quality = await _context.Qualities
                .FirstOrDefaultAsync(m => m.QualityId == id);
            if (quality == null)
            {
                return NotFound();
            }

            return View(quality);
        }

        // POST: Qualities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quality = await _context.Qualities.FindAsync(id);
            if (quality != null)
            {
                _context.Qualities.Remove(quality);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QualityExists(int id)
        {
            return _context.Qualities.Any(e => e.QualityId == id);
        }
    }
}