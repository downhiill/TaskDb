using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.Models.Entities
{
    public class ShortUserProfessionRole
    {
        public ShortUser User { get; set; }
        public ShortRole Role { get; set; }
        public string ProfessionName { get; set; }
    }
}
