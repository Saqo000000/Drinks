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
using Drinks.Models.ViewModels;
using Drinks.Repositories.Interfaces;

namespace Drinks.Controllers
{
    public class MenusController : Controller
    {
        
        private readonly IMenuRepository  menuRepository;
        private readonly IDrinkRepository  drinkRepository;


        public MenusController(IMenuRepository menuRepository,IDrinkRepository drinkRepository)
        {
            this.menuRepository = menuRepository;
            this.drinkRepository = drinkRepository;
        }


        public async Task<IActionResult> Index()
        {
            return View(await menuRepository.GetAllMenus());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var menu = await menuRepository.GetMenuById(id);
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
                await menuRepository.AddMenu(menu);
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

            Menu menu = await menuRepository.GetMenuById(id);

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
                    await menuRepository.UpdateMenu(menu);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!menuRepository.MenuExists(menu.ID))
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

            var menu = await menuRepository.GetMenuById(id);
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
            var menu = await menuRepository.GetMenuById(id);
            if (menu == null)
            {
                NotFound();
            }
            await menuRepository.RemoveMenu(menu);
            return RedirectToAction(nameof(Index));
        }

        //must have [ActionName]
        public async Task<IActionResult> MenuRowContent(int id)
        {
            IEnumerable<DrinkMenu> items = await menuRepository.GetDrinkMenusById(id);
            Menu menu = await menuRepository.GetMenuById(id);
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
            Menu menu = await menuRepository.GetMenuById(id);
                //_context.Menus.SingleAsync(m => m.ID == id);
            if (menu == null)
            {
                return NotFound();
            }
            IEnumerable<Drink> drinks = await drinkRepository.GetAllDrinksWithCategory();
              //  _context.Drinks.ToListAsync();

            return View(new AddDrinkToMenuViewModel(menu, drinks));
        }

        [HttpPost]
        public async Task<IActionResult> AddDrinkToMenu(AddDrinkToMenuViewModel model)
        {
            if (ModelState.IsValid)
            {
                int menuId = model.MenuId;
                int drinkId = model.DrinkId;
                if (menuRepository.MenuExists(menuId) && drinkRepository.DrinkExists(drinkId))
                {
                    if (!menuRepository.DrinkMenusExists(menuId, drinkId))
                    {
                        DrinkMenu drinkMenu = new DrinkMenu
                        {
                            DrinkID = drinkId,
                            MenuID = menuId
                        };
                       await menuRepository.AddDrinkMenu(drinkMenu);
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return RedirectToAction(nameof(AddDrinkToMenu), model.MenuId);
        }
    }
}
