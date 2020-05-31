using SmarkHealthKidoPack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmarkHealthKidoPack.ViewModel
{
    public class MessWelcomeViewModel
    {
        public int CustomersCount { get; set; }
        public int FoodCount { get; set; }
        public int adminCount { get; set; }
        public int categoryCount { get; set; }
        public List<Food> food { get; set; }
    }
}
