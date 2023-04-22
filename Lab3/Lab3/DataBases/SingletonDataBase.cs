using Lab3.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.DataBases
{
    public class SingletonDataBase : IDataBase<IServer>
    {
        private static Lazy<SingletonDataBase> _instance = 
            new Lazy<SingletonDataBase>(() => new SingletonDataBase());
        public static SingletonDataBase Instace => _instance.Value;

        private List<IServer> _servers;

        private SingletonDataBase()
        {
            _servers = new List<IServer>();
        }
        
        public void Insert(IServer value)
        {
            _servers.Add(value);
        }
        public IServer GetValueById(Guid guid)
        {
            IServer serverSearched = _servers.FirstOrDefault(server => server.Id == guid);

            return serverSearched;
        }
        public IEnumerable<IServer> GetAll()
        {
            return _servers.Select(server => server);
        }
        public void Update(IServer value)
        {
            IServer server = GetValueById(value.Id);

            if (server == null)
                return;

            foreach(var prop in server.GetType().GetProperties())
            {
                prop.SetValue(server, prop.GetValue(value));
            }
        }
        public bool Delete(Guid guid)
        {
            IServer server = GetValueById(guid);

            if (server == null)
                return false;

            return _servers.Remove(server);
        }
    }
}
