using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data
{
    public class Account
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public int UserId { get; set; }
        public UserDb UserDb { get; set; }
    }
}
