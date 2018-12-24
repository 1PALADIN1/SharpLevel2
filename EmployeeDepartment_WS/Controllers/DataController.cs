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
        public List<Employee> GetEmployees() => DataModel.EmployeeList;

        /// <summary>
        /// Получение списка подразделений
        /// </summary>
        /// <returns>Возвращает список всех подразделений</returns>
        [Route("getdepartlist")]
        public List<Department> GetDepartments() => DataModel.DepartmentList;

        /// <summary>
        /// Обновление всех подразделений
        /// </summary>
        /// <param name="departments">Список подразделений</param>
        /// <returns>Возвращает статус OK при успешном добавлении</returns>
        [Route("updatedepart")]
        public HttpResponseMessage Post([FromBody] List<Department> departments)
        {
            if (DataModel.UpdateAllData(departments))
                return Request.CreateResponse(HttpStatusCode.OK);
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// Обновление всех сотрудников
        /// </summary>
        /// <param name="departments">Список сотрудников</param>
        /// <returns>Возвращает статус OK при успешном добавлении</returns>
        [Route("updateemp")]
        public HttpResponseMessage Post([FromBody] List<Employee> employees)
        {
            if (DataModel.UpdateAllData(employees))
                return Request.CreateResponse(HttpStatusCode.OK);
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
