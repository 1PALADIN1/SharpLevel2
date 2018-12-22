using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDepartment_WS.Models
{
    /// <summary>
    /// Класс сотрудников
    /// </summary>
    [Serializable]
    public class Employee : IDB
    {
        private int id;
        private string firstName;
        private string lastName;
        private Department department;

        public string FullName
        {
            get => $"{lastName} {firstName}";
        }

        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
            }
        }

        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
            }
        }

        public Department Department
        {
            get => department;
            set
            {
                department = value;
            }
        }

        /// <summary>
        /// Инициализация нового сотрудника
        /// </summary>
        /// <param name="firstName">Имя</param>
        /// <param name="lastName">Фамилия</param>
        /// <param name="department">Подразделение</param>
        public Employee(string firstName, string lastName, Department department)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.department = department;
        }

        /// <summary>
        /// Инициализация нового сотрудника
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="firstName">Имя</param>
        /// <param name="lastName">Фамилия</param>
        /// <param name="department">Подразделение</param>
        public Employee(int id, string firstName, string lastName, Department department)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.department = department;
        }

        /// <summary>
        /// Переопределение метода ToString()
        /// </summary>
        /// <returns>Полное имя сотрудника</returns>
        public override string ToString()
        {
            return $"{FullName} - {Department.Name}";
        }

        /// <summary>
        /// Генерация строки обновления данных
        /// </summary>
        /// <param name="tableName">Название таблицы</param>
        /// <returns>Возвращает итоговую строку запроса</returns>
        public string UpdateString(string tableName)
        {
            return $"UPDATE [dbo].[{tableName}] SET FirstName = '{FirstName}', LastName = '{LastName}', DepartId = {Department.Id} WHERE Id = {id};";
        }

        /// <summary>
        /// Генерация строки вставки данных
        /// </summary>
        /// <param name="tableName">Название таблицы</param>
        /// <returns>Возвращает итоговую строку запроса</returns>
        public string InsertString(string tableName)
        {
            return $"INSERT INTO [dbo].[{tableName}] (FirstName, LastName, DepartId) VALUES ('{FirstName}', '{LastName}', {Department.Id});";
        }

        /// <summary>
        /// Генерация строки удаления данных
        /// </summary>
        /// <param name="tableName">Название таблицы</param>
        /// <returns>Возвращает итоговую строку запроса</returns>
        public string DeleteString(string tableName)
        {
            return $"DELETE FROM  [dbo].[{tableName}] WHERE Id = {id};";
        }
    }
}
