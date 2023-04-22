using Lab3.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Contracts
{
    public interface IClient : IIdentifiable
    {
        string Name { get; set; }
        string Email { get; set; }
        DateTime DateOfBirth { get; set; }

        Task<Response> SendRequestAsync();
    }
}
