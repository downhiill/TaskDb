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
        public static IEnumerable<object[]> ValidUsers => new List<object[]>
        {
            new object[] { new UserModel { Name = "John", SecondName = "Abun", FullName = "John Abun", Age = 30 }, true },
            new object[] { new UserModel { Name = "Alice", SecondName = "Abun", FullName = "John Abun", Age = 25 }, true }
        };
        public static IEnumerable<object[]> InvalidUsers => new List<object[]>
        {
            new object[] { new UserModel { Name = "", Age = 30 }, false },
            new object[] { new UserModel { Name = " ", Age = 25 }, false },
            new object[] { new UserModel { Name = "\t", Age = 40 }, false }
        };
        public static IEnumerable<object[]> AllUsers => ValidUsers.Concat(InvalidUsers);


        public static IEnumerable<object[]> ValidProfessions => new List<object[]>
        {
            new object[] { "Software Developer", true },
            new object[] { "Data Scientist", true }
        };
        public static IEnumerable<object[]> InvalidProfessions => new List<object[]>
        {
            new object[] { "", false },
            new object[] { " ", false },
            new object[] { "\t", false }
        };

        public static IEnumerable<object[]> AllProfessions => ValidProfessions.Concat(InvalidProfessions);

    }
}
