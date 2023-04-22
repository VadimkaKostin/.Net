using Lab3.Contracts;
using Lab3.DataBases;
using Lab3.Http;
using Lab3.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public static class Program
    {
        private static readonly SingletonDataBase db = SingletonDataBase.Instace;

        public static async Task Main(string[] args)
        {
            ILoadBalancer loadBalancer = new LoadBalancer();

            loadBalancer.AddServer(new CustomServer("Server1", 50));
            loadBalancer.AddServer(new CustomServer("Server2", 45));
            loadBalancer.AddServer(new CustomServer("Server3", 55));
            loadBalancer.AddServer(new CustomServer("Server4", 50));
            loadBalancer.AddServer(new CustomServer("Server5", 50));

            IClient client = new Client(loadBalancer)
            {
                Name = "Vadim Kostin",
                Email = "vadkostinxm@gmail.com",
                DateOfBirth = Convert.ToDateTime("2003-09-26")
            };

            HttpBuilder httpBuilder = new HttpBuilder();

            while (true)
            {
                Task.Run(() => SendRequest(client));

                bool result = UpdateConsole();

                if (result)
                {
                    Console.WriteLine("Error! All servers are offline.");
                    break;
                }

                await Task.Delay(150);
            }

            Console.ReadLine();
        }

        public static async Task SendRequest(IClient client)
        {
            Response response = await client.SendRequestAsync();
        }
        public static bool UpdateConsole()
        {
            Console.Clear();

            int countOffline = 0;

            foreach(IServer server in db.GetAll())
            {
                Console.Write($"{server.Name}: ");

                if (server.IsOnline)
                    Console.WriteLine($"{server.CurrentLoad}/{server.MaxLoad}");
                else
                {
                    Console.WriteLine($"offline");
                    countOffline++;
                }
            }

            return countOffline == db.GetAll().Count();
        }
    }
}
