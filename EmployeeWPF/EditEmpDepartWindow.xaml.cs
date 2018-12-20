using EmployeeWPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace EmployeeWPF
{
    /// <summary>
    /// Логика взаимодействия для EditEmpDepartWindow.xaml
    /// </summary>
    public partial class EditEmpDepartWindow : Window
    {
        private Employee selectedEmployee;

        internal Employee SelectedEmployee
        {
            get => selectedEmployee;
            set => selectedEmployee = value;
        }

        public EditEmpDepartWindow()
        {
            InitializeComponent();
            InitData();
        }

        /// <summary>
        /// Инициализация данных
        /// </summary>
        private void InitData()
        {
            departmentListBox.ItemsSource = DataController.departmentList;
        }

        /// <summary>
        /// Кнопка выбора подразделения
        /// </summary>
        /// <param name="sender">Объект, который вызвал событие</param>
        /// <param name="e">Параметры вызова</param>
        private void BtEmpChooseDepart_Click(object sender, RoutedEventArgs e)
        {
            if (departmentListBox.SelectedItem is Department department)
            {
                SelectedEmployee.Department = department;
                Close();
            }
        }
    }
}
