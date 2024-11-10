using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.Commands
{
    internal interface ICommand
    {
        /// <summary>
        /// Получает имя команды.
        /// </summary>
        /// <value>Имя команды, которое используется для её идентификации.</value>
        string Name { get; }
        void Execute();
    }
}
