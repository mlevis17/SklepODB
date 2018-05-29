using Perst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepDB
{
    class Client : Person
    {

#if USE_GENERICS
    internal Link<Order> orders;
#else
        internal Link orders;
#endif

        internal String phone;

        public override string Describe()
        {
            return "I am an client!";
        }

        public void ShowClients(Client[] clients)
        {
            for (int i = 0; i < clients.Length; i++)
            {
                Console.WriteLine(clients[i].name + '\t' + clients[i].address);
            }
        }
    }
}
