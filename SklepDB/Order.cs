using Perst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepDB
{
    class Order : Persistent
    {

#if USE_GENERICS
    internal Link<OrderPosition> orderPositions;
#else
        internal Link orderPositions;
#endif

        internal Employee employee;
        internal Client client;

        internal string name;
        internal double totalPrice;
        internal string status;
        internal DateTime dateOrdered;

        public void ChangeStatus(string newStatus)
        {
            status = newStatus;
        }

        public void ShowOrder(Order order)
        {
            double orderCost = 0;
            for (int i = order.orderPositions.Length; --i >= 0;)
            {
                Console.WriteLine(((OrderPosition)order.orderPositions[i]).product.name +
                    ": quantity = " + ((OrderPosition)order.orderPositions[i]).count +
                    " with total price = " + ((OrderPosition)order.orderPositions[i]).SumOrderPositionPrice());

                orderCost += ((OrderPosition)order.orderPositions[i]).SumOrderPositionPrice();
            }
            Console.WriteLine("     Total order cost: " + orderCost);
        }
    }
}
