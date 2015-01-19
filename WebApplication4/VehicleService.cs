using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceInterface;

namespace WebApplication4
{
    public class VehicleService : Service
    {
        public MeasuredDataRepository MeasuredDataRepository { get; set; }

        public object Any(Vehicle request)
        {
            var id = MeasuredDataRepository.AddVehicle(request);
            return new VehicleResponse {Id = id};
        }

        public object Any(VehicleId request)
        {
            var id = MeasuredDataRepository.RemoveVehicle(request);
            return new VehicleResponse {Id = id};
        }
    }
}