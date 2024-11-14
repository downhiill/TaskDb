using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.Models.Entities
{
    public class UserInfo
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int Age { get; set; }
        public DateTime DateCreate { get; set; }
    }
}
