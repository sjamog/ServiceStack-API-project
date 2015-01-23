using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace WebApplication4
{
    [Route("/vehicles", "GET")]
    [Route("/vehicles/year/{Year}", "GET")]
    [Route("/vehicles/make/{Make}", "GET")]
    [Route("/vehicles/model/{Model}", "GET")]
    [Route("/vehicles/{PageSize}/{PageNumber}", "GET")]
    public class VehicleListRequest : IReturn<VehicleListResponse>
    {
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }

    public class VehicleListResponse
    {
        public List<Vehicle> Vehicles { get; set; }
    }
}