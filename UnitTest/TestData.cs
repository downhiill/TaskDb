using Project.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public static class TestData
    {
        public static IEnumerable<object[]> InvalidUsers => new List<object[]>
        {
            new object[] { new UserModel { Name = "", Age = 30, Wages = 12500, DateOfBirth = new DateTime(2000, 12, 25) } },
            new object[] { new UserModel { Name = " ", Age = 25, Wages = 12500, DateOfBirth = new DateTime(2000, 12, 25) } },
            new object[] { new UserModel { Name = "\t", Age = 40, Wages = 12500, DateOfBirth = new DateTime(2000, 12, 25) } }
        };
    }
}
