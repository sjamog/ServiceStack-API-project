using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace WebApplication4
{
    public class VehicleListService : Service
    {
        public MeasuredDataRepository MeasuredDataRepository { get; set; }
        public object Get(VehicleListRequest request)
        {
            if (request.Year != default(int))
            {
                var cacheYearKey = UrnId.Create<VehicleListRequest>(request.Year);
                return RequestContext.ToOptimizedResultUsingCache(base.Cache, cacheYearKey, () =>
                {
                    var list = MeasuredDataRepository.GetVehicles(request.Year, 0, 0, Session, this.GetSession());
                    return list;
                });
            }
            else if (request.Make != default(string))
            {
                if (request.Make.IsEmpty())
                {
                    return new HttpResult(new {errorMessage = "That was a bad request"}, HttpStatusCode.BadRequest);
                }
                
                var list = MeasuredDataRepository.GetVehiclesByMake(request.Make, 0, 0, Session, this.GetSession());
                return list;
            }
            else if (request.Model != default(string))
            {
                if (request.Model.IsEmpty())
                {
                    return new HttpResult(new { errorMessage = "That was a bad request" }, HttpStatusCode.BadRequest);
                }
                var list = MeasuredDataRepository.GetVehiclesByModel(request.Model, 0, 0, Session, this.GetSession());
                return list;
            }
            else
            {
                var list = MeasuredDataRepository.GetVehicles(request.Year, request.PageSize, request.PageNumber, Session, this.GetSession());
                return list;
            }
            
        }
    }
}