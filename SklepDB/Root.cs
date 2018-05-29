using Perst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepDB
{
      
    class Root : Persistent
    {
#if USE_GENERICS
    internal FieldIndex<string,Product> products;
    internal FieldIndex<string,Employee> employees;
    internal FieldIndex<string,Client> clients;
    internal FieldIndex<string,Department> departments;
    internal FieldIndex<string,Order> orders;
    internal FieldIndex<string,OrderPosition> orderPositions;
#else
        internal FieldIndex products;
        internal FieldIndex employees;
        internal FieldIndex clients;
        internal FieldIndex departments;
        internal FieldIndex orders;
        internal FieldIndex orderPositions;
#endif
    }

    
}
