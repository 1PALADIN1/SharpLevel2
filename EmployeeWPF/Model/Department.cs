using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeWPF.Model
{
    /// <summary>
    /// Класс подразделений
    /// </summary>
    class Department : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string name;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                NotifyPropertyChanged(nameof(this.Name));
            }
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

        /// <summary>
        /// Уведомление об изменении свойства объекта
        /// </summary>
        /// <param name="propName">Название свойства</param>
        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
