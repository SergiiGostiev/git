using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeHierarchy
{
    interface ISubordinatesManager
    {
        List<EmployeeBase> GetEmployees();

        void AddEmployee(EmployeeBase newEmployee);
    }
}
