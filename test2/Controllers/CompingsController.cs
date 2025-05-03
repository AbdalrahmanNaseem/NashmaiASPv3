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
    public class CompingsController : Controller
    {
        private readonly test2Context _context;

        public CompingsController(test2Context context)
        {
            _context = context;
        }

        // GET: Compings
        public async Task<IActionResult> Index()
        {
            var test2Context = _context.Compings.Include(c => c.Category).Include(c => c.User);
            return View(await test2Context.ToListAsync());
        }

        // GET: Compings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comping = await _context.Compings
                .Include(c => c.Category)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comping == null)
            {
                return NotFound();
            }

            return View(comping);
        }

        // GET: Compings/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Compings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,ShortDesc,LongDesc,CategoryId,UserId,Goal,Amount")] Comping comping)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.UserId = new SelectList(_context.Users, "Id", "Username", comping.UserId);
                ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Title", comping.CategoryId);
                return View(comping);
            }

            _context.Add(comping);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Compings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var comping = await _context.Compings.FindAsync(id);
            if (comping == null)
                return NotFound();

            ViewBag.UserId = new SelectList(_context.Users, "Id", "Username", comping.UserId);
            ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Title", comping.CategoryId);
            return View(comping);
        }

        // POST: Compings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ShortDesc,LongDesc,CategoryId,UserId,Goal,Amount")] Comping comping)
        {
            if (id != comping.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comping);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Compings.Any(e => e.Id == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.UserId = new SelectList(_context.Users, "Id", "Username", comping.UserId);
            ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Title", comping.CategoryId);
            return View(comping);
        }

        // GET: Compings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comping = await _context.Compings
                .Include(c => c.Category)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comping == null)
            {
                return NotFound();
            }

            return View(comping);
        }

        // POST: Compings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comping = await _context.Compings.FindAsync(id);
            if (comping != null)
            {
                _context.Compings.Remove(comping);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompingExists(int id)
        {
            return _context.Compings.Any(e => e.Id == id);
        }
    }
}
