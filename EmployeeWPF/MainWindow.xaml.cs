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

            //привязка к представлению
            employeeListBox.ItemsSource = employeeList;
            departmentListBox.ItemsSource = departmentList;
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

        /// <summary>
        /// Обработчик событий переключения между подразделениями в списке
        /// </summary>
        /// <param name="sender">Объект, который вызвал событие</param>
        /// <param name="e">Параметры вызова</param>
        private void DepartmentListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (departmentListBox.SelectedItem is Department department)
            {
                tbDepartmentName.Text = department.Name;
            }
        }

        /// <summary>
        /// Обработчик событий переключения между сотрудниками в списке
        /// </summary>
        /// <param name="sender">Объект, который вызвал событие</param>
        /// <param name="e">Параметры вызова</param>
        private void EmployeeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (employeeListBox.SelectedItem is Employee employee)
            {
                tbEmployeeFirstName.Text = employee.FirstName;
                tbEmployeeLastName.Text = employee.LastName;
                tbEmployeeDeprtment.Text = employee.Department.Name;
            }
        }

        /// <summary>
        /// Сохранение отредактированных данных
        /// </summary>
        /// <param name="sender">Объект, который вызвал событие</param>
        /// <param name="e">Параметры вызова</param>
        private void BtSave_Click(object sender, RoutedEventArgs e)
        {
            if (departmentListBox.SelectedItem is Department department)
            {
                department.Name = tbDepartmentName.Text;
            }

            if (employeeListBox.SelectedItem is Employee employee)
            {
                employee.FirstName = tbEmployeeFirstName.Text;
                employee.LastName = tbEmployeeLastName.Text;
            }

            RefreshData();
        }

        /// <summary>
        /// Обновление данных на экране
        /// </summary>
        private void RefreshData()
        {
            //списки
            departmentListBox.ItemsSource = null;
            departmentListBox.ItemsSource = departmentList;
            employeeListBox.ItemsSource = null;
            employeeListBox.ItemsSource = employeeList;
        }
    }
}
