using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.CacheAccess;
using ServiceStack.OrmLite;
using ServiceStack.ServiceInterface.Auth;

namespace WebApplication4
{
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

        public int RemoveVehicle(VehicleById vehicleById)
        {
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                db.DeleteById<Vehicle>(vehicleById.Id);
                return 1;
            }
        }

        public int UpdateVehicle(VehicleById vehicle)
        {
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                db.Update<Vehicle>(vehicle, v => v.Id == vehicle.Id);
                return 1;
            }
        }

        public VehicleListResponse GetVehicles(int year, int pageSize, int pageNumber, ISession session, IAuthSession authSession)
        {
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                if (year == default(int))
                {
                    //var list = db.Select<Vehicle>();
                    var list = db.Select<Vehicle>(q => q.Limit(skip:pageSize*(pageNumber-1), rows:pageSize));
                    return new VehicleListResponse { Vehicles = list };
                }
                else
                {
                    var list = db.Select<Vehicle>(v => v.Year == year);
                    return new VehicleListResponse { Vehicles = list };
                }
            }
        }

        public VehicleListResponse GetVehicleById(int id)
        {
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                var vehicle = db.Select<Vehicle>(v => v.Id == id);
                return new VehicleListResponse { Vehicles = vehicle };
            }
        }

        public VehicleListResponse GetVehiclesByMake(string make, int pageSize, int pageNumber, ISession session, IAuthSession authSession)
        {
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                var list = db.Select<Vehicle>(v => v.Make == make);
                return new VehicleListResponse { Vehicles = list };
            }
        }

        public VehicleListResponse GetVehiclesByModel(string model, int pageSize, int pageNumber, ISession session, IAuthSession authSession)
        {
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                var list = db.Select<Vehicle>(v => v.Model == model);
                return new VehicleListResponse { Vehicles = list };
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