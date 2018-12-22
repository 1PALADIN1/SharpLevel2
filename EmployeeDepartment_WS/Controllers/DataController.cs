using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections.ObjectModel;
using EmployeeDepartment_WS.Models;

namespace EmployeeDepartment_WS.Controllers
{
    public class DataController : ApiController
    {
        /// <summary>
        /// Получение списка сотрудников
        /// </summary>
        /// <returns>Возвращает список всех сотрудников</returns>
        [Route("getemplist")]
        public List<Employee> Get() => DataModel.EmployeeList;

        /// <summary>
        /// Получение списка подразделений
        /// </summary>
        /// <returns>Возвращает список всех подразделений</returns>
        [Route("getdepartlist")]
        public List<Department> GetDepartments() => DataModel.DepartmentList;
    }
}
