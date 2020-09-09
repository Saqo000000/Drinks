using Drinks.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Drinks.ViewModels
{
    public class ViewMenuViewModel
    {
        public IEnumerable<DrinkMenu> Items { get; set; }
        public Menu Menu { get; set; }
    }
}
