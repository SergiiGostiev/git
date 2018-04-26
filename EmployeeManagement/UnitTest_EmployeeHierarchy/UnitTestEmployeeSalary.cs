using System;
using EmployeeHierarchy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest_EmployeeHierarchy
{
    [TestClass]
    public class UnitTestEmployeeSalary
    {
        [TestMethod]
        public void Salary_Calc_Employee()
        {
            /*
             *  base salary = 5000	
             *  worked years = 8	
             *  premium rate = 8 * 0.03 = 0.24	
             *  total salary = 5000 * 1.24 = 6200
             */
            decimal expected = 6200;
            Employee employee1 = new Employee(1, "employee1", new DateTime(2010, 1, 1));
            decimal actual = employee1.Salary;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Salary_Calc_Manager()
        {
            /*
             *  base salary = 5000	
             *  worked years = 13	
             *  premium rate = 13 * 0.05 = 0.65 = 0.4
             *  salary with year premium = 5000 * 1.4 = 7000
             *  total salary of direct subordinates: sales2 = 5118.6
             *  surcharge for direct subordinates = 5118.6 * 0.5% = 25.59
             *  total salary = 7000 + 25.59 = 7025.59
             */

            decimal expected = 7025.59m;
            Employee employee1 = new Employee(1, "employee1", new DateTime(2010, 1, 1));
            Sales sales2 = new Sales(2,"sales2", new DateTime(2016, 1, 1));
            Manager manager1 = new Manager(3, "manager1", new DateTime(2005, 1, 1));
            sales2.AddEmployee(employee1);
            manager1.AddEmployee(sales2);

            decimal actual = manager1.Salary;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Salary_Calc_Sales()
        {
            /*
             *  base salary = 5000	
             *  worked years = 18	
             *  premium rate = 18 * 0.05 = 0.18
             *  salary with year premium = 5000 * 1.18 = 5900
             *  total salary of all subordinates: 6200 (employee1) + 5118.6 (sales2) + 7025.59 (manager1) = 18344.19
             *  surcharge for all subordinates = 18344.19 * 0.3% = 55.03
             *  total salary = 5900 + 55.03 = 5955.03
             */

            decimal expected = 5955.03m;
            Employee employee1 = new Employee(1, "employee1", new DateTime(2010, 1, 1));
            Sales sales2 = new Sales(2, "sales2", new DateTime(2016, 1, 1));
            Manager manager1 = new Manager(3, "manager1", new DateTime(2005, 1, 1));
            Sales sales1 = new Sales(4, "sales1", new DateTime(2000, 1, 1));


            sales2.AddEmployee(employee1);
            manager1.AddEmployee(sales2);
            sales1.AddEmployee(manager1);

            decimal actual = sales1.Salary;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Salary_Calc_Employee_By_Specific_Date()
        {
            /*
             *  base salary = 5000	
             *  worked years = 0	
             *  premium rate = 0 * 0.03 = 0	
             *  total salary = 5000 
             */
            decimal expected = 5000;
            Employee employee1 = new Employee(1, "employee1", new DateTime(2010, 1, 1));
            decimal actual = employee1.GetSalaryByDay(new DateTime(2010, 2, 1));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Calculate_Total_Salary_For_All_Employees()
        {

            /*
             *  employee1 = 6200
             *  manager1 = 7025.59
             *  sales1 = 5955.03
             *  sales2 = 5118.6
             *  total = 24299.23
             */

            decimal expected = 24299.22m;

            EmployeesManager employeesManager = new EmployeesManager();
           
            Employee employee1 = new Employee(1, "employee1", new DateTime(2010, 1, 1));
            Sales sales2 = new Sales(2, "sales2", new DateTime(2016, 1, 1));
            Manager manager1 = new Manager(3, "manager1", new DateTime(2005, 1, 1));
            Sales sales1 = new Sales(4, "sales1", new DateTime(2000, 1, 1));

            sales2.AddEmployee(employee1);
            manager1.AddEmployee(sales2);
            sales1.AddEmployee(manager1);

            employeesManager.AddEmployee(employee1);
            employeesManager.AddEmployee(sales2);
            employeesManager.AddEmployee(manager1);
            employeesManager.AddEmployee(sales1);

            decimal actual = employeesManager.GetAllEmployeesSalary();
            Assert.AreEqual(expected, actual);
        }

    }
}
