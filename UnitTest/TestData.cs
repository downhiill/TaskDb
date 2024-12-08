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
        public static IEnumerable<object[]> GetUsersForDelete()
        {
            // Пример: возвращаем список пользователей и флаг успешности удаления
            yield return new object[] { 1, true }; // Существующий пользователь с ID 1
            yield return new object[] { 9999, false }; // Несуществующий пользователь с ID 9999
        }

        public static IEnumerable<object[]> GetUsersForUpdateName => new List<object[]>
        {
            new object[] { "John", "Johnny", true },    // Успешное изменение имени
            new object[] { "Jane", "Janette", true },   // Успешное изменение имени
            new object[] { "NonExistentUser", "NewName", false }  // Несуществующий пользователь
        };
        public static IEnumerable<object[]> GetUsersForUpdateAge => new List<object[]>
        {
            new object[] { 30, 35, true },    // Успешное изменение возраста
            new object[] { 40, 45, true },    // Успешное изменение возраста
            new object[] { 9999, 35, false }  // Несуществующий пользователь
        };

    }
}
