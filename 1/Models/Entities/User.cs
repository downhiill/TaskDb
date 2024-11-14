using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public int Age { get; set; }
        public DateTime DateCreate { get; set; }
        public decimal Wages { get; set; }
        public bool Active { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string FullName { get; set; }
        public int? ProfessionId {  get; set; }
        public Profession Profession { get; set; }
        public List<RolesUsers> Roles { get; set; }
        public UserInfo Info { get; set; }
        public Account Account { get; set; }

    }
}
