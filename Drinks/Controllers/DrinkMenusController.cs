using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Drinks.Data;
using Drinks.Models;

namespace Drinks.Controllers
{
    public class DrinkMenusController : Controller
    {
        private readonly DrinkDbContext _context;

        public DrinkMenusController(DrinkDbContext context)
        {
            _context = context;
        }

        // GET: DrinkMenus
        public async Task<IActionResult> Index()
        {
            var drinkDbContext = _context.DrinkMenus.Include(d => d.Drink).Include(d => d.Menu);
            return View(await drinkDbContext.ToListAsync());
        }

        // GET: DrinkMenus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinkMenu = await _context.DrinkMenus
                .Include(d => d.Drink)
                .Include(d => d.Menu)
                .FirstOrDefaultAsync(m => m.DrinkID == id);
            if (drinkMenu == null)
            {
                return NotFound();
            }

            return View(drinkMenu);
        }

        // GET: DrinkMenus/Create
        public IActionResult Create()
        {
            ViewData["DrinkID"] = new SelectList(_context.Drinks, "Id", "Id");
            ViewData["MenuID"] = new SelectList(_context.Menus, "ID", "ID");
            return View();
        }

        // POST: DrinkMenus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MenuID,DrinkID")] DrinkMenu drinkMenu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(drinkMenu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DrinkID"] = new SelectList(_context.Drinks, "Id", "Id", drinkMenu.DrinkID);
            ViewData["MenuID"] = new SelectList(_context.Menus, "ID", "ID", drinkMenu.MenuID);
            return View(drinkMenu);
        }

        // GET: DrinkMenus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinkMenu = await _context.DrinkMenus.FindAsync(id);
            if (drinkMenu == null)
            {
                return NotFound();
            }
            ViewData["DrinkID"] = new SelectList(_context.Drinks, "Id", "Id", drinkMenu.DrinkID);
            ViewData["MenuID"] = new SelectList(_context.Menus, "ID", "ID", drinkMenu.MenuID);
            return View(drinkMenu);
        }

        // POST: DrinkMenus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MenuID,DrinkID")] DrinkMenu drinkMenu)
        {
            if (id != drinkMenu.DrinkID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(drinkMenu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DrinkMenuExists(drinkMenu.DrinkID))
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
            ViewData["DrinkID"] = new SelectList(_context.Drinks, "Id", "Id", drinkMenu.DrinkID);
            ViewData["MenuID"] = new SelectList(_context.Menus, "ID", "ID", drinkMenu.MenuID);
            return View(drinkMenu);
        }

        // GET: DrinkMenus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinkMenu = await _context.DrinkMenus
                .Include(d => d.Drink)
                .Include(d => d.Menu)
                .FirstOrDefaultAsync(m => m.DrinkID == id);
            if (drinkMenu == null)
            {
                return NotFound();
            }

            return View(drinkMenu);
        }

        // POST: DrinkMenus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var drinkMenu = await _context.DrinkMenus.FindAsync(id);
            _context.DrinkMenus.Remove(drinkMenu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DrinkMenuExists(int id)
        {
            return _context.DrinkMenus.Any(e => e.DrinkID == id);
        }
    }
}
