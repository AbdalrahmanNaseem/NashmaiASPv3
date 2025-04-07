using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using test2.Data;
using test2.Models;

namespace test2.Controllers
{
    public class ParsonsController : Controller
    {
        private readonly test2Context _context;

        public ParsonsController(test2Context context)
        {
            _context = context;
        }

        // GET: Parsons
        public async Task<IActionResult> Index()
        {
            return View(await _context.Parson.ToListAsync());
        }

        // GET: Parsons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parson = await _context.Parson
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parson == null)
            {
                return NotFound();
            }

            return View(parson);
        }

        // GET: Parsons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Parsons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price")] Parson parson)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parson);
        }

        // GET: Parsons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parson = await _context.Parson.FindAsync(id);
            if (parson == null)
            {
                return NotFound();
            }
            return View(parson);
        }

        // POST: Parsons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price")] Parson parson)
        {
            if (id != parson.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParsonExists(parson.Id))
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
            return View(parson);
        }

        // GET: Parsons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parson = await _context.Parson
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parson == null)
            {
                return NotFound();
            }

            return View(parson);
        }

        // POST: Parsons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parson = await _context.Parson.FindAsync(id);
            if (parson != null)
            {
                _context.Parson.Remove(parson);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParsonExists(int id)
        {
            return _context.Parson.Any(e => e.Id == id);
        }
    }
}
