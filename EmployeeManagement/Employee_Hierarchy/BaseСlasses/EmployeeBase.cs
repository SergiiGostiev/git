using System;
using System.Collections.Generic;

namespace EmployeeHierarchy
{
    public abstract class EmployeeBase
    {
        protected int empId;
        protected string empName;
        protected DateTime hireDate;
        protected const decimal baseSalary = 5000;
        protected decimal yearRate;
        protected decimal maxRate;
        protected List<EmployeeBase> employees = new List<EmployeeBase>();
        private static Dictionary<int, Dictionary<DateTime, decimal>> employeeSalary = new Dictionary<int, Dictionary<DateTime, decimal>>();

        public EmployeeBase(int empId, string empName, DateTime hireDate, decimal yearRate, decimal maxRate)
        {
            this.empId = empId;
            this.empName = empName;
            this.hireDate = hireDate;
            this.yearRate = yearRate;
            this.maxRate = maxRate;
        }

        protected bool CheckEmployeeSalaryDictionary(int empId, DateTime day)
        {
            return employeeSalary.ContainsKey(empId) && employeeSalary[empId].ContainsKey(day);               
        }

        protected decimal GetEmployeeSalary(int empId, DateTime day)
        {
            return employeeSalary[empId][day];
        }

        protected void SetEmployeeSalary(int empId, DateTime day, decimal salary)
        {
            if(employeeSalary.ContainsKey(empId))
            {
                employeeSalary[empId].Add(day, salary);
            }
            else
            {
                employeeSalary[empId] = new Dictionary<DateTime, decimal>();
                employeeSalary[empId].Add(day, salary);
            }              
        }

        public virtual decimal Salary
        {
            get
            {
                return baseSalary;
            }
        }

        public abstract decimal GetSalaryByDay(DateTime day);

        public int GetWorkedYears(DateTime day)
        {
            int workedYears = day.Year - hireDate.Year;
            if (day.Month < hireDate.Month ||
                (day.Month == hireDate.Month && day.Day < hireDate.Day)) workedYears--;
            return workedYears;
        }

        protected decimal YearsPremiumCalculation(DateTime day)
        {
            int workedYears = GetWorkedYears(day);
            decimal premium = workedYears >= 1 ? 1 + (workedYears * yearRate) : 1;
            return premium > maxRate ? maxRate : premium;
        }
    }
}
