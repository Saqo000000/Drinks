using Drinks.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drinks.ViewModels
{
    public class AddDrinkToMenuViewModel
    {
        public Menu Menu { get; set; }
        public List<SelectListItem> Drinks { get; set; }
        public AddDrinkToMenuViewModel()
        {

        }
        public int MenuId { get; set; }
        public int DrinkId { get; set; }
        public AddDrinkToMenuViewModel(Menu menu,IEnumerable<Drink> drinks)
        {
            Menu = menu;
            Drinks = new List<SelectListItem>();
            foreach (Drink drink in drinks)
            {
                Drinks.Add(new SelectListItem
                {
                    Value = drink.Id.ToString(),
                    Text = drink.Name
                });
            }
        }
    }
}
