using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;

namespace WebApplication4
{
    [Route("/status")]
    [Route("/status/{Date}")]
    [Authenticate]
    //[RequiredRole("User")]
    //[RequiredPermission("ViewCurrentStatus")]
    public class StatusRequest : IReturn<StatusResponse>
    {
        public DateTime Date { get; set; }
    }

    [NewestIPFilter(ApplyTo = ApplyTo.Patch)]
    public class StatusResponse
    {
        public int Total { get; set; }
        public int Goal { get; set; }
        public string Message { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }
}