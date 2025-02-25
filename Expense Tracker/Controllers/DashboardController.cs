﻿using Expense_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Expense_Tracker.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> Index()
        {
            DateTime StartDate = DateTime.Today.AddDays(-6);
            DateTime EndDate = DateTime.Today;

            List<Transaction> SelectedTransactions = await _context.Transactions
                .Include(x => x.Category)
                .Where(y => y.Date >= StartDate && y.Date <= EndDate)
                .ToListAsync();

            // Total Income
            int TotalIncome = SelectedTransactions
                .Where(i => i.Category.Type == "Income")
                .Sum(j => j.Amount);
            ViewBag.TotalIncome = TotalIncome.ToString("C0");

            // Total Expense
            int TotalExpense = SelectedTransactions
                .Where(i => i.Category.Type == "Expense")
                .Sum(j => j.Amount);
            ViewBag.TotalExpense = TotalExpense.ToString("C0");

            // Balance
            int Balance = TotalIncome - TotalExpense;
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            culture.NumberFormat.CurrencyNegativePattern = 1;
            ViewBag.Balance = String.Format(culture, "{0:C0}", Balance);

            // Doughnut Chart - Expense By Category
            ViewBag.DoughnutChartData = SelectedTransactions
                .Where(i => i.Category.Type == "Expense")
                .GroupBy(j => j.Category.CategoryId)
                .Select(k => new
                {
                    categoryTitleWithIcon = k.First().Category.Icon + " " + k.First().Category.Title,
                    amount = k.Sum(j => j.Amount),
                    formattedAmount = k.Sum(j => j.Amount).ToString("C0"),
                })
                .OrderByDescending(l => l.amount)
                .ToList();

            // Spline Chart - Income vs Expense
            // Income
            List<SplineChartData> IncomeSummary = SelectedTransactions
                .Where(i => i.Category.Type == "Income")
                .GroupBy(j => j.Date)
                .Select(k => new SplineChartData()
                {
                    day = k.First().Date.ToString("dd-MMM"),
                    income = k.Sum(l => l.Amount),
                })
                .ToList();

            // Expense
            List<SplineChartData> ExpenseSummary = SelectedTransactions
                .Where(i => i.Category.Type == "Expense")
                .GroupBy(j => j.Date)
                .Select(k => new SplineChartData()
                {
                    day = k.First().Date.ToString("dd-MMM"),
                    expense = k.Sum(l => l.Amount),
                })
                .ToList();

            // Combine Income & Expense
            string[] LastSevenDays = Enumerable.Range(0, 7)
                .Select(i => StartDate.AddDays(i).ToString("dd-MMM"))
                .ToArray();

            ViewBag.SplineChartData = from day in LastSevenDays
                                      join income in IncomeSummary on day equals income.day into dayIncomeJoined
                                      from income in dayIncomeJoined.DefaultIfEmpty()
                                      join expense in ExpenseSummary on day equals expense.day into expenseJoined
                                      from expense in expenseJoined.DefaultIfEmpty()
                                      select new
                                      {
                                          day = day,
                                          income = income == null ? 0 : income.income,
                                          expense = expense == null ? 0 : expense.expense,
                                      };

            // Recent Transactions
            ViewBag.RecentTransactions = await _context.Transactions
                .Include(i => i.Category)
                .OrderByDescending(j => j.Category)
                .Take(5)
                .ToListAsync();

            // Saving Categories Chart Data
            var savingCategories = await _context.SavingCategories.ToListAsync();
            var savings = await _context.Savings
                .Include(x => x.SavingCategory)
                .Where(s => s.Date >= StartDate && s.Date <= EndDate)
                .ToListAsync();

            var savingsGroupedByDate = savings
                .GroupBy(s => s.Date.ToString("dd-MMM"))
                .ToDictionary(g => g.Key, g => new {
                    totalValue = Math.Round(g.Sum(s => s.SavingCategory.TotalValue ?? 0), 2),
                    totalCost = Math.Round(g.Sum(s => s.Total), 2)
                });

            ViewBag.SavingSplineChartData = LastSevenDays.Select(day => new
            {
                date = day,
                totalValue = savingsGroupedByDate.ContainsKey(day) ? savingsGroupedByDate[day].totalValue : 0,
                totalCost = savingsGroupedByDate.ContainsKey(day) ? savingsGroupedByDate[day].totalCost : 0
            }).ToList();

            // Savings Distribution Chart Data
            var savingsDistribution = await _context.Savings
                .Include(x => x.SavingCategory)
                .ToListAsync();

            ViewBag.SavingDonutChartData = savingsDistribution
                .GroupBy(x => x.SavingCategory)
                .Select(x => new
                {
                    categoryTitleWithIcon = x.Key.TitleWithIcon,
                    amount = x.Sum(s => s.Amount),
                    percentage = (x.Sum(s => s.Amount) / savingsDistribution.Sum(s => s.Amount)) * 100
                });

            return View();
        }
    }

    public class SplineChartData
    {
        public string day;
        public int income;
        public int expense;
    }
}
