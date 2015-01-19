using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.DataAnnotations;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace WebApplication4
{
    [Route("/entry", "POST")]
    [Route("/entry/{Quantity}/{EntryTime}")]
    [GetIPFilter]
    [Authenticate]
    public class Entry : IReturn<EntryResponse>
    {
        [AutoIncrement]
        public int Id { get; set; }
        public DateTime EntryTime { get; set; }
        public int Quantity { get; set; }
    }

    public class EntryResponse
    {
        public int Id { get; set; }
    }
}