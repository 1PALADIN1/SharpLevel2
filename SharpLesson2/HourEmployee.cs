using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpLesson2
{
    /// <summary>
    /// Класс сотрудников с почасовой оплатой
    /// </summary>
    class HourEmployee : BaseEmployee
    {
        private double hourRate; //почасовая ставка

        public double HourRate
        {
            get => hourRate;
        }

        /// <summary>
        /// Инициализация сотрудника с почасовой оплатой
        /// </summary>
        public HourEmployee()
        {
            hourRate = 0.0;
        }

        /// <summary>
        /// Инициализация сотрудника с почасовой оплатой
        /// </summary>
        /// <param name="hourRate">Почасовая ставка</param>
        public HourEmployee(double hourRate)
        {
            this.hourRate = hourRate;
        }
        
        /// <summary>
        /// Считает месячную заработную плату сотрудника с почасовой оплатой
        /// </summary>
        /// <returns>Возращает сумму месячного оклада</returns>
        public override double CountSalary()
        {
            return 20.8 * 8 * HourRate;
        }
    }
}
