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
        public static IEnumerable<object[]> GetUsersForAdd()
        {
            // Данные для корректных и некорректных пользователей
            yield return new object[] { new UserModel { Name = "John", Age = 30, Wages = 12500, DateOfBirth = new DateTime(2000, 12, 25) }, true };
            yield return new object[] { new UserModel { Name = "", Age = 30, Wages = 12500, DateOfBirth = new DateTime(2000, 12, 25) }, false };
            yield return new object[] { new UserModel { Name = "John", Age = 25, Wages = 12500, DateOfBirth = new DateTime(2000, 12, 25) }, false };
        }
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
