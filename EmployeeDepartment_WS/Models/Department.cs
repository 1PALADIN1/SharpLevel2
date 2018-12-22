using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDepartment_WS.Models
{
    /// <summary>
    /// Класс подразделений
    /// </summary>
    [Serializable]
    public class Department : IDB
    {
        private int id;
        private string name;

        public int Id
        {
            get => id;
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
            }
        }

        /// <summary>
        /// Инициализация нового подразделения
        /// </summary>
        /// <param name="name">Название</param>
        public Department(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Инициализация нового подразделения
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="name">Название</param>
        public Department(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        /// <summary>
        /// Переопределение метода ToString()
        /// </summary>
        /// <returns>Название подразделения</returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Генерация строки обновления данных
        /// </summary>
        /// <param name="tableName">Название таблицы</param>
        /// <returns>Возвращает итоговую строку запроса</returns>
        public string UpdateString(string tableName)
        {
            return $"UPDATE [dbo].[{tableName}] SET Name = '{name}' WHERE Id = {id};";
        }

        /// <summary>
        /// Генерация строки вставки данных
        /// </summary>
        /// <param name="tableName">Название таблицы</param>
        /// <returns>Возвращает итоговую строку запроса</returns>
        public string InsertString(string tableName)
        {
            return $"INSERT INTO [dbo].[{tableName}] (Name) VALUES ('{name}');";
        }

        /// <summary>
        /// Генерация строки удаления данных
        /// </summary>
        /// <param name="tableName">Название таблицы</param>
        /// <returns>Возвращает итоговую строку запроса</returns>
        public string DeleteString(string tableName)
        {
            return $"DELETE FROM [dbo].[{tableName}] WHERE Id = {id};";
        }
    }
}
