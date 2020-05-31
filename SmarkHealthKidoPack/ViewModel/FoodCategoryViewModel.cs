using SmarkHealthKidoPack.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmarkHealthKidoPack.ViewModel
{
    public class FoodCategoryViewModel
    {
        [Required]
        public FoodCategory  foodCategory{ get; set; }
        public string Referer { get; set; }
    }
}
