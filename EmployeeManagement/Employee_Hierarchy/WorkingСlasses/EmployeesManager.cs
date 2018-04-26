using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeHierarchy
{
    public class EmployeesManager
    {
        private List<EmployeeBase> employeesAll = new List<EmployeeBase>();

        public void AddEmployee(EmployeeBase newEmployee)
        {
            employeesAll.Add(newEmployee);
        }

        public decimal GetAllEmployeesSalary()
        {
            decimal totalSalary = 0m;
            foreach(var emp in employeesAll)
            {
                totalSalary += emp.Salary;
            }
            return totalSalary;
        }
    }
}
