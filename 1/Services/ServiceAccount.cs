using _1.Models.Context;
using _1.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.Services
{
    public class ServiceAccount
    {
        private readonly ApplicationContext _db;

        public ServiceAccount()
        {
            _db = new ApplicationContext();
        }

        public void UpdateAccount(int userId,string login,string password)
        {

            // Находим аккаунт по userId
            var account = _db.Account.SingleOrDefault(a => a.UserId == userId);
            if (account == null)
            {
                throw new InvalidOperationException("Account not found for the given user ID.");
            }

            // Обновляем логин и пароль
            account.Login = login;
            account.Password = password;

            // Сохраняем изменения
            _db.SaveChanges();
        }

        public void RemoveAccount(int userId)
        {
            var account = _db.Account.Find(userId);
            if (account != null)  // Проверяем, найден ли аккаунт
            {
                _db.Account.Remove(account);  // Удаляем аккаунт из базы данных
                _db.SaveChanges();  // Сохраняем изменения, чтобы удаление отразилось в базе данных
            }
            else
            {
                Console.WriteLine("Аккаунт не найден");  // Если аккаунт не найден, выводим сообщение
            }
        }

        public Account GetAccount(int userId)
        {
            return _db.Account.SingleOrDefault(a => a.UserId == userId);

        }
    }
    
}
