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
        public async Task<IEnumerable<DrinkCategory>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }


        public  SelectList GetAllCategoriesAsSelectList()
        {
            return  new SelectList(_context.Categories, "ID", "Name");
        }

    }
}
