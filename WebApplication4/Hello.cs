using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace WebApplication4
{

    [Route("/hello", "GET")]
    [Route("/hello/{Name}")]
    [Authenticate]
    public class Hello
    {
        public string Name { get; set; }
    }

    [Route("/hello/batch")]
    public class BatchHello
    {
        public List<Hello> Hello { get; set; }
    }

}