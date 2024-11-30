using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.IService
{
    public class AccountModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public int UserId { get; set; }
        public UserModel UserModel { get; set; }
    }
}
