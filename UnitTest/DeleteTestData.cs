using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class DeleteTestData
    {
        public static IEnumerable<object[]> UserDeletionData => new List<object[]>
        {
            new object[] { 1, true },  // Существующий пользователь
            new object[] { 9999, false } // Несуществующий пользователь
        };
    }
}
