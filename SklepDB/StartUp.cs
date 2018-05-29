using Perst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepDB
{
    public class StartUp
    {
        internal static System.String Input(System.String prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                String line = Console.ReadLine().Trim();
                if (line.Length != 0)
                {
                    return line;
                }
            }
        }

        static int InputInt(String prompt)
        {
            while (true)
            {
                try
                {
                    return Int32.Parse(Input(prompt));
                }
                catch (Exception) { }
            }
        }

        static double InputReal(String prompt)
        {
            while (true)
            {
                try
                {
                    return Double.Parse(Input(prompt));
                }
                catch (Exception) { }
            }
        }

        public static void Main(String[] args)
        {
            String name;
            Employee employee;
            Client client;
            Product product;
            Department department;
            Order order;
            OrderPosition orderPosition;
            Employee[] employees;
            Client[] clients;
            Product[] products;
            Department[] departments;
            Storage db = StorageFactory.Instance.CreateStorage();
            db.Open("testlist.dbs");
            Root root = (Root)db.Root;

            if (root == null)
            {
                root = new Root();
#if USE_GENERICS
            root.products = db.CreateFieldIndex<string,Product>("name", true);
            root.employees = db.CreateFieldIndex<string,Employee>("name", true);
#else
                root.products = db.CreateFieldIndex(typeof(Product), "name", true);
                root.employees = db.CreateFieldIndex(typeof(Employee), "name", true);
                root.clients = db.CreateFieldIndex(typeof(Client), "name", true);
                root.departments = db.CreateFieldIndex(typeof(Department), "name", true);
                root.orders = db.CreateFieldIndex(typeof(Order), "name", true);
#endif
                db.Root = root;
            }
            while (true)
            {
                Console.WriteLine("------------------------------------------");
                Console.WriteLine("1. Add employee");
                Console.WriteLine("2. Add client");
                Console.WriteLine("3. Add department");
                Console.WriteLine("4. Add product");
                Console.WriteLine("5. Add order");
                Console.WriteLine("6. Add order position");
                Console.WriteLine("7. Search employees");
                Console.WriteLine("8. Search clients");
                Console.WriteLine("9. Search departments");
                Console.WriteLine("10. Search products");
                Console.WriteLine("11. Employees of product");
                Console.WriteLine("12. Show order");
                Console.WriteLine("13. Show all employees and clients");
                Console.WriteLine("20. Exit");
                String str = Input("> ");
                int cmd;
                try
                {
                    cmd = Int32.Parse(str);
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid command");
                    continue;
                }
                switch (cmd)
                {
                    case 1:
                        employee = new Employee();
                        employee.name = Input("Employee name: ");
                        name = Input("Department name: ");
#if USE_GENERICS
                order.employee = root.employees[name];
#else
                        employee.department = (Department)root.departments[name];
#endif
                        if (employee.department == null)
                        {
                            Console.WriteLine("No such department");
                            continue;
                        }

                        employee.address = Input("Employee address: ");
                        employee.salary = InputReal("Employee salary: ");
#if USE_GENERICS
                employee.orders = db.CreateLink<Order>();
#else
                        employee.orders = db.CreateLink();
#endif
                        employee.department.employees.Add(employee);
                        employee.department.Store();

                        root.employees.Put(employee);
                        break;
                    case 2:
                        client = new Client();
                        client.name = Input("Client name: ");
                        client.address = Input("Client address: ");
                        client.phone = Input("Client phone: ");
#if USE_GENERICS
                client.orders = db.CreateLink<Order>();
#else
                        client.orders = db.CreateLink();
#endif
                        root.clients.Put(client);
                        break;
                    case 3:
                        department = new Department();
                        department.name = Input("Department name: ");
#if USE_GENERICS
                department.employees = db.CreateLink<Employee>();
#else
                        department.employees = db.CreateLink();
#endif
                        root.departments.Put(department);
                        break;
                    case 4:
                        product = new Product();
                        product.name = Input("Product name: ");
                        product.price = InputReal("Product price: ");

#if USE_GENERICS
                product.orders = db.CreateLink<Order>();
#else
                        product.orderPositions = db.CreateLink();
#endif
                        root.products.Put(product);
                        break;
                    case 5:
                        order = new Order();
                        order.name = Input("Order name: ");
                        name = Input("Employee name: ");
#if USE_GENERICS
                order.employee = root.employees[name];
#else
                        order.employee = (Employee)root.employees[name];
#endif
                        if (order.employee == null)
                        {
                            Console.WriteLine("No such employee");
                            continue;
                        }

                        name = Input("Client name: ");
#if USE_GENERICS
                order.Client = root.clients[name];
#else
                        order.client = (Client)root.clients[name];
#endif
                        if (order.client == null)
                        {
                            Console.WriteLine("No such client");
                            continue;
                        }
                                                
                        order.dateOrdered = DateTime.Now;
                        order.status = "Ordered";
#if USE_GENERICS
                order.orderPositions = db.CreateLink<OrderPosition>();
#else
                        order.orderPositions = db.CreateLink();
#endif

                        order.client.orders.Add(order);
                        order.employee.orders.Add(order);
                        order.client.Store();
                        order.employee.Store();

                        root.orders.Put(order);
                        break;
                    case 6:
                        orderPosition = new OrderPosition();
                        name = Input("Order name: ");
#if USE_GENERICS
                orderPosition.order = root.orders[name];
#else
                        orderPosition.order = (Order)root.orders[name];
#endif
                        if (orderPosition.order == null)
                        {
                            Console.WriteLine("No such order");
                            continue;
                        }

                        name = Input("Product name: ");
#if USE_GENERICS
                orderPosition.product = root.products[name];
#else
                        orderPosition.product = (Product)root.products[name];
#endif
                        if (orderPosition.product == null)
                        {
                            Console.WriteLine("No such product");
                            continue;
                        }

                        orderPosition.count = InputInt("Quantity of product: ");
                        orderPosition.order.orderPositions.Add(orderPosition);
                        orderPosition.product.orderPositions.Add(orderPosition);
                        orderPosition.order.Store();
                        orderPosition.product.Store();
                        break;
                    case 7:
                        name = Input("Employee name prefix: ");
#if USE_GENERICS
                employees = root.Employees.Get(new Key(name), new Key(name + (char)255, false));
#else
                        employees = (Employee[])root.employees.Get(new Key(name), new Key(name + (char)255, false));
#endif
                        if (employees.Length == 0)
                        {
                            Console.WriteLine("No such Employees found");
                        }
                        else
                        {
                            var instance = new Employee();
                            instance.ShowEmployees(employees);
                        }
                        continue;
                    case 8:
                        name = Input("Client name prefix: ");
#if USE_GENERICS
                clients = root.Client.Get(new Key(name), new Key(name + (char)255, false));
#else
                        clients = (Client[])root.clients.Get(new Key(name), new Key(name + (char)255, false));
#endif
                        if (clients.Length == 0)
                        {
                            Console.WriteLine("No such clients found");
                        }
                        else
                        {
                            var instance = new Client();
                            instance.ShowClients(clients);
                        }
                        continue;
                    case 9:
                        name = Input("Department name prefix: ");
#if USE_GENERICS
                clients = root.Client.Get(new Key(name), new Key(name + (char)255, false));
#else
                        departments = (Department[])root.departments.Get(new Key(name), new Key(name + (char)255, false));
#endif
                        if (departments.Length == 0)
                        {
                            Console.WriteLine("No such departments found");
                        }
                        else
                        {
                            var instance = new Department();
                            instance.ShowDepartments(departments);
                        }
                        continue;
                    case 10:
                        name = Input("Product name prefix: ");
#if USE_GENERICS
                products = root.products.Get(new Key(name), new Key(name + (char)255, false));
#else
                        products = (Product[])root.products.Get(new Key(name), new Key(name + (char)255, false));
#endif
                        if (products.Length == 0)
                        {
                            Console.WriteLine("No such products found");
                        }
                        else
                        {
                            var instance = new Product();
                            instance.ShowProducts(products);
                        }
                        continue;
                    case 11:
                        name = Input("Product name: ");
#if USE_GENERICS
                product = (Product)root.products[name];
#else
                        product = (Product)root.products[name];
#endif
                        if (product == null)
                        {
                            Console.WriteLine("No such product");
                        }
                        else
                        {
                            product.ShowEmployeesOfProduct(product);
                        }
                        continue;
                    case 12:
                        name = Input("      Order name: ");
#if USE_GENERICS
                order = (Order)root.orders[name];
#else
                        order = (Order)root.orders[name];
#endif
                        if (order == null)
                        {
                            Console.WriteLine("No such order");
                        }
                        else
                        {
                            order.ShowOrder(order);
                        }
                        continue;
                    case 13:                        
                        employees = (Employee[])root.employees.Get(new Key(""), new Key("" + (char)255, false));
                        clients = (Client[])root.clients.Get(new Key(""), new Key("" + (char)255, false));

                        if (employees.Length == 0)
                        {
                            Console.WriteLine("No employees found");
                        }
                        else
                        {
                            for (int i = 0; i < employees.Length; i++)
                            {
                                Console.WriteLine(employees[i].name + '\t' + employees[i].Describe());
                            }
                        }

                        if (clients.Length == 0)
                        {
                            Console.WriteLine("No clients found");
                        }
                        else
                        {
                            for (int i = 0; i < clients.Length; i++)
                            {
                                Console.WriteLine(clients[i].name + '\t' + clients[i].Describe());
                            }
                        }

                        continue;
                    case 20:
                        db.Close();
                        Console.WriteLine("End of session");
                        return;
                    default:
                        Console.WriteLine("Invalid command");
                        continue;
                }
                db.Commit();
            }
        }
    }
}
