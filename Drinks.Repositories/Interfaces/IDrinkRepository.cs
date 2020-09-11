using Drinks.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Drinks.Repositories.Interfaces
{
    public interface IDrinkRepository
    {
        Task<IEnumerable<Drink>> GetAllDrinksWithCategory();
        Task<Drink> GetAllDrinkWithCategoryById(int? id);
        Task AddDrink(Drink drink);
        Task<Drink> GetDrinkById(int? id);
        Task UpdateDrink(Drink drink);
        bool DrinkExists(int id);
        Task<List<Drink>> GetDrinksByCategoryId(int? id);
        Task<Drink> GetDrinkForRemove(int? id);
        Task RemoveDrink(int id);

    }
}
