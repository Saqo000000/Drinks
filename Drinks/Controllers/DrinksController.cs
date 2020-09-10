using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Drinks.Data.Data;
using Drinks.Models.Models;

namespace Drinks.Controllers
{
    public class DrinksController : Controller
    {
        private readonly DrinkDbContext _context;

        public DrinksController(DrinkDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var drinkDbContext = _context.Drinks.Include(d => d.Category);
            return View(await drinkDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Drink drink = await _context.Drinks
                .Include(d => d.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (drink == null)
            {
                return NotFound();
            }

            return View(drink);
        }

        public IActionResult Create()
        {
            ViewBag.Category = new SelectList(_context.Categories, "ID", "Name");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,CategoryID")] Drink drink)
        {
            if (ModelState.IsValid)
            {
                _context.Drinks.Add(drink);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Category = new SelectList(_context.Categories, "ID", "Name");
            return View(drink);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drink = await _context.Drinks.FindAsync(id);
            if (drink == null)
            {
                return NotFound();
            }
            ViewBag.Category = new SelectList(_context.Categories, "ID", "Name");
            return View(drink);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,CategoryID")] Drink drink)
        {
            if (id != drink.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(drink);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DrinkExists(drink.Id))
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
            ViewBag.Category = new SelectList(_context.Categories, "ID", "Name", drink.CategoryID);
            return View(drink);
        }

        public async Task<IActionResult> GetDrinksByCategoryId(int? id)
        {
            if (id == null || id<=0)
            {
                return NotFound();
            }

            List<Drink> drinks = await _context.Drinks.Where(drink => drink.CategoryID == id).Include(dr=>dr.Category).ToListAsync();
            if (drinks == null)
            {
                return NotFound();
            }
            ViewBag.Category = null;
            foreach (Drink item in drinks)
            {
                ViewBag.Category = item.Category.Name;
                break;
            }
            return View("Index", drinks);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drink = await _context.Drinks
                .Include(d => d.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (drink == null)
            {
                return NotFound();
            }

            return View(drink);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var drink = await _context.Drinks.FindAsync(id);
            _context.Drinks.Remove(drink);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DrinkExists(int id)
        {
            return _context.Drinks.Any(e => e.Id == id);
        }
    }
}
