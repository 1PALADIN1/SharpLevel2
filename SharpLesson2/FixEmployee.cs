using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpLesson2
{
    /// <summary>
    /// Класс сотрудников с фиксированной оплатой
    /// </summary>
    class FixEmployee : BaseEmployee
    {
        private double salary; //зарплата

        /// <summary>
        /// Инициализация сотрудника с фиксированной оплатой
        /// </summary>
        public FixEmployee()
        {
            salary = 0.0;
        }

        /// <summary>
        /// Инициализация сотрудника с фиксированной оплатой
        /// </summary>
        /// <param name="salary">Зарплата</param>
        public FixEmployee(double salary)
        {
            this.salary = salary;
        }
        
        /// <summary>
        /// Считает месячную заработную плату сотрудника с фиксированной оплатой
        /// </summary>
        /// <returns>Возращает сумму месячного оклада</returns>
        public override double CountSalary()
        {
            return salary;
        }
    }
}
