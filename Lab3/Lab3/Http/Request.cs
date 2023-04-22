using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Http
{
    public class Request : HttpMessage
    {
        public string Method { get; set; }
        public int Load { get; set; }
    }
}
