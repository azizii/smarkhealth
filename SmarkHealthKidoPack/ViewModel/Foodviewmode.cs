using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmarkHealthKidoPack.ViewModel
{
    public class Foodviewmode
    {
        public int FoodId { get; set; }
        [Required]
        [Display(Name = "Food name")]
        [StringLength(10)]
        public string foodName { get; set; }

        [Required]
        [Range(0, 999.99)]
        public decimal price { get; set; }

        [Required]
        [Range(0, 999.99)]
        public int foodCalories { get; set; }
        public IFormFile photo { get; set; }
        public int foodCategoryId { get; set; }
       





    }
}
