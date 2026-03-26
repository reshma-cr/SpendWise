using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpendWise.Data;
using SpendWise.Models;
using SpendWise.Models.ViewModels;

namespace SpendWise.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly SpendWiseDbContext _context;
        public ExpenseController(SpendWiseDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var expenses = await _context.Expenses.Include(e => e.Category).ToListAsync();
            return View(expenses);
        }
        public async Task<IActionResult> Create()
        {
            var vm = new CreateExpenseViewModel
            {
                Categories = await _context.Categories.ToListAsync()
            };

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateExpenseViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var expense = new Expense
                {
                    Amount = vm.Amount,
                    Date = vm.Date,
                    Description = vm.Description,
                    TransactionType = vm.Type,
                    CategoryId = vm.CategoryId
                };
                _context.Expenses.Add(expense);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Expense");
            }
            vm.Categories = await _context.Categories.ToListAsync();
            return View(vm);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
                return NotFound();
            var vm = new CreateExpenseViewModel
            {
                Amount = expense.Amount,
                Date = expense.Date,
                Description = expense.Description,
                Type = expense.TransactionType,
                CategoryId = expense.CategoryId,
                Categories = await _context.Categories.ToListAsync()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateExpenseViewModel vm)
        {
            if(ModelState.IsValid)
            {
                var expense = await _context.Expenses.FindAsync(id);

                if (expense == null)
                    return NotFound();

                expense.Amount = vm.Amount;
                expense.Date = vm.Date;
                expense.Description = vm.Description;
                expense.TransactionType = vm.Type;
                expense.CategoryId = vm.CategoryId;

                _context.Expenses.Update(expense);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Expense");
            }
            vm.Categories = await _context.Categories.ToListAsync();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);

            if (expense == null)
                return NotFound();

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Expense");
        }

    }
}
