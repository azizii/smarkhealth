using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmarkHealthKidoPack.Models
{
    public class Food
    {

        public int FoodId { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        [StringLength(10)]
        public string FoodName { get; set; }

        [Required]
        [Range(0, 999.99)]
        public decimal Price { get; set; }

        [Required]
        [Range(1, 800, ErrorMessage = "Can only be between 1 .. 800")]
        [StringLength(3, ErrorMessage = "Max 3 digits")]
        [Remote("PredictionOK", "Predict", ErrorMessage = "Prediction can only be a number in range 1 .. 800")]
        public int foodCalories { get; set; }

       
        public string photopath { get; set; }

        public int foodCategoryId { get; set; }

        public FoodCategory foodCategory { get; set; }



        public virtual ICollection<ChildFood> ChildFoods { get; set; }

       
       // public virtual ICollection<ChildFood> ChildFoods { get; set; }
        //  public IList<ChildFood> childFoods { get; set; }
    }
}
