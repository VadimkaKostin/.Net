using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Http
{
    public class HttpBuilder
    {
        protected HttpMessage _root;

        public RequestBuilder Request()
        {
            return new RequestBuilder();
        }
        public ResponseBuilder Response()
        {
            return new ResponseBuilder();
        }
    }
    public class RequestBuilder : HttpBuilder
    {
        public RequestBuilder()
        {
            _root = new Request();
        }

        public RequestBuilder WithMethod(string method)
        {
            if(string.IsNullOrEmpty(method))
                throw new ArgumentNullException("method");

            if (method != "GET" && method != "POST")
                throw new ArgumentException("Method can supply only two values: GET and POST.");

            if(!string.IsNullOrEmpty(_root.Body) && method == "GET")
                throw new ArgumentException("Request with method GET cannot have request body.");

            (_root as Request).Method = method;
            return this;
        }
        public RequestBuilder WithHeader(string header)
        {
            if(string.IsNullOrEmpty(header))
                throw new ArgumentNullException("header");

            _root.Header = header;
            return this;
        }
        public RequestBuilder WithBody(string body)
        {
            if ((_root as Request).Method == "GET")
                throw new ArgumentException("Request with method GET cannot have request body.");

            _root.Body = body; 
            return this;
        }
        public RequestBuilder WithLoad(int load)
        {
            if(load < 0) 
                throw new ArgumentOutOfRangeException("load");

            (_root as Request).Load = load;
            return this;
        }
        public Request Build()
        {
            return _root as Request;
        }
    }
    public class ResponseBuilder : HttpBuilder
    {
        public ResponseBuilder()
        {
            _root = new Response();
        }

        public ResponseBuilder WithHeader(string header)
        {
            if(string.IsNullOrEmpty(header))
                throw new ArgumentNullException("header");

            _root.Header = header;
            return this;
        }
        public ResponseBuilder WithBody(string body)
        {
            if (string.IsNullOrEmpty(body))
                throw new ArgumentNullException("body");

            _root.Body = body;
            return this;
        }
        public ResponseBuilder WithStatusCode(int statusCode)
        {
            if (statusCode < 100 || statusCode > 599)
                throw new ArgumentException("statusCode");
            (_root as Response).StatusCode = statusCode;
            return this;
        }
        public Response Build()
        {
            return _root as Response;
        }
    }
}
