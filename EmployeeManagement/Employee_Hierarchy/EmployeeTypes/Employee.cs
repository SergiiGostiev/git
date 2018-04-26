using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeHierarchy
{
    public class Employee : EmployeeBase
    {
        public Employee(int empId, string empName, DateTime hireDate)
            :base(empId, empName, hireDate, 0.03m, 1.3m)
        {
        }

        public override decimal GetSalaryByDay(DateTime day)
        {
            if (!CheckEmployeeSalaryDictionary(empId, day))
            {
                decimal premium = YearsPremiumCalculation(day);
                SetEmployeeSalary(empId, day, Math.Round(base.Salary * premium, 2));
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
