using Drinks.Data.Data;
using Drinks.Models.Models;
using Drinks.Models.ViewModels;
using Drinks.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drinks.Repositories.Repository
{
    internal class MenuRepository: IMenuRepository
    {
        private readonly DrinkDbContext _context;

        public MenuRepository(DrinkDbContext context)
        {
            _context = context;
        }

        public async Task AddMenu(Menu menu)
        {
            _context.Menus.Add(menu);
            await _context.SaveChangesAsync();
        }

        public async Task<Menu> FrindMenuById(int? id)
        {
           return await _context.Menus.FindAsync(id);
        }

        public async Task<IEnumerable<Menu>> GetAllMenus()
        {
            return await _context.Menus.ToListAsync();
        }

        public async Task<Menu> GetMenuById(int? id)
        {
            return await _context.Menus
                .FirstOrDefaultAsync(m => m.ID == id);
        }

        public async Task UpdateMenu(Menu menu)
        {
            _context.Update(menu);
            await _context.SaveChangesAsync();
        }
        public bool MenuExists(int id)
        {
            return _context.Menus.Any(menu => menu.ID == id);
        }

        public async Task RemoveMenu(Menu menu)
        {
            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DrinkMenu>> GetDrinkMenusById(int id)
        {
           return await _context.DrinkMenus.
                Include(item => item.Drink).
                Where(dm => dm.MenuID == id).ToListAsync();
        }

        public async Task<Menu> GetMenuById(int id)
        {
           return await _context.Menus.SingleAsync(m => m.ID == id);
        }
        public bool DrinkMenusExists(int menuId, int drinkId)
        {
            return _context.DrinkMenus.Any(drink => drink.DrinkID == drinkId && drink.MenuID == menuId);
        }
        public async Task AddDrinkMenu(DrinkMenu drinkMenu)
        {
            await _context.DrinkMenus.AddAsync(drinkMenu);
            await _context.SaveChangesAsync();
        }
    }
}
