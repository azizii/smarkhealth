using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmarkHealthKidoPack.Models
{
    public class ChildFood
    {
        

        public int FoodId { get; set; }

        public Food Food { get; set; }

        public int ChildId { get; set; }
      
        public Child  child{ get; set; }
    }
}
