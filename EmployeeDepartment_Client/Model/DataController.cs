using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Http;
using System.Data;
using EmployeeWPF.Utils;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EmployeeWPF.Model
{
    /// <summary>
    /// Класс для упраления списками сотрудников и подразделений
    /// </summary>
    class DataController
    {
        private static ObservableCollection<Employee> employeeList;
        private static ObservableCollection<Department> departmentList;
        private static HttpClient httpClient;

        public static readonly string endPoint = ConfigurationManager.AppSettings["BaseAddress"]; //получение адреса сервиса из конфига

        //CLEAR
        private static SqlConnection connection;
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
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            //настроки конвертера
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            FillLists();
        }

        /// <summary>
        /// Наполение списков сотрудников и подразделений данными
        /// </summary>
        private static void FillLists()
        {
            //получаем подразделения
            var departResult = httpClient.GetStringAsync($"{endPoint}{Endpoint.getDepartments}").Result;
            JArray jArray = JArray.Parse(departResult);

            foreach (var item in jArray)
            {
                departmentList.Add(new Department(Convert.ToInt32(item["id"]), item["name"].ToString()));
            }

            //получаем сотрудников
            var empResult = httpClient.GetStringAsync($"{endPoint}{Endpoint.getEmployees}").Result;
            jArray = JArray.Parse(empResult);
            foreach (var item in jArray)
            {
                employeeList.Add(new Employee(Convert.ToInt32(item["id"]), item["firstName"].ToString(),
                                    item["lastName"].ToString(), GetDepartmentById(Convert.ToInt32(item["department"]["id"]))));
            }
        }

        /// <summary>
        /// Получение подразделения из списка по идентификатору
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
        /// Обновление записи
        /// </summary>
        /// <param name="updateObject">Объект</param>
        public static void UpdateRecord(IDB updateObject)
        {
            //string tableName = String.Empty;
            //if (updateObject is Employee) tableName = "Employee";
            //if (updateObject is Department) tableName = "Department";

            //if (String.IsNullOrEmpty(tableName)) return;

            //command = new SqlCommand(updateObject.UpdateString(tableName), connection);
            //command.ExecuteNonQuery();
        }

        /// <summary>
        /// Обновление всех данных
        /// </summary>
        public static void UpdateAllData()
        {
            //подразделения
            var stringContent = new StringContent(JsonConvert.SerializeObject(departmentList),
                Encoding.UTF8, "application/json");
            var postResult = httpClient.PostAsync($"{endPoint}{Endpoint.updateDepartments}", stringContent).Result;

            //сотрудники
            stringContent = new StringContent(JsonConvert.SerializeObject(employeeList),
                Encoding.UTF8, "application/json");
            postResult = httpClient.PostAsync($"{endPoint}{Endpoint.updateEmployees}", stringContent).Result;
        }

        /// <summary>
        /// Вставка записи
        /// </summary>
        public static void InsertRecord(IDB insertObject)
        {
            string postAddress = String.Empty;
            if (insertObject is Department)
                postAddress = $"{endPoint}{Endpoint.insertDepartment}";
            if (insertObject is Employee)
                postAddress = $"{endPoint}{Endpoint.insertEmployee}";

            if (String.IsNullOrEmpty(postAddress)) return;
            Post(insertObject, postAddress);

            //временное решение, чтобы цеплялись id к новым записям
            departmentList.Clear();
            employeeList.Clear();
            FillLists();
        }

        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="deleteObject">Удаляемый объект</param>
        public static void DeleteRecord(IDB deleteObject)
        {
            string postAddress = String.Empty;
            if (deleteObject is Employee employee)
            {
                employeeList.Remove(employee);
                postAddress = $"{endPoint}{Endpoint.deleteEmployee}";
            }
            if (deleteObject is Department department)
            {
                departmentList.Remove(department);
                postAddress = $"{endPoint}{Endpoint.deleteDepartment}";
            }

            if (String.IsNullOrEmpty(postAddress)) return;
            Post(deleteObject, postAddress);
        }

        /// <summary>
        /// Выполение post-запроса
        /// </summary>
        /// <param name="postObject">Объект для отправки</param>
        /// <param name="postAddress">Адрес точки отправки</param>
        private static void Post(IDB postObject, string postAddress)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(postObject),
               Encoding.UTF8, "application/json");
            var postResult = httpClient.PostAsync(postAddress, stringContent).Result;
        }
    }
}
