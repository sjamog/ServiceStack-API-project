using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.DataAnnotations;
using ServiceStack.ServiceHost;

namespace WebApplication4
{
    [Route("/vehicles/add", "POST")]
    [Route("/vehicles/add/{Make}/{Model}/{Year}")]
    public class Vehicle : IReturn<VehicleResponse>
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
    }

    [Route("/vehicles/remove", "POST")]
    [Route("/vehicles/remove/{Id}")]
    public class VehicleId : IReturn<VehicleResponse>
    {
        public int Id { get; set; }
    }

    public class VehicleResponse
    {
        public int Id { get; set; }
    }
}