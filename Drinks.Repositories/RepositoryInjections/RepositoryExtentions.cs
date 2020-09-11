using Drinks.Repositories.Interfaces;
using Drinks.Repositories.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Drinks.Repositories.RepositoryInjections
{
    public static class RepositoryExtentions
    {
        public static void AddDIDrink(this IServiceCollection services)
        {
            services.AddTransient<IDrinkRepository, DrinkRepository>();
        }
        public static void AddDIDrinkCategory(this IServiceCollection services)
        {
            services.AddTransient<IDrinkCategoryRepository, DrinkCategoryRepository>();
        }
    }
}
