using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmarkHealthKidoPack.Models
{
    public class Food
    {
        public int FoodId { get; set; }

        public string FoodName { get; set; }
        public bool Isselected { get; set; }

        public int foodCalories { get; set; }

        public string photopath { get; set; }
    }
}
