using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;

namespace WebApplication4
{
    public class VehicleService : Service
    {
        public MeasuredDataRepository MeasuredDataRepository { get; set; }

        public object Put(Vehicle request)
        {
            var id = MeasuredDataRepository.AddVehicle(request);
            Response.StatusCode = 200;
            return new VehicleResponse {Id = id};
        }

        public object Post(Vehicle request)
        {
            if (request.Make.IsEmpty() || request.Model.IsEmpty() || request.Year == default(int))
            {
                return new HttpResult(new {errorMessage = "That was a bad request"}, HttpStatusCode.BadRequest);
            }
            var id = MeasuredDataRepository.AddVehicle(request);
            Response.StatusCode = 200;
            return new VehicleResponse() {Id = id};
        }

        public object Get(VehicleById request)
        {
            var vehicle = MeasuredDataRepository.GetVehicleById(request.Id);
            Response.StatusCode = 200;
            return vehicle;
        }

        public object Delete(VehicleById request)
        {
            var id = MeasuredDataRepository.RemoveVehicle(request);
            return new VehicleResponse {Id = id};
        }

        public HttpResult Put(VehicleById request)
        {
            var id = MeasuredDataRepository.UpdateVehicle(request);
            Response.StatusCode = 210;
            //return new VehicleResponse {Id = id};
            return new HttpResult(new VehicleResponse {Id=id}, HttpStatusCode.OK);
        }
    }
}