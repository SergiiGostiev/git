using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeHierarchy
{
    public class Manager : EmployeeBase, ISubordinatesManager
    {
        public Manager(int empId, string empName, DateTime hireDate)
            : base(empId, empName, hireDate, 0.05m, 1.4m)
        {
        }

        public List<EmployeeBase> GetEmployees()
        {
            return employees;
        }

        public void AddEmployee(EmployeeBase newEmployee)
        {
            employees.Add(newEmployee);
        }

        public override decimal GetSalaryByDay(DateTime day)
        {
            if (!CheckEmployeeSalaryDictionary(empId, day))
            {
                decimal premium = YearsPremiumCalculation(day);
                decimal subSalaries = 0m;

                foreach (var emp in employees)
                {
                    subSalaries += emp.Salary;
                }

                SetEmployeeSalary(empId, day, Math.Round(((base.Salary * premium) + (0.005m * subSalaries)), 2));
                return GetEmployeeSalary(empId, day);
            }
            else
            {
                return GetEmployeeSalary(empId, day);
            }
        }

        public override decimal Salary
        {
            get
            {
                DateTime day = DateTime.Today;
                return GetSalaryByDay(day);
            }
        }
    }
}
