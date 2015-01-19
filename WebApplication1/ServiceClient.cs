using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceClient.Web;
using WebApplication4;

namespace ServiceClient
{
    public class Program
    {
        static void Main(string[] args)
        {
            int quantity = -1;
            var client = new JsonServiceClient("http://localhost:9812/");
            do
            {
                Console.WriteLine("Enter lines of code (0 to exit)");
                quantity = int.Parse(Console.ReadLine());
                var response = client.Send<EntryResponse>(new Entry {Quantity = quantity, EntryTime = DateTime.Now});
                Console.WriteLine("Response: " + response.Id);
            } while (quantity != 0);
            var postResponse = client.Post<StatusResponse>("status", new StatusRequest() {Date = DateTime.Now});
            Console.WriteLine("{0} of {1} achieved", postResponse.Total, postResponse.Goal);
            Console.ReadLine();
        }
    }
}