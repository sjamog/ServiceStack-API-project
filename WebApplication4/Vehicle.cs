using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.DataAnnotations;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace WebApplication4
{
    [Route("/vehicles", "POST")]
    [Route("/vehicles/{Make}/{Model}/{Year}", "POST")]
    [Route("/vehicles/{Make}/{Model}/{Year}", "GET")]
    [Authenticate]
    public class Vehicle : IReturn<VehicleResponse>
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
    }

    //[Route("/vehicles/remove", "POST")]
    [Route("/vehicles/{Id}", "GET")]
    [Route("/vehicles/{Id}", "DELETE")]
    [Route("/vehicles/{Id}", "PUT")]
    [Route("/vehicles/{Id}/{Make}/{Model}/{Year}", "PUT")]
    [Authenticate]
    public class VehicleById : IReturn<VehicleResponse>
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
    }

    public class VehicleResponse
    {
        public int Id { get; set; }
    }
}