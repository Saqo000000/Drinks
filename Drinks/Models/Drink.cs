﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Drinks.Models
{
    public class Drink
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }





        public int CategoryID { get; set; }
        public DrinkCategory Category { get; set; }




        public IEnumerable<DrinkMenu> DrinkMenus { get; set; }
    }
}
