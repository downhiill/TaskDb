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
        /// Добавляет новую профессию.
        /// </summary>
        /// <param name="name">Название профессии.</param>
        void AddProfession(string name);

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

        /// <summary>
        /// Изменяет дату рождения пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя, чью дату рождения нужно изменить.</param>
        /// <param name="dateOfBirth">Новая дата рождения пользователя.</param>
        void EditDateOfBirth(int userId, DateTime dateOfBirth);

        /// <summary>
        /// Изменяет заработную плату пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя, чью заработную плату нужно изменить.</param>
        /// <param name="wages">Новая заработная плата пользователя.</param>
        void EditWages(int userId, decimal wages);

        /// <summary>
        /// Изменяет профессию пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя, чью профессию нужно изменить.</param>
        /// <param name="professionId">Идентификатор профессии, которую нужно назначить пользователю.</param>
        void EditProfessionUser(int userId, int? professionId);

        /// <summary>
        /// Удаляет пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя, которого нужно удалить.</param>
        void Delete(int userId);

        /// <summary>
        /// Удаляет профессию.
        /// </summary>
        /// <param name="professionId">Идентификатор профессии, которую нужно удалить.</param>
        void DeleteProfession(int professionId);

        /// <summary>
        /// Получает список всех пользователей.
        /// </summary>
        /// <returns>Список всех пользователей.</returns>
        List<UserModel> GetAllUsers();

        /// <summary>
        /// Получает список всех пользователей с краткой информацией.
        /// </summary>
        /// <param name="skip">Количество пользователей, которых нужно пропустить.</param>
        /// <param name="take">Количество пользователей, которых нужно вернуть.</param>
        /// <returns>Список всех пользователей с краткой информацией.</returns>
        List<ShortUser> GetAllShortUsers(int skip, int take);

        /// <summary>
        /// Получает список всех профессий пользователей.
        /// </summary>
        /// <returns>Список профессий пользователей.</returns>
        List<ModelUserProfession> GetAllProfessionsUsers();

        /// <summary>
        /// Получает статистику по профессиям.
        /// </summary>
        /// <returns>Статистика по профессиям.</returns>
        List<ModelProfessionStats> GetAllProfessionsStats();

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
