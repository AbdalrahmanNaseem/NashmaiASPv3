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
    public class MohsController : Controller
    {
        private readonly test2Context _context;

        public MohsController(test2Context context)
        {
            _context = context;
        }

        // GET: Mohs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Moh.ToListAsync());
        }

        // GET: Mohs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moh = await _context.Moh
                .FirstOrDefaultAsync(m => m.Id == id);
            if (moh == null)
            {
                return NotFound();
            }

            return View(moh);
        }

        // GET: Mohs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Mohs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Genre,Price")] Moh moh)
        {
            if (ModelState.IsValid)
            {
                _context.Add(moh);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(moh);
        }

        // GET: Mohs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moh = await _context.Moh.FindAsync(id);
            if (moh == null)
            {
                return NotFound();
            }
            return View(moh);
        }

        // POST: Mohs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Genre,Price")] Moh moh)
        {
            if (id != moh.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moh);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MohExists(moh.Id))
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
            return View(moh);
        }

        // GET: Mohs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moh = await _context.Moh
                .FirstOrDefaultAsync(m => m.Id == id);
            if (moh == null)
            {
                return NotFound();
            }

            return View(moh);
        }

        // POST: Mohs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var moh = await _context.Moh.FindAsync(id);
            if (moh != null)
            {
                _context.Moh.Remove(moh);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MohExists(int id)
        {
            return _context.Moh.Any(e => e.Id == id);
        }
    }
}
