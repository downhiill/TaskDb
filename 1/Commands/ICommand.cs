using System;

namespace _1.Commands
{
    /// <summary>
    /// Интерфейс для описания команды.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Получает название команды, отображаемое в меню.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Выполняет действие, связанное с данной командой.
        /// </summary>
        void Execute();
    }
}
