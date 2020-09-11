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
using Drinks.Repositories.Interfaces;

namespace Drinks.Controllers
{
    public class DrinkCategoriesController : Controller
    {

        private readonly IDrinkCategoryRepository  drinkCategoryRepository;

        public DrinkCategoriesController(IDrinkCategoryRepository  drinkCategoryRepository)
        {
            this.drinkCategoryRepository = drinkCategoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await drinkCategoryRepository.GetAllCategories());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinkCategory = await drinkCategoryRepository.GetCategoryById(id);
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
                await drinkCategoryRepository.AddDrinkCategory(drinkCategory);
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

            DrinkCategory drinkCategory = await drinkCategoryRepository.GetCategoryById(id);
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
                    await drinkCategoryRepository.UpdateDrinkCategory(drinkCategory);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!drinkCategoryRepository.DrinkCategoryExists(drinkCategory.ID))
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

            DrinkCategory drinkCategory = await drinkCategoryRepository.GetCategoryById(id);
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
            if (drinkCategoryRepository.DrinkCategoryExists(id))
            {
                DrinkCategory drinkCategory = await drinkCategoryRepository.GetCategoryById(id);
                await drinkCategoryRepository.RemoveDrinkCategory(drinkCategory);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
