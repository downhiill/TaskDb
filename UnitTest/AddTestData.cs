using Project.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class AddTestData
    {
        public static IEnumerable<object[]> ValidUsers => new List<object[]>
        {
            new object[] { new UserModel { Name = "John", Age = 30 }, true },
            new object[] { new UserModel { Name = "Alice", Age = 25 }, true }
        };

        public static IEnumerable<object[]> InvalidUsers => new List<object[]>
        {
            new object[] { new UserModel { Name = "", Age = 30 }, false },
            new object[] { new UserModel { Name = " ", Age = 25 }, false },
            new object[] { new UserModel { Name = "\t", Age = 40 }, false }
        };

        public static IEnumerable<object[]> AllUsers => ValidUsers.Concat(InvalidUsers);
    }
}
