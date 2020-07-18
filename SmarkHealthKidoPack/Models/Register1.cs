using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmarkHealthKidoPack.Models
{
    public class Register1
    {
        public int Register1Id { get; set; }
        // [Column(TypeName = "varchar(MAX)")]
        public byte[] fingerprints { get; set; }
    }
}
