using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

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
        private static string selectEmployee = @"SELECT * FROM [dbo].[Employee]";
        private static string selectDepartment = @"SELECT * FROM [dbo].[Department]";

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
                FillLists();
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
        /// Наполение списков сотрудников и подразделений данными из БД
        /// </summary>
        private static void FillLists()
        {
            SqlDataReader reader;

            //наполняем подразделения
            command = new SqlCommand(selectDepartment, connection);
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while(reader.Read())
                {
                    departmentList.Add(new Department(reader.GetInt32(reader.GetOrdinal("Id")),
                                                      reader.GetString(reader.GetOrdinal("Name")) ));
                }
            }
            reader.Close();

            //наполняем сотрудников
            command = new SqlCommand(selectEmployee, connection);
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    employeeList.Add(new Employee(reader.GetInt32(reader.GetOrdinal("Id")),
                                                  reader.GetString(reader.GetOrdinal("FirstName")),
                                                  reader.GetString(reader.GetOrdinal("LastName")),
                                                  GetDepartmentById(reader.GetInt32(reader.GetOrdinal("DepartId"))) ));
                }
            }
            reader.Close();
        }

        /// <summary>
        /// Получение подразделения по идентификатору
        /// </summary>
        /// <returns></returns>
        private static Department GetDepartmentById(int id)
        {
            foreach (var item in departmentList)
            {
                if (item.Id == id)
                    return item;
            }
            return null;
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

        /// <summary>
        /// Обновление записи
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <param name="updateObject">Объект</param>
        public static void UpdateRecord(IDB updateObject)
        {
            string tableName = String.Empty;
            if (updateObject is Employee) tableName = "Employee";
            if (updateObject is Department) tableName = "Department";

            if (String.IsNullOrEmpty(tableName)) return;

            command = new SqlCommand(updateObject.UpdateString(tableName), connection);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Обновление всех данных
        /// </summary>
        public static void UpdateAllData()
        {
            foreach (var item in employeeList)
            {
                UpdateRecord(item);
            }

            foreach (var item in departmentList)
            {
                UpdateRecord(item);
            }
        }

        /// <summary>
        /// Вставка записи
        /// </summary>
        public static void InsertRecord(IDB insertObject)
        {
            string tableName = String.Empty;
            if (insertObject is Employee employee)
            {
                employeeList.Add(employee);
                tableName = "Employee";
            }
            if (insertObject is Department department)
            {
                departmentList.Add(department);
                tableName = "Department";
            }

            if (String.IsNullOrEmpty(tableName)) return;

            command = new SqlCommand(insertObject.InsertString(tableName), connection);
            command.ExecuteNonQuery();

            //временное решение, чтобы цеплялись id к новым записям
            departmentList.Clear();
            employeeList.Clear();
            FillLists();
        }

        public static void DeleteRecord(IDB deleteObject)
        {
            string tableName = String.Empty;
            if (deleteObject is Employee employee)
            {
                employeeList.Remove(employee);
                tableName = "Employee";
            }
            if (deleteObject is Department department)
            {
                departmentList.Remove(department);
                tableName = "Department";
            }

            if (String.IsNullOrEmpty(tableName)) return;

            command = new SqlCommand(deleteObject.DeleteString(tableName), connection);
            command.ExecuteNonQuery();
        }
    }
}
