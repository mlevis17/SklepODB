using Perst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepDB
{
    class OrderPosition : Persistent
    {
        internal Order order;
        internal Product product;

        internal int count;

        public double SumOrderPositionPrice()
        {
            return count * product.price;
        }
    }
}
