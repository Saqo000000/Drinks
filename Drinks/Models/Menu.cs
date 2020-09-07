using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drinks.Models
{
    public class Menu
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<DrinkMenu> DrinkMenus { get; set; }    //= new List<DrinkMenu>();
    }
}
