using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data
{
    public class UserInfo
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserDb UserDb { get; set; }
        public int Age { get; set; }
        public DateTime DateCreate { get; set; }
    }
}
