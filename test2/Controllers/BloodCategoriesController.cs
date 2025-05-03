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
    public class BloodCategoriesController : Controller
    {
        private readonly test2Context _context;

        public BloodCategoriesController(test2Context context)
        {
            _context = context;
        }

        // GET: BloodCategories
        public async Task<IActionResult> Index()
        {
            var test2Context = _context.BloodCategory.Include(b => b.User);
            return View(await test2Context.ToListAsync());
        }

        // GET: BloodCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bloodCategory = await _context.BloodCategory
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bloodCategory == null)
            {
                return NotFound();
            }

            return View(bloodCategory);
        }

        // GET: BloodCategories/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: BloodCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Phone,UserId")] BloodCategory bloodCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bloodCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bloodCategory.UserId);
            return View(bloodCategory);
        }

        // GET: BloodCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bloodCategory = await _context.BloodCategory.FindAsync(id);
            if (bloodCategory == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bloodCategory.UserId);
            return View(bloodCategory);
        }

        // POST: BloodCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Phone,UserId")] BloodCategory bloodCategory)
        {
            if (id != bloodCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bloodCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BloodCategoryExists(bloodCategory.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bloodCategory.UserId);
            return View(bloodCategory);
        }

        // GET: BloodCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bloodCategory = await _context.BloodCategory
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bloodCategory == null)
            {
                return NotFound();
            }

            return View(bloodCategory);
        }

        // POST: BloodCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bloodCategory = await _context.BloodCategory.FindAsync(id);
            if (bloodCategory != null)
            {
                _context.BloodCategory.Remove(bloodCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BloodCategoryExists(int id)
        {
            return _context.BloodCategory.Any(e => e.Id == id);
        }
    }
}
