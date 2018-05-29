using Perst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepDB
{
    class Employee : Person
    {

#if USE_GENERICS
    internal Link<Order> orders;
#else
        internal Link orders;
#endif

        internal Department department;

        internal double salary;

        public override string Describe()
        {
            return "I am an employee!";
        }

        public void RaiseSalary(double raise)
        {
            salary = salary + raise;
        }

        public void ShowEmployees(Employee[] employees)
        {
            for (int i = 0; i < employees.Length; i++)
            {
                Console.WriteLine(employees[i].name + '\t' + employees[i].address);
            }
        }
    }       
}
