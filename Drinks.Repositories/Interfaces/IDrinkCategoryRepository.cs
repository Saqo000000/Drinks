using Drinks.Models.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Drinks.Repositories.Interfaces
{
    public interface IDrinkCategoryRepository
    {
        Task<IEnumerable<DrinkCategory>> GetAllCategories();
        SelectList GetAllCategoriesAsSelectList();
    }
}
