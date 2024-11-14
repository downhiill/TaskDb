using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.Models.Entities
{
    public class Account
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
