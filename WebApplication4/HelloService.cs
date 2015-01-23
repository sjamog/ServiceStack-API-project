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
        public object Post(Hello request)
        {
            return new HelloResponse { Result = "Hello, " + request.Name };
        }

        public object Any(BatchHello request)
        {
            string nameString = "";
            for (int i=0; i < request.Hello.Count; i++)
            {
                nameString += request.Hello[i].Name.ToString();
            }
            return new HelloResponse {Result = "Hello, " + nameString};
        }
    }
}