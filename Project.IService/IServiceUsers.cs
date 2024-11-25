using System;
using System.Collections.Generic;

namespace Project.IService
{
    /// <summary>
    /// Интерфейс для сервиса работы с пользователями.
    /// </summary>
    public interface IServiceUsers
    {
        /// <summary>
        /// Добавляет нового пользователя.
        /// </summary>
        /// <param name="user">Модель пользователя для добавления.</param>
        /// <returns>Идентификатор добавленного пользователя.</returns>
        int Add(UserModel user);

        /// <summary>
        /// Изменяет имя пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя, чье имя нужно изменить.</param>
        /// <param name="name">Новое имя пользователя.</param>
        void EditName(int userId, string name);

        /// <summary>
        /// Изменяет возраст пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя, чей возраст нужно изменить.</param>
        /// <param name="age">Новый возраст пользователя.</param>
        void EditAge(int userId, int age);

        void EditDateOfBirth(int userId, DateTime dateOfBirth);

        void EditWages(int userId, decimal wages);

        /// <summary>
        /// Удаляет пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя, которого нужно удалить.</param>
        void Delete(int userId);

        /// <summary>
        /// Получает список всех пользователей.
        /// </summary>
        /// <returns>Список всех пользователей.</returns>
        List<UserModel> GetAllUsers();

        List<ShortUser> GetAllShortUsers(int skip, int take);

        /// <summary>
        /// Ищет пользователей старше указанного возраста.
        /// </summary>
        /// <param name="age">Возраст, с которого нужно искать пользователей.</param>
        /// <returns>Список пользователей старше указанного возраста.</returns>
        List<UserModel> SearchUsersMoreAge(int age);

        /// <summary>
        /// Ищет пользователей по имени или другим параметрам.
        /// </summary>
        /// <param name="term">Поисковый запрос, который может соответствовать имени или другим параметрам пользователя.</param>
        /// <returns>Список пользователей, соответствующих поисковому запросу.</returns>
        List<UserModel> SearchUsers(string term);
    }
}
