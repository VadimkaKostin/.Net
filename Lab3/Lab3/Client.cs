using Lab3.Contracts;
using Lab3.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class Client : IClient
    {
        private readonly ILoadBalancer _loadBalancer;
        private readonly HttpBuilder _httpBuilder;

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }

        public Client(ILoadBalancer loadBalancer)
        {
            _loadBalancer = loadBalancer;

            _httpBuilder = new HttpBuilder();

            Id = Guid.NewGuid();
        }

        public async Task<Response> SendRequestAsync()
        {

            Request request = _httpBuilder.Request()
                .WithHeader($"Url: random url\n Time: {DateTime.Now}")
                .WithMethod("POST")
                .WithBody($"Id: {Id}\nName: {Name}\nEmail:{Email}\nDate of birth:{DateOfBirth}")
                .WithLoad(5)
                .Build();

            Response response = await _loadBalancer.HandleRequest(request);

            return response;
        }
    }
}
