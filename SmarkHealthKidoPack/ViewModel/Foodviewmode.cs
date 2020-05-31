using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmarkHealthKidoPack.ViewModel
{
    public class Foodviewmode
    {
        public int FoodId { get; set; }

        public string foodName { get; set; }

        public int foodCalories { get; set; }
        public IFormFile photo { get; set; }
    }
}
