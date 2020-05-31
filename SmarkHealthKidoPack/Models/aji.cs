using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmarkHealthKidoPack.Models
{
    public class aji
    {
        public int ajiId { get; set; }
        public string Name { get; set; }
       // public IList<>  { get; set; }
        public virtual ICollection<ajiabdullah> ajiabdullahs { get; set; }
      
    }
}
