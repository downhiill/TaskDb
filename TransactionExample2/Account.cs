using System;

namespace TransactionExample
{
    /// <summary>
    /// Представляет аккаунт с идентификатором, именем и балансом.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Получает или задает уникальный идентификатор аккаунта.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или задает имя владельца аккаунта.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Получает или задает текущий баланс аккаунта.
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Возвращает строковое представление аккаунта с его идентификатором, именем и балансом.
        /// </summary>
        /// <returns>
        /// Строка, представляющая аккаунт в формате "Id: {Id}, Name: {Name}, Balance: {Balance}".
        /// </returns>
        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Balance: {Balance}";
        }
    }
}
