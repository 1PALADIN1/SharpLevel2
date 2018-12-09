using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpLesson1
{
    /// <summary>
    /// Класс для логирования
    /// </summary>
    class Log
    {
        public delegate string LogMessage();

        /// <summary>
        /// Логирование в консоль
        /// </summary>
        /// <param name="message">Сообщение</param>
        public static void LogConsole(LogMessage message)
        {
            Console.WriteLine(">> {0}", message.Invoke());
        }
    }
}
