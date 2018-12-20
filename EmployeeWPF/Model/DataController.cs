using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeWPF.Model
{
    /// <summary>
    /// Класс для упраления списками сотрудников и подразделений
    /// </summary>
    class DataController
    {
        private static ObservableCollection<Employee> employeeList;
        private static ObservableCollection<Department> departmentList;

        public static ObservableCollection<Employee> EmployeeList
        {
            get
            {
                if (employeeList == null)
                    InitData();
                return employeeList;
            }
        }

        public static ObservableCollection<Department> DepartmentList
        {
            get
            {
                if (departmentList == null)
                    InitData();
                return departmentList;
            }
        }

        private DataController() { }

        /// <summary>
        /// Инициализация данных
        /// </summary>
        private static void InitData()
        {
            employeeList = new ObservableCollection<Employee>();
            departmentList = new ObservableCollection<Department>();
            FillTestData();
        }

        /// <summary>
        /// Заполнение списков тестовыми данными (временная функция)
        /// </summary>
        private static void FillTestData()
        {
            Department d1 = new Department("Главрыба");
            Department d2 = new Department("Look Oil");
            Department d3 = new Department("PinApple");
            Department d4 = new Department("Дворик в деревне");
            Department d5 = new Department("Man & Woman");

            departmentList.Add(d1);
            departmentList.Add(d2);
            departmentList.Add(d3);
            departmentList.Add(d4);
            departmentList.Add(d5);

            employeeList.Add(new Employee("Иван", "Каретников", d1));
            employeeList.Add(new Employee("Анатолий", "Жлобин", d2));
            employeeList.Add(new Employee("Евгения", "Зайцева", d4));
            employeeList.Add(new Employee("Илья", "Татаров", d3));
            employeeList.Add(new Employee("Наталья", "Ильина", d1));
            employeeList.Add(new Employee("Ирина", "Иванова", d2));
            employeeList.Add(new Employee("Виктория", "Ильичева", d5));
        }
    }
}
