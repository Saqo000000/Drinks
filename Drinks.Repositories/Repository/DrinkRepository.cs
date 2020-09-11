using Drinks.Data.Data;
using Drinks.Models.Models;
using Drinks.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drinks.Repositories.Repository
{
    internal class DrinkRepository : IDrinkRepository
    {
        private readonly DrinkDbContext _context;
        public DrinkRepository(DrinkDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Drink>> GetAllDrinksWithCategory()
        {
            return await _context.Drinks.Include(d => d.Category).ToListAsync();
        }

        public async Task<Drink> GetAllDrinkWithCategoryById(int? id)
        {
            return await _context.Drinks
                .Include(d => d.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddDrink(Drink drink)
        {
            await _context.Drinks.AddAsync(drink);
            _context.SaveChanges();
        }

        public async Task<Drink> GetDrinkById(int? id)
        {
            return await _context.Drinks.FindAsync(id);
        }

        public async Task UpdateDrink(Drink drink)
        {
            _context.Update(drink);
            await _context.SaveChangesAsync();
        }
        public bool DrinkExists(int id)
        {
            return _context.Drinks.Any(drink => drink.Id == id);
        }
        public async Task<List<Drink>> GetDrinksByCategoryId(int? id)
        {
            return await _context.Drinks.Where(drink => drink.CategoryID == id).Include(dr => dr.Category).ToListAsync();
        }
        public async Task<Drink> GetDrinkForRemove(int? id)
        {
            return await _context.Drinks
                .Include(d => d.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            
        }
        public async Task RemoveDrink(int id)
        {
            Drink drink= await _context.Drinks.FindAsync(id);
            _context.Drinks.Remove(drink);
            await _context.SaveChangesAsync();
        }
    }
}
