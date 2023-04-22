using Lab3.Contracts;
using Lab3.DataBases;
using Lab3.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class LoadBalancer : ILoadBalancer
    {
        private readonly SingletonDataBase _dataBase;

        public LoadBalancer()
        {
            _dataBase = SingletonDataBase.Instace;
        }

        public void AddServer(IServer server)
        {
            _dataBase.Insert(server);
        }
        public void RemoveServer(IServer server)
        {
            _dataBase.Delete(server.Id);
        }
        public IServer GetOptimalServer(int load)
        {
            List<IServer> servers = _dataBase.GetAll()
                .Where(server => server.CanHandleRequest(load))
                .ToList();

            if(servers.Count == 0)
                return null;

            int min = servers.Min(server => server.CurrentLoad);

            IServer optimalServer = servers.FirstOrDefault(server => server.CurrentLoad == min);

            return optimalServer;
        }
        public async Task<Response> HandleRequest(Request request)
        {
            IServer server = this.GetOptimalServer(request.Load);

            if (server == null)
                return null;

            Response response = await server.HandleRequest(request);

            return response;
        }
    }
}
