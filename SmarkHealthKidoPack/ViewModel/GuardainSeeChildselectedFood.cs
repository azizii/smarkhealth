using SmarkHealthKidoPack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmarkHealthKidoPack.ViewModel
{
    public class GuardainSeeChildselectedFood
    {
        public string childname { get; set; }
        public Food  foods { get; set; }
        public string selecteddate { get; set; }
        public int quantity { get; set; }
    }
}
