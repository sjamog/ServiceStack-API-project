using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.Common;
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
                    var list = MeasuredDataRepository.GetVehicles(request.Year, Session, this.GetSession());
                    return list;
                });
            }
            else
            {
                var list = MeasuredDataRepository.GetVehicles(-1, Session, this.GetSession());
                return list;
            }
            
        }
    }
}