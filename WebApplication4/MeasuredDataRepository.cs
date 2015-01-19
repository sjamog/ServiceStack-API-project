using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.CacheAccess;
using ServiceStack.OrmLite;
using ServiceStack.ServiceInterface.Auth;

namespace WebApplication4
{
    //IOC for the entry/status services
    //adds modularity, extensibility 
    public class MeasuredDataRepository
    {
        public IDbConnectionFactory DbConnectionFactory { get; set; }

        public int AddVehicle(Vehicle vehicle)
        {
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                db.CreateTable<Vehicle>();
                db.Insert(vehicle);
                return (int) db.GetLastInsertId();
            }
        }

        public int RemoveVehicle(VehicleId vehicleId)
        {
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                //db.Select<Vehicle>("Make = {0} AND Model = {1} AND Year = {2}", vehicle.Make,
                //    vehicle.Model, vehicle.Year);
                //db.Delete<Vehicle>("Make = {0} AND Model = {1} AND Year = {2}", vehicle.Make,
                //    vehicle.Model, vehicle.Year);
                db.DeleteById<Vehicle>(vehicleId.Id);
                return 1;
            }
        }

        public VehicleListResponse GetVehicles(int year, ISession session, IAuthSession authSession)
        {
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                if (year == -1)
                {
                    var list = db.Select<Vehicle>();
                    return new VehicleListResponse { Vehicles = list };
                }
                else
                {
                    var list = db.Select<Vehicle>(v => v.Year == year);
                    return new VehicleListResponse { Vehicles = list };
                }
                
                //Array vehicleList = db.Select<Vehicle>(v => v.Year == year).ToArray();
                
                //var sumTotal = db.Select<Entry>(e => e.EntryTime == dateFull.Date).Sum(e => e.Quantity);
                //return new StatusResponse { Goal = 500, Message = message, Total = sumTotal };
            }
        }

        public int AddEntry(Entry entry)
        {
            entry.EntryTime = entry.EntryTime.Date;
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                db.CreateTable<Entry>();
                db.Insert(entry);
                return (int)db.GetLastInsertId();
            }
        }
        public void AddEntry(DateTime time, ISession session, int quantity)
        {
            var date = time.Date;
            var measuredData = (MeasuredData) session[date.ToString()];
            if (measuredData == null)
            {
                measuredData = new MeasuredData() {Goal = 500};
            }
            measuredData.Total += quantity;
            session[date.ToString()] = measuredData;
        }

        public StatusResponse GetStatus(DateTime dateFull, ISession session, IAuthSession authSession)
        {
            var message = (authSession as AuthUserSession).TwitterScreenName;

            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                var sumTotal = db.Select<Entry>(e => e.EntryTime == dateFull.Date).Sum(e => e.Quantity);
                return new StatusResponse {Goal = 500, Message = message, Total = sumTotal};
            }
            //var date = dateFull.Date;
            //var measuredData = (MeasuredData) session[date.ToString()];
            //if (measuredData == null)
            //{
            //    measuredData = new MeasuredData() {Goal = 500, Total = 0};
            //}
            //var message = (authSession as AuthUserSession).TwitterScreenName;
            //return new StatusResponse() {Goal = measuredData.Goal, Total = measuredData.Total, Message = message};
        }
    }
}