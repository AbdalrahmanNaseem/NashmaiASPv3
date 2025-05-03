﻿using System;
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
    public class BloodDonationsController : Controller
    {
        private readonly test2Context _context;

        public BloodDonationsController(test2Context context)
        {
            _context = context;
        }

        // GET: BloodDonations
        public async Task<IActionResult> Index()
        {
            var test2Context = _context.BloodDonations.Include(b => b.BloodComping).Include(b => b.User);
            return View(await test2Context.ToListAsync());
        }

        // GET: BloodDonations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bloodDonation = await _context.BloodDonations
                .Include(b => b.BloodComping)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bloodDonation == null)
            {
                return NotFound();
            }

            return View(bloodDonation);
        }

        // GET: BloodDonations/Create
        public IActionResult Create()
        {
            ViewData["BloodCompingId"] = new SelectList(_context.BloodCompings, "Id", "Type");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: BloodDonations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,BloodCompingId,Amount")] BloodDonation bloodDonation)
        {
            if (ModelState.IsValid)
            {
                // الحالة الافتراضية
                bloodDonation.Status = "Pending";

                _context.Add(bloodDonation);

                // زيادة قيمة الـ Amount في جدول BloodComping
                var bloodComping = await _context.BloodCompings.FindAsync(bloodDonation.BloodCompingId);
                if (bloodComping != null)
                {
                    bloodComping.Amount += 1;
                    _context.Entry(bloodComping).Property(b => b.Amount).IsModified = true;
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["BloodCompingId"] = new SelectList(_context.BloodCompings, "Id", "Type", bloodDonation.BloodCompingId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bloodDonation.UserId);
            return View(bloodDonation);
        }
        // GET: BloodDonations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bloodDonation = await _context.BloodDonations.FindAsync(id);
            if (bloodDonation == null)
            {
                return NotFound();
            }
            ViewData["BloodCompingId"] = new SelectList(_context.BloodCompings, "Id", "Type", bloodDonation.BloodCompingId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bloodDonation.UserId);
            return View(bloodDonation);
        }

        // POST: BloodDonations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,UserId,BloodCompingId,Amount,Status")] BloodDonation bloodDonation)
        {
            if (id != bloodDonation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bloodDonation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BloodDonationExists(bloodDonation.Id))
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

            ViewData["BloodCompingId"] = new SelectList(_context.BloodCompings, "Id", "Type", bloodDonation.BloodCompingId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bloodDonation.UserId);
            return View(bloodDonation);
        }


        // GET: BloodDonations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bloodDonation = await _context.BloodDonations
                .Include(b => b.BloodComping)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bloodDonation == null)
            {
                return NotFound();
            }

            return View(bloodDonation);
        }

        // POST: BloodDonations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var bloodDonation = await _context.BloodDonations.FindAsync(id);
            if (bloodDonation != null)
            {
                _context.BloodDonations.Remove(bloodDonation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BloodDonationExists(int? id)
        {
            return _context.BloodDonations.Any(e => e.Id == id);
        }
    }
}
