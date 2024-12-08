using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class EditTestData
    {
        public static IEnumerable<object[]> ValidUpdateNames => new List<object[]>
        {
            new object[] { "John", "Johnny" },
            new object[] { "Jane", "Janette" }
        };

        public static IEnumerable<object[]> NonExistentUserIds => new List<object[]>
        {
            new object[] { 9999, "NewName" }
        };

        public static IEnumerable<object[]> ValidAgeUpdates => new List<object[]>
        {
            new object[] { "John", 30, 35 }, // Имя, текущий возраст, новый возраст
            new object[] { "Jane", 25, 40 }
        };

        public static IEnumerable<object[]> NonExistentUserAgeUpdates => new List<object[]>
        {
            new object[] { 9999, 35 } // ID несуществующего пользователя, новый возраст
        };
    }
}
