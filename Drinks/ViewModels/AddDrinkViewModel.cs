using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Drinks.ViewModels
{
    public class AddDrinkViewModel
    {
        [Required]
        [Display(Name="Drink Name")]
        public string Name { get; set; }
        [Required(ErrorMessage ="You must give your drink a description")]
        public string Description { get; set; }

    }
}
