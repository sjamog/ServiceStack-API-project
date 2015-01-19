using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceInterface;

namespace WebApplication4
{
    public class HelloService : Service
    {
        public object Any(Hello request)
        {
            return new HelloResponse {Result = "Hello, " + request.Name};
        }
    }
}