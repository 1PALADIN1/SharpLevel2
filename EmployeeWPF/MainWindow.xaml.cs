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

        public ListBox EmployeeListBox
        {
            get => employeeListBox;
        }

        /// <summary>
        /// инициализация списков
        /// </summary>
        private void InitData()
        {
            DataController.FillTestData();

            employeeList = DataController.employeeList;
            departmentList = DataController.departmentList;

            //привязка к представлению
            employeeListBox.ItemsSource = employeeList;
            departmentListBox.ItemsSource = departmentList;
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

        /// <summary>
        /// Изменение подразделения сотрудника
        /// </summary>
        /// <param name="sender">Объект, который вызвал событие</param>
        /// <param name="e">Параметры вызова</param>
        private void BtChangeDepartment_Click(object sender, RoutedEventArgs e)
        {
            //должен быть выбран сотрудник
            if (employeeListBox.SelectedItem is Employee employee)
            {
                var editWin = new EditEmpDepartWindow
                {
                    Owner = this,
                    SelectedEmployee = employee
                };

                editWin.Closed += EditWin_Closed;
                editWin.Show();
            }
        }

        /// <summary>
        /// Событие происходящие по закрытию окна редактирования
        /// </summary>
        /// <param name="sender">Объект, который вызвал событие</param>
        /// <param name="e">Параметры вызова</param>
        private void EditWin_Closed(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}
