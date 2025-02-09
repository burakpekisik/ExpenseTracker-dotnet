using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Expense_Tracker.Models;

namespace Expense_Tracker.Controllers
{
    public class SavingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SavingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Saving
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Savings.Include(t => t.SavingCategory);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Saving/AddOrEdit
        public IActionResult AddOrEdit(int id = 0)
        {
            PopulateCategories();
            if (id == 0)
            {
                return View(new Saving());
            }
            else
            {
                return View(_context.Savings.Find(id));
            }
        }

        // POST: Saving/AddOrEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("SavingId,SavingCategoryId,CostPerUnit,Amount,Description,Date")] Saving saving)
        {
            if (ModelState.IsValid)
            {
                var category = await _context.SavingCategories.FindAsync(saving.SavingCategoryId);
                if (category != null)
                {
                    if (saving.SavingId == 0)
                    {
                        // New saving
                        category.Amount += saving.Amount;
                        category.TotalCost += (saving.Amount * saving.CostPerUnit);
                        _context.Add(saving);
                    }
                    else
                    {
                        // Update existing saving
                        var existingSaving = await _context.Savings.FindAsync(saving.SavingId);
                        if (existingSaving != null)
                        {
                            category.Amount = category.Amount - existingSaving.Amount + saving.Amount;
                            category.TotalCost = category.TotalCost - (existingSaving.Amount * existingSaving.CostPerUnit) + (saving.Amount * saving.CostPerUnit);
                            _context.Entry(existingSaving).CurrentValues.SetValues(saving);
                        }
                    }

                    // Calculate weighted average cost per unit
                    if (category.Amount > 0)
                    {
                        category.CostPerUnit = category.TotalCost / category.Amount;
                    }

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            PopulateCategories();
            return View(saving);
        }

        // POST: Saving/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var saving = await _context.Savings.FindAsync(id);
            if (saving != null)
            {
                var category = await _context.SavingCategories.FindAsync(saving.SavingCategoryId);
                if (category != null)
                {
                    category.Amount -= saving.Amount;
                    category.TotalCost -= (saving.Amount * saving.CostPerUnit);

                    if (category.Amount > 0)
                    {
                        category.CostPerUnit = category.TotalCost / category.Amount;
                    }
                    else
                    {
                        category.CostPerUnit = null;
                    }
                }

                _context.Savings.Remove(saving);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [NonAction]
        public void PopulateCategories()
        {
            var CategoryCollection = _context.SavingCategories.ToList();
            SavingCategory DefaultCategory = new SavingCategory() { CategoryId = 0, Title = "Choose a Category" };
            CategoryCollection.Insert(0, DefaultCategory);
            ViewBag.SavingCategories = CategoryCollection;
        }
    }
}
