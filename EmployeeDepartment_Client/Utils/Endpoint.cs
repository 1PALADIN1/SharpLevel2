using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeWPF.Utils
{
    public static class Endpoint
    {
        public static readonly string getEmployees = "getemplist";
        public static readonly string getDepartments = "getdepartlist";
        public static readonly string updateDepartments = "updatedepartlist";
        public static readonly string updateEmployees = "updateemplist";
        public static readonly string insertDepartment = "insertdepart";
        public static readonly string insertEmployee = "insertemployee";
        public static readonly string deleteDepartment = "deletedepart";
        public static readonly string deleteEmployee = "deleteemp";
        public static readonly string updateEmployee = "updateemp";
    }
}
