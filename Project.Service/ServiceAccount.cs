using Project.Data;
using Project.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var account = _context.Account.Find(userId);
            if (account != null)  // Проверяем, найден ли аккаунт
            {
                _context.Account.Remove(account);  // Удаляем аккаунт из базы данных
                _context.SaveChanges();  // Сохраняем изменения, чтобы удаление отразилось в базе данных
            }
            else
            {
                Console.WriteLine("Аккаунт не найден");  // Если аккаунт не найден, выводим сообщение
            }
        }

        public Account GetAccount(int userId)
        {
            return _context.Account.SingleOrDefault(a => a.UserId == userId);

        }
    }
}
