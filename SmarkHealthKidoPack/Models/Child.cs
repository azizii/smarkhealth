using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmarkHealthKidoPack.Models
{
    public class Child
    {
        public int ChildId { get; set; }

        public string ChildName { get; set; }

        public int age { get; set; }

        public int height { get; set; }

        public int weight { get; set; }

        public string password { get; set; }

        public string fingerprint { get; set; }

        public int guardianId { get; set; } 

        public Guardian Guardian { get; set; }
        public virtual ICollection<ChildFood> ChildFoods { get; set; }

     
       // public virtual ICollection<ChildFood>  { get; set; }
        //public IList<ChildFood>  childFoods { get; set; }
    }
}
