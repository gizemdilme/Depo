﻿using System;
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
    public class ColorsController : Controller
    {
        private readonly WarehouseContext _context;

        public ColorsController(WarehouseContext context)
        {
            _context = context;
        }

        // GET: Colors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Colors.ToListAsync());
        }

        // GET: Colors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var color = await _context.Colors
                .FirstOrDefaultAsync(m => m.ColorId == id);
            if (color == null)
            {
                return NotFound();
            }

            return View(color);
        }

        // GET: Colors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Colors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ColorId,ColorCode,ColorName")] Color color)
        {
            if (ModelState.IsValid)
            {
                _context.Add(color);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(color);
        }

        // GET: Colors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var color = await _context.Colors.FindAsync(id);
            if (color == null)
            {
                return NotFound();
            }
            return View(color);
        }

        // POST: Colors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ColorId,ColorCode,ColorName")] Color color)
        {
            if (id != color.ColorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(color);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ColorExists(color.ColorId))
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
            return View(color);
        }

        // GET: Colors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var color = await _context.Colors
                .FirstOrDefaultAsync(m => m.ColorId == id);
            if (color == null)
            {
                return NotFound();
            }

            return View(color);
        }

        // POST: Colors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var color = await _context.Colors.FindAsync(id);
            if (color != null)
            {
                _context.Colors.Remove(color);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ColorExists(int id)
        {
            return _context.Colors.Any(e => e.ColorId == id);
        }
    }
}
