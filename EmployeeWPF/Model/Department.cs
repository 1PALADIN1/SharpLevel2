using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeWPF.Model
{
    /// <summary>
    /// Класс подразделений
    /// </summary>
    class Department
    {
        private string name;

        public string Name
        {
            get => name;
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
        /// Переопределение метода ToString()
        /// </summary>
        /// <returns>Название подразделения</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
