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
    public class khattabsController : Controller
    {
        private readonly test2Context _context;

        public khattabsController(test2Context context)
        {
            _context = context;
        }

        // GET: khattabs
        public async Task<IActionResult> Index()
        {
            return View(await _context.khattab.ToListAsync());
        }

        // GET: khattabs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khattab = await _context.khattab
                .FirstOrDefaultAsync(m => m.Id == id);
            if (khattab == null)
            {
                return NotFound();
            }

            return View(khattab);
        }

        // GET: khattabs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: khattabs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price")] khattab khattab)
        {
            if (ModelState.IsValid)
            {
                _context.Add(khattab);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(khattab);
        }

        // GET: khattabs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khattab = await _context.khattab.FindAsync(id);
            if (khattab == null)
            {
                return NotFound();
            }
            return View(khattab);
        }

        // POST: khattabs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price")] khattab khattab)
        {
            if (id != khattab.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(khattab);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!khattabExists(khattab.Id))
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
            return View(khattab);
        }

        // GET: khattabs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khattab = await _context.khattab
                .FirstOrDefaultAsync(m => m.Id == id);
            if (khattab == null)
            {
                return NotFound();
            }

            return View(khattab);
        }

        // POST: khattabs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var khattab = await _context.khattab.FindAsync(id);
            if (khattab != null)
            {
                _context.khattab.Remove(khattab);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool khattabExists(int id)
        {
            return _context.khattab.Any(e => e.Id == id);
        }
    }
}
