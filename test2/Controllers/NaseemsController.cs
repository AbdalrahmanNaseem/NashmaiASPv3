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
    public class NaseemsController : Controller
    {
        private readonly test2Context _context;

        public NaseemsController(test2Context context)
        {
            _context = context;
        }

        // GET: Naseems
        public async Task<IActionResult> Index()
        {
            return View(await _context.Naseem.ToListAsync());
        }

        // GET: Naseems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var naseem = await _context.Naseem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (naseem == null)
            {
                return NotFound();
            }

            return View(naseem);
        }

        // GET: Naseems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Naseems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price")] Naseem naseem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(naseem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(naseem);
        }





        // GET: Naseems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var naseem = await _context.Naseem.FindAsync(id);
            if (naseem == null)
            {
                return NotFound();
            }
            return View(naseem);
        }

        // POST: Naseems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price")] Naseem naseem)
        {
            if (id != naseem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(naseem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NaseemExists(naseem.Id))
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
            return View(naseem);
        }



        // GET: Naseems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var naseem = await _context.Naseem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (naseem == null)
            {
                return NotFound();
            }

            return View(naseem);
        }

        // POST: Naseems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var naseem = await _context.Naseem.FindAsync(id);
            if (naseem != null)
            {
                _context.Naseem.Remove(naseem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NaseemExists(int id)
        {
            return _context.Naseem.Any(e => e.Id == id);
        }
    }
}
