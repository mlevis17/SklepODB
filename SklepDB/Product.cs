using Perst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepDB
{
    class Product : Persistent
    {
#if USE_GENERICS
    internal Link<Order> orders;
#else
        internal Link orderPositions;
#endif

        internal String name;        
        internal double price;

        public void ShowEmployeesOfProduct(Product product)
        {
            for (int i = product.orderPositions.Length; --i >= 0;)
            {
                Console.WriteLine(((OrderPosition)product.orderPositions[i]).order.employee.name);
            }
        }

        public void ShowProducts(Product[] products)
        {
            for (int i = 0; i < products.Length; i++)
            {
                Console.WriteLine(products[i].name + '\t' + products[i].price);
            }
        }
    }
}
