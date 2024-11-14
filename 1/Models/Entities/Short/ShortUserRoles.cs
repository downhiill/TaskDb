using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.Models.Entities.Short
{
    public class ShortUserRoles
    {
        public ShortUser User { get; set; }
        public List<ShortRole> Roles { get; set; }
    }
}
