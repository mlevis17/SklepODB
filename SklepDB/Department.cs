using Perst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepDB
{
    class Department : Persistent
    {
#if USE_GENERICS
    internal Link<Employee> employees;
#else
        internal Link employees;
#endif

        internal String name;

        public void ShowDepartments(Department[] departments)
        {
            for (int i = 0; i < departments.Length; i++)
            {
                Console.WriteLine(departments[i].name);
            }
        }
    }
}
