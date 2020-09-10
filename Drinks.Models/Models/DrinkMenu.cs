using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drinks.Models.Models
{
    public class DrinkMenu
    {
        public int MenuID { get; set; }
        public Menu Menu { get; set; }

        public int DrinkID { get; set; }
        public Drink Drink { get; set; }
    }
}
