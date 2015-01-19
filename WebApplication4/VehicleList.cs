using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;

namespace WebApplication4
{
    [Route("/vehicles/get")]
    [Route("/vehicles/get/{Year}", "GET")]
    public class VehicleListRequest : IReturn<VehicleListResponse>
    {
        public int Year { get; set; }
    }

    public class VehicleListResponse
    {
        public List<Vehicle> Vehicles { get; set; }
    }
}