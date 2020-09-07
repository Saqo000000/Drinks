using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drinks.Models
{
    public class DrinkCategory
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public IEnumerable<Drink> Drinks { get; set; }
    }
}
