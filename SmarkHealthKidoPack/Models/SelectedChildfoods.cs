using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmarkHealthKidoPack.Models
{
    public class SelectedChildfoods
    {
        public int SelectedChildfoodsId { get; set; }
        public int childid { get; set; }
        public int foodid { get; set; }
        public string dateselected { get; set; }
        public int quantity { get; set; }
        
    }
}
