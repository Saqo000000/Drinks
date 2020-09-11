using Drinks.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Drinks.Repositories.Interfaces
{
    public interface IMenuRepository
    {
        Task<IEnumerable<Menu>> GetAllMenus();
        Task<Menu> GetMenuById(int?id);
        Task AddMenu(Menu menu);
        Task<Menu> FrindMenuById(int? id);
        Task UpdateMenu(Menu menu);
        bool MenuExists(int id);
        Task RemoveMenu(Menu menu);
        Task<IEnumerable<DrinkMenu>> GetDrinkMenusById(int id);
        Task<Menu> GetMenuById(int id);
        bool DrinkMenusExists(int menuId, int drinkId);
        Task AddDrinkMenu(DrinkMenu drinkMenu);
    }
}
