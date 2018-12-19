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

            //привязка к представлению
            employeeListBox.ItemsSource = DataController.employeeList;
            departmentListBox.ItemsSource = DataController.departmentList;
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
            departmentListBox.ItemsSource = DataController.departmentList;
            employeeListBox.ItemsSource = null;
            employeeListBox.ItemsSource = DataController.employeeList;
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
                editWin.Show();
            }
        }

        /// <summary>
        /// Обработка нажатия клавиши добавления нового подразделения
        /// </summary>
        /// <param name="sender">Объект, который вызвал событие</param>
        /// <param name="e">Параметры вызова</param>
        private void BtAddDepartment_Click(object sender, RoutedEventArgs e)
        {
            var newDepartWin = new AddNewDepartWindow
            {
                Owner = this
            };
            newDepartWin.Show();
        }

        /// <summary>
        /// Обработка нажатия клавиши добавления нового солтрудника
        /// </summary>
        /// <param name="sender">Объект, который вызвал событие</param>
        /// <param name="e">Параметры вызова</param>
        private void BtAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            var newEmpWin = new AddNewEmployeeWindow
            {
                Owner = this
            };
            newEmpWin.Show();
        }

        /// <summary>
        /// Удаление выбранной записи
        /// </summary>
        /// <param name="sender">Объект, который вызвал событие</param>
        /// <param name="e">Параметры вызова</param>
        private void BtDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            if (departmentListBox.SelectedItem is Department department)
            {
                DataController.departmentList.Remove(department);
            }

            if (EmployeeListBox.SelectedItem is Employee employee)
            {
                DataController.employeeList.Remove(employee);
            }
        }
    }
}
