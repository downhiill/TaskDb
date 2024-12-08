using Project.Data;
using Project.IService;
using System;
using System.Linq;

namespace Project.Service
{
    public class ServiceAccount
    {
        private readonly ApplicationContext _context;

        /// <summary>
        /// Конструктор для инъекции зависимости ApplicationContext.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public ServiceAccount(ApplicationContext context)
        {
            _context = context;
        }

        public void UpdateAccount(int userId, string login, string password)
        {
            // Находим аккаунт по userId
            var account = _context.Account.SingleOrDefault(a => a.UserId == userId);
            if (account == null)
            {
                throw new InvalidOperationException("Account not found for the given user ID.");
            }

            // Обновляем логин и пароль
            account.Login = login;
            account.Password = password;

            // Сохраняем изменения
            _context.SaveChanges();
        }

        public void RemoveAccount(int userId)
        {
            // Находим аккаунт по userId
            var account = _context.Account
                .FirstOrDefault(a => a.UserId == userId);

            if (account == null)
            {
                throw new InvalidOperationException("Account not found for the given user ID.");
            }

            // Удаляем найденный аккаунт
            _context.Account.Remove(account);
            _context.SaveChanges();
        }

        public Account GetAccount(int userId)
        {
            var account = _context.Account.SingleOrDefault(a => a.UserId == userId);
            if (account == null)
            {
                throw new InvalidOperationException("Account not found for the given user ID.");
            }

            return account;
        }
    }
}
