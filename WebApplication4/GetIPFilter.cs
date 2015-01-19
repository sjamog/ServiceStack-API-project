using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.CacheAccess;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace WebApplication4
{
    public class GetIPFilter : RequestFilterAttribute
    {
        public ICacheClient Cache { get; set; }

        public override void Execute(IHttpRequest req, IHttpResponse res, object requestDto)
        {
            Cache.Add("newest ip", req.UserHostAddress);
        }
    }

    public class NewestIPFilter : ResponseFilterAttribute
    {
        public ICacheClient Cache { get; set; }

        public override void Execute(IHttpRequest req, IHttpResponse res, object responseDto)
        {
            var status = responseDto as StatusResponse;
            if (status != null)
            {
                status.Message += "Newest IP: " + Cache.Get<string>("newest ip");
            }
        }
    }
}