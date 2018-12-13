using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using EmployeeWPF.Model;

namespace EmployeeWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Employee> employeeList;
        private ObservableCollection<Department> departmentList;

        public MainWindow()
        {
            InitializeComponent();
            InitData();
        }

        /// <summary>
        /// инициализация списков
        /// </summary>
        private void InitData()
        {
            departmentList = new ObservableCollection<Department>();
            employeeList = new ObservableCollection<Employee>();

            //заполнение тестовыми данными
            FillTestData();
        }

        /// <summary>
        /// Заполнение списков тестовыми данными (временная функция)
        /// </summary>
        private void FillTestData()
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
