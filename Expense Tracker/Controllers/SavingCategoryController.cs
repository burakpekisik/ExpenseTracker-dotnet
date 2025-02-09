using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Expense_Tracker.Models;
using Expense_Tracker.Services;

namespace Expense_Tracker.Controllers
{
    public class SavingCategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly YahooFinanceService _yahooService;

        public SavingCategoryController(ApplicationDbContext context)
        {
            _context = context;
            _yahooService = new YahooFinanceService();
        }

        // GET: SavingCategories
        public async Task<IActionResult> Index()
        {
            var categories = await _context.SavingCategories.ToListAsync();

            foreach (var category in categories)
            {
                if (!string.IsNullOrEmpty(category.Code))
                {
                    category.CurrentPrice = await _yahooService.GetCurrentPrice(category.Code);
                    if (category.CurrentPrice.HasValue)
                    {
                        category.TotalValue = category.Amount * category.CurrentPrice.Value;
                    }
                }
            }

            await _context.SaveChangesAsync();
            return View(categories);
        }

        // GET: SavingCategory/AddOrEdit
        public IActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new SavingCategory());
            }
            else
            {
                return View(_context.SavingCategories.Find(id));
            }
        }

        // POST: SavingCategory/AddOrEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("CategoryId,Title,Icon,Type,Code,Amount")] SavingCategory category)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(category.Code))
                {
                    category.CurrentPrice = await _yahooService.GetCurrentPrice(category.Code);
                    if (category.CurrentPrice.HasValue)
                    {
                        category.TotalValue = category.Amount * category.CurrentPrice.Value;
                    }
                }

                if (category.CategoryId == 0)
                {
                    _context.Add(category);
                }
                else
                {
                    var existingCategory = await _context.SavingCategories.FindAsync(category.CategoryId);
                    if (existingCategory != null)
                    {
                        existingCategory.Title = category.Title;
                        existingCategory.Icon = category.Icon;
                        existingCategory.Type = category.Type;
                        existingCategory.Code = category.Code;
                        existingCategory.Amount = category.Amount;
                        existingCategory.CurrentPrice = category.CurrentPrice;
                        existingCategory.TotalValue = category.TotalValue;
                        _context.Update(existingCategory);
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // POST: SavingCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.SavingCategories.FindAsync(id);
            if (category != null)
            {
                _context.SavingCategories.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
