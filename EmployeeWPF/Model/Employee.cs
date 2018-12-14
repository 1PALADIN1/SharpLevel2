using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeWPF.Model
{
    /// <summary>
    /// Класс сотрудников
    /// </summary>
    class Employee
    {
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
            set => firstName = value;
        }

        public string LastName
        {
            get => lastName;
            set => lastName = value;
        }

        public Department Department
        {
            get => department;
            set => department = value;
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
        /// Переопределение метода ToString()
        /// </summary>
        /// <returns>Полное имя сотрудника</returns>
        public override string ToString()
        {
            return $"{FullName} - {Department.Name}";
        }
    }
}
