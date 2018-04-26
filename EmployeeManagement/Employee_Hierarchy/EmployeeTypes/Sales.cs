using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeHierarchy
{
    public class Sales : EmployeeBase, ISubordinatesManager
    {
        public Sales(int empId,string empName, DateTime hireDate)
            : base(empId, empName, hireDate, 0.01m, 1.35m)
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

        private decimal CalcAllLevelSubSalaries(ISubordinatesManager subordinate)
        {
            decimal sumSalary = 0m;
            var subList = subordinate.GetEmployees();
            if (subList.Count >= 1)
            {
                foreach (var sub in subList)
                {
                    sumSalary += sub.Salary;
                    ISubordinatesManager newSub = sub as ISubordinatesManager;
                    if (newSub != null)
                        sumSalary += CalcAllLevelSubSalaries(newSub);
                }
            }
            return sumSalary;
        }

        public override decimal GetSalaryByDay(DateTime day)
        {
            if (day < hireDate)
                throw new System.ArgumentException("Date for salary calculation can not be less than hire date");

            if (!CheckEmployeeSalaryDictionary(empId, day))
            {
                decimal premium = YearsPremiumCalculation(day);
                decimal subSalaries = 0m;

                foreach (var emp in employees)
                {
                    subSalaries += emp.Salary;
                    ISubordinatesManager newSub = emp as ISubordinatesManager;
                    if (newSub != null)
                        subSalaries += CalcAllLevelSubSalaries(newSub);
                }
                SetEmployeeSalary(empId, day, Math.Round(((base.Salary * premium) + (0.003m * subSalaries)), 2));

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
