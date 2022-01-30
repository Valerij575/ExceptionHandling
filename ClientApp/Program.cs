using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string uri = "http://localhost:49315/api/employees/12345";

            using (WebClient client = new WebClient())
            {
                var response = client.DownloadString(uri);
                Console.WriteLine(response);
            }

            Console.ReadKey();
        }
    }
}
