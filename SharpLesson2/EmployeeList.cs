using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//г) *Создать класс, содержащий массив сотрудников, и реализовать возможность вывода данных с использованием foreach.
namespace SharpLesson2
{
    class EmployeeList : IEnumerable
    {
        private List<BaseEmployee> empList;

        /// <summary>
        /// Инициализация списка сотрудников
        /// </summary>
        public EmployeeList()
        {
            empList = new List<BaseEmployee>();
        }

        /// <summary>
        /// Добавляет сотрудника в список
        /// </summary>
        /// <param name="employee">Сотрудник</param>
        public void Add(BaseEmployee employee)
        {
            empList.Add(employee);
        }

        /// <summary>
        /// Подсчитывает зарплату для указанного сотрудника
        /// </summary>
        /// <param name="index">Индекс сотрудника в списке</param>
        /// <returns></returns>
        public double CountSalary(int index)
        {
            return empList[index].CountSalary();
        }

        /// <summary>
        /// Возвращает перечислитель, осуществляющий перебор элементов списка
        /// </summary>
        /// <returns>Объект типа Enumerator</returns>
        public IEnumerator GetEnumerator()
        {
            return empList.GetEnumerator();
        }
    }
}
