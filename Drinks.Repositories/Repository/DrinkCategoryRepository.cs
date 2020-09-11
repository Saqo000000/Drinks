using Drinks.Data.Data;
using Drinks.Models.Models;
using Drinks.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drinks.Repositories.Repository
{
    internal class DrinkCategoryRepository : IDrinkCategoryRepository
    {

        private readonly DrinkDbContext _context;
        public DrinkCategoryRepository(DrinkDbContext context)
        {
            _context = context;
        }

        public async Task AddDrinkCategory(DrinkCategory drinkCategory)
        {
            _context.Categories.Add(drinkCategory);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DrinkCategory>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }


        public  SelectList GetAllCategoriesAsSelectList()
        {
            return  new SelectList(_context.Categories, "ID", "Name");
        }

        public async Task<DrinkCategory> GetCategoryById(int? id)
        {
           return await _context.Categories
                .FirstOrDefaultAsync(m => m.ID == id);
        }

        public async Task UpdateDrinkCategory(DrinkCategory drinkCategory)
        {
            _context.Update(drinkCategory);
            await _context.SaveChangesAsync();
        }
        public bool DrinkCategoryExists(int id)
        {
            return _context.Categories.Any(e => e.ID == id);
        }

        public async Task RemoveDrinkCategory(DrinkCategory drinkCategory)
        {
            _context.Categories.Remove(drinkCategory);
            await _context.SaveChangesAsync();
        }
    }
}
