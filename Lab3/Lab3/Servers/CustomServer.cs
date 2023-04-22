using Lab3.Contracts;
using Lab3.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Servers
{
    public class CustomServer : IServer
    {
        private readonly HttpBuilder _httpBuilder;

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int CurrentLoad { get; set; }
        public int MaxLoad { get; set ; }
        public bool IsOnline { get; set; }

        public CustomServer(string name, int maxLoad)
        {
            Id = Guid.NewGuid();
            Name = name;
            CurrentLoad = 0;
            MaxLoad = maxLoad;
            IsOnline = true;

            _httpBuilder = new HttpBuilder();
        }

        public bool CanHandleRequest(int requestLoad)
        {
            return IsOnline && (CurrentLoad + requestLoad) <= MaxLoad;
        }

        public async Task<Response> HandleRequest(Request request)
        {
            CurrentLoad += request.Load;

            int duration = new Random().Next(7, 11);

            //Опрацювання запиту
            await Task.Delay(duration * 1000);

            if (CurrentLoad == MaxLoad)
            {
                IsOnline = false;
            }
            else
                CurrentLoad -= request.Load;

            Response response = 
                _httpBuilder.Response()
                            .WithHeader(DateTime.Now.ToString())
                            .WithBody($"Response from \'{Name}\'")
                            .WithStatusCode(200)
                            .Build();

            return response;
        }
    }
}
