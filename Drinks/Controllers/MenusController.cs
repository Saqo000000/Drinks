using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Drinks.Data;
using Drinks.Models;
using Drinks.ViewModels;

namespace Drinks.Controllers
{
    public class MenusController : Controller
    {
        private readonly DrinkDbContext _context;

        public MenusController(DrinkDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Menus.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus
                .FirstOrDefaultAsync(m => m.ID == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                _context.Menus.Add(menu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(menu);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var menu = await _context.Menus.FindAsync(id);
            
            if (menu == null)
            {
                return NotFound();
            }
            return View(menu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] Menu menu)
        {
            if (id != menu.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuExists(menu.ID))
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
            return View(menu);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus
                .FirstOrDefaultAsync(m => m.ID == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menu = await _context.Menus.FindAsync(id);
            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> MenuRowContent(int id)
        {
            IEnumerable<DrinkMenu> items = await _context.DrinkMenus.
                Include(item => item.Drink).
                Where(dm => dm.MenuID == id).ToListAsync();
            Menu menu = await _context.Menus.SingleAsync(m => m.ID == id);
            if (menu == null || items == null)
            {
                return NotFound();
            }
            ViewMenuViewModel view = new ViewMenuViewModel()
            {
                Items = items,
                Menu = menu
            };
            return View(view);
        }


        [HttpGet]
        public async Task<IActionResult> AddDrinkToMenu(int id)
        {
            Menu menu = await _context.Menus.SingleAsync(m => m.ID == id);
            if (menu==null)
            {
                return NotFound();
            }
            IEnumerable<Drink> drinks = await _context.Drinks.ToListAsync();

            return View(new AddDrinkToMenuViewModel(menu, drinks));
        }

        [HttpPost]
        public async Task<IActionResult> AddDrinkToMenu(AddDrinkToMenuViewModel model)
        {
            if (ModelState.IsValid)
            {
                int menuId = model.MenuId;
                int drinkId = model.DrinkId;
                if (MenuExists(menuId)&& DrinkExists(drinkId))
                {
                    if (!DrinkMenusExists(menuId,drinkId))
                    {
                        DrinkMenu drinkMenu = new DrinkMenu
                        {
                            DrinkID = drinkId,
                            MenuID = menuId
                        };
                        await _context.DrinkMenus.AddAsync(drinkMenu);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return RedirectToAction(nameof(AddDrinkToMenu), model.MenuId);
        }
        private bool MenuExists(int id)
        {
            return _context.Menus.Any(menu => menu.ID == id);
        }
        private bool DrinkExists(int id)
        {
            return _context.Drinks.Any(drink => drink.Id == id);
        }
        private bool DrinkMenusExists(int menuId,int drinkId)
        {
            return _context.DrinkMenus.Any(drink => drink.DrinkID == drinkId && drink.MenuID==menuId);
        }
    }
}
