using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Drinks.Data.Data;
using Drinks.Models.Models;
using Drinks.Repositories.Interfaces;

namespace Drinks.Controllers
{
    public class DrinksController : Controller
    {
        private readonly IDrinkRepository drinkRepository;
        private readonly IDrinkCategoryRepository categoryRepository;


        public DrinksController(IDrinkRepository repository, IDrinkCategoryRepository categoryRepository)
        {
            this.drinkRepository = repository;
            this.categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await drinkRepository.GetAllDrinksWithCategory());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Drink drink=await drinkRepository.GetAllDrinkWithCategoryById(id);
            if (drink == null)
            {
                return NotFound();
            }

            return View(drink);
        }
        
        public IActionResult Create()
        {
            ViewBag.Category = categoryRepository.GetAllCategoriesAsSelectList();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,CategoryID")] Drink drink)
        {
            if (ModelState.IsValid)
            {
                await drinkRepository.AddDrink(drink);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Category = categoryRepository.GetAllCategoriesAsSelectList();
            return View(drink);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Drink drink = await drinkRepository.GetDrinkById(id);
            if (drink == null)
            {
                return NotFound();
            }
            ViewBag.Category = categoryRepository.GetAllCategoriesAsSelectList();
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
                    await drinkRepository.UpdateDrink(drink);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!drinkRepository.DrinkExists(drink.Id))
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
            ViewBag.Category = categoryRepository.GetAllCategoriesAsSelectList();
            return View(drink);
        }

        public async Task<IActionResult> GetDrinksByCategoryId(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            List<Drink> drinks = await drinkRepository.GetDrinksByCategoryId(id);
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
            Drink drink = await drinkRepository.GetDrinkForRemove(id);
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
            await drinkRepository.RemoveDrink(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
