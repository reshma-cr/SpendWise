using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpendWise.Data;
using SpendWise.Models;

namespace SpendWise.Controllers
{
    public class CategoryController : Controller
    {
        private readonly SpendWiseDbContext _context;
        public CategoryController(SpendWiseDbContext context) 
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                // reload categories if validation fails
                var categories = await _context.Categories.ToListAsync();
                return View("Index", categories);
            }

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
                return NotFound();

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }
    }
}
