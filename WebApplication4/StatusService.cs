using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;

namespace WebApplication4
{
    public class StatusService : Service
    {
        public MeasuredDataRepository MeasuredDataRepository { get; set; }
        public object Any(StatusRequest request)
        {
            var cacheDateKey = UrnId.Create<StatusRequest>(request.Date.ToShortDateString());
            return RequestContext.ToOptimizedResultUsingCache(base.Cache, cacheDateKey, () =>
            {
                var status = MeasuredDataRepository.GetStatus(request.Date, Session, this.GetSession());
                return status;
            });

            //var date = request.Date.Date;
            //var measuredData = (MeasuredData) Session[date.ToString()];
            //if (measuredData == null)
            //{
            //    measuredData = new MeasuredData { Goal = 500, Total = 0 };
            //}
            //var message = (this.GetSession() as AuthUserSession).TwitterScreenName;
            ////var message = session.TwitterScreenName;
            ////var message = this.GetSession().DisplayName;
            //return new StatusResponse() {Goal = measuredData.Goal, Total = measuredData.Total, Message = message};
        }
    }
}