using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceInterface;

namespace WebApplication4
{
    public class EntryService : Service
    {
        public MeasuredDataRepository MeasuredDataRepository { get; set; }

        public object Any(Entry request)
        {
            var id = MeasuredDataRepository.AddEntry(request);
            return new EntryResponse {Id = id};
        }
        public object Post(Entry request)
        {
            MeasuredDataRepository.AddEntry(request.EntryTime, Session, request.Quantity);
            return new EntryResponse {Id = 1};
            //var date = request.EntryTime.Date;
            //var measuredData = (MeasuredData) Session[date.ToString()];
            //if (measuredData == null)
            //{
            //    measuredData = new MeasuredData {Goal = 500};
            //}
            //measuredData.Total += request.Quantity;
            //Session[date.ToString()] = measuredData;
            //return new EntryResponse {Id = 1};
        }
    }

    public class MeasuredData
    {
        public int Total { get; set; }
        public int Goal { get; set; }
    }
}