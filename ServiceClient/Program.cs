using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceInterface.ServiceModel;
using WebApplication4;

namespace ServiceClient
{
    class Program
    {
        static void Main(string[] args)
        {
            int quantity = -1;
            var client = new JsonServiceClient("http://localhost:2200/") { UserName = "muser", Password = "pass123" };
            client.Send<AssignRolesResponse>(new AssignRoles()
            {
                UserName = "muser",
                Roles = new ArrayOfString("User"),
                Permissions = new ArrayOfString("ViewCurrentStatus")
            });
            do
            {
                Console.WriteLine("Enter lines of code (0 to exit)");
                quantity = int.Parse(Console.ReadLine());
                client.SendAsync(new Entry { Quantity = quantity, EntryTime = DateTime.Now } , 
                    queryResponse => Console.WriteLine("Response: " + queryResponse.Id), 
                    (queryResponse, exception) => Console.WriteLine("Request Error"));
                //var response = client.Send(new Entry { Quantity = quantity, EntryTime = DateTime.Now });
                //Console.WriteLine("Response: " + response.Id);
            } while (quantity != 0);
            //var postResponse = client.Post<StatusResponse>("status", new StatusRequest() { Date = DateTime.Now });
            StatusResponse postResponse = null;
            //postResponse = client.Post<StatusResponse>("status", new StatusRequest { Date = DateTime.Now });
            try
            {
                postResponse = client.Patch<StatusResponse>("status", new StatusRequest {Date = DateTime.Now});
                //postResponse = client.Post<StatusResponse>("status", new StatusRequest { Date = DateTime.Now });
            }
            catch (WebServiceException wse)
            {
                Console.WriteLine(wse.ErrorMessage);
            }
            Console.WriteLine("{0} of {1} achieved", postResponse.Total, postResponse.Goal);
            Console.WriteLine(postResponse.Message);
            Console.ReadLine();
        }
    }
}
