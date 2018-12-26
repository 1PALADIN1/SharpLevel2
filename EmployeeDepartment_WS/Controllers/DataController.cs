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
        /// <returns>Возвращает статус OK при успешном обновлении</returns>
        [Route("updatedepartlist")]
        public HttpResponseMessage Post([FromBody] List<Department> departments)
        {
            if (DataModel.UpdateAllData(departments))
                return Request.CreateResponse(HttpStatusCode.OK);
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// Обновление всех сотрудников
        /// </summary>
        /// <param name="employees">Список сотрудников</param>
        /// <returns>Возвращает статус OK при успешном обновлении</returns>
        [Route("updateemplist")]
        public HttpResponseMessage Post([FromBody] List<Employee> employees)
        {
            if (DataModel.UpdateAllData(employees))
                return Request.CreateResponse(HttpStatusCode.OK);
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// Вставка нового подразделения
        /// </summary>
        /// <param name="insertDepart">Подразделение</param>
        /// <returns>Возвращает статус OK при успешном добавлении</returns>
        [Route("insertdepart")]
        public HttpResponseMessage Post([FromBody] Department insertDepart)
        {
            if (DataModel.InsertRecord(insertDepart))
                return Request.CreateResponse(HttpStatusCode.OK);
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// Вставка нового сотрудника
        /// </summary>
        /// <param name="insertEmployee">Сотрудник</param>
        /// <returns>Возвращает статус OK при успешном добавлении</returns>
        [Route("insertemployee")]
        public HttpResponseMessage Post([FromBody] Employee insertEmployee)
        {
            if (DataModel.InsertRecord(insertEmployee))
                return Request.CreateResponse(HttpStatusCode.OK);
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// Удаление подразделения
        /// </summary>
        /// <param name="department">Подразделение</param>
        /// <returns>Возвращает статус OK при успешном удалении</returns>
        [Route("deletedepart")]
        public HttpResponseMessage PostDelete([FromBody] Department department)
        {
            if (DataModel.DeleteRecord(department))
                return Request.CreateResponse(HttpStatusCode.OK);
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        /// <param name="employee">Сотрудник</param>
        /// <returns>Возвращает статус OK при успешном удалении</returns>
        [Route("deleteemp")]
        public HttpResponseMessage PostDelete([FromBody] Employee employee)
        {
            if (DataModel.DeleteRecord(employee))
                return Request.CreateResponse(HttpStatusCode.OK);
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// Обновление единичной записи сотрудника
        /// </summary>
        /// <param name="employee">Сотрудник</param>
        /// <returns>Возвращает статус OK при успешном обновлении</returns>
        [Route("updateemp")]
        public HttpResponseMessage PostUpdate([FromBody] Employee employee)
        {
            if (DataModel.UpdateRecord(employee))
            {
                DataModel.RefreshLists();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
