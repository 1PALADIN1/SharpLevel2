using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace EmployeeWPF.Model
{
    /// <summary>
    /// Класс для упраления списками сотрудников и подразделений
    /// </summary>
    class DataController
    {
        private static ObservableCollection<Employee> employeeList;
        private static ObservableCollection<Department> departmentList;
        private static SqlConnection connection;
        private static bool needFillData = false;
        private static SqlCommand command;

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
            
            try
            {
                InitDBConnection();
                connection.Open();
                GenerateDBSchema();
                FillTestData();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// Инициализация подключения к БД
        /// </summary>
        private static void InitDBConnection()
        {
            if (connection == null)
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString);
        }

        /// <summary>
        /// Генерация схемы данных (таблицы сотрдуников и подразделений)
        /// </summary>
        private static void GenerateDBSchema()
        {
            string createEmpTable = @"
                    CREATE TABLE [dbo].[Employee] (
                        [Id]        INT    IDENTITY(1,1)      NOT NULL,
                        [FirstName] VARCHAR (50) NOT NULL,
                        [LastName]  VARCHAR (50) NOT NULL,
                        [DepartId]  INT          NOT NULL,
                        PRIMARY KEY CLUSTERED ([Id] ASC)
                    );";
            string createDepartTable = @"
                    CREATE TABLE [dbo].[Department] (
                        [Id]   INT     IDENTITY(1,1)     NOT NULL,
                        [Name] VARCHAR (50) NOT NULL,
                        PRIMARY KEY CLUSTERED ([Id] ASC)
                    );";
            string selectEmployee = @"SELECT * FROM [dbo].[Employee]";
            string selectDepartment = @"SELECT * FROM [dbo].[Department]";

            try
            {
                //пробуем сделать выборку сотрудников
                command = new SqlCommand(selectEmployee, connection);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                //если таблицы не существует, создаём её
                command = new SqlCommand(createEmpTable, connection);
                command.ExecuteNonQuery();
                needFillData = true;
            }
            
            try
            {
                //пробуем сделать выборку предприятий
                command = new SqlCommand(selectDepartment, connection);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                //если таблицы не существует, создаём её
                command = new SqlCommand(createDepartTable, connection);
                command.ExecuteNonQuery();
                needFillData = true;
            }
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

            //заполняем таблицы тестовыми данными
            if (needFillData)
            {
                string deleteAllEmployee = @"DELETE FROM [dbo].[Employee];";
                string deleteAllDepartment = @"DELETE FROM [dbo].[Department];";

                //на всякий случай очищаем данные из таблиц (вдруг одна из них заполнена)
                command = new SqlCommand(deleteAllEmployee, connection);
                command.ExecuteNonQuery();
                command = new SqlCommand(deleteAllDepartment, connection);
                command.ExecuteNonQuery();

                var insertDepart = new string[]
                {
                    @"INSERT INTO [dbo].[Department] (Name) VALUES ('Man & Woman');",
                    @"INSERT INTO [dbo].[Department] (Name) VALUES ('Dance Dance');",
                    @"INSERT INTO [dbo].[Department] (Name) VALUES ('Gris');",
                    @"INSERT INTO [dbo].[Department] (Name) VALUES ('Nikke');",
                    @"INSERT INTO [dbo].[Department] (Name) VALUES ('Abidas');",
                    @"INSERT INTO [dbo].[Department] (Name) VALUES ('Apple Apple');"
                };

                foreach (var item in insertDepart)
                {
                    command = new SqlCommand(item, connection);
                    command.ExecuteNonQuery();
                }

                var inserEmployee = new string[]
                {
                     @"INSERT INTO [dbo].[Employee] (FirstName, LastName, DepartId) VALUES ('Olga', 'Buzova', 1);",
                     @"INSERT INTO [dbo].[Employee] (FirstName, LastName, DepartId) VALUES ('Igor', 'Zheplakov', 3);",
                     @"INSERT INTO [dbo].[Employee] (FirstName, LastName, DepartId) VALUES ('Enrike', 'Iglesias', 4);",
                     @"INSERT INTO [dbo].[Employee] (FirstName, LastName, DepartId) VALUES ('Victor', 'Michurin', 1);",
                     @"INSERT INTO [dbo].[Employee] (FirstName, LastName, DepartId) VALUES ('Nani', 'Abdulaev', 2);",
                     @"INSERT INTO [dbo].[Employee] (FirstName, LastName, DepartId) VALUES ('Nike', 'Nikov', 5);",
                };

                foreach (var item in inserEmployee)
                {
                    command = new SqlCommand(item, connection);
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Закрытие соединения
        /// </summary>
        public static void CloseDBConnection()
        {
            if (connection != null)
            {
                connection.Close();
            }
        }
    }
}
