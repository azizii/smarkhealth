using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmarkHealthKidoPack.Models
{
    public class FoodCategory
    {

        public int FoodCategoryId { get; set; }
        [Required]
        [Display(Name = "Foodcategory")]
        [StringLength(10)]
        public string FoodCategoryName { get; set; }

        public int MessId { get; set; }

        public Mess Mess { get; set; }

        public ICollection<Food> food { get; set; }
    }
}
