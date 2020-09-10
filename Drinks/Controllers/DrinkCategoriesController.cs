using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Drinks.Data;
using Drinks.Models;
using Drinks.Data.Data;
using Drinks.Models.Models;

namespace Drinks.Controllers
{
    public class DrinkCategoriesController : Controller
    {
        private readonly DrinkDbContext _context;

        public DrinkCategoriesController(DrinkDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinkCategory = await _context.Categories
                .FirstOrDefaultAsync(m => m.ID == id);
            if (drinkCategory == null)
            {
                return NotFound();
            }

            return View(drinkCategory);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] DrinkCategory drinkCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(drinkCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(drinkCategory);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinkCategory = await _context.Categories.FindAsync(id);
            if (drinkCategory == null)
            {
                return NotFound();
            }
            return View(drinkCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] DrinkCategory drinkCategory)
        {
            if (id != drinkCategory.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(drinkCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DrinkCategoryExists(drinkCategory.ID))
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
            return View(drinkCategory);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinkCategory = await _context.Categories
                .FirstOrDefaultAsync(m => m.ID == id);
            if (drinkCategory == null)
            {
                return NotFound();
            }

            return View(drinkCategory);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var drinkCategory = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(drinkCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DrinkCategoryExists(int id)
        {
            return _context.Categories.Any(e => e.ID == id);
        }
    }
}
