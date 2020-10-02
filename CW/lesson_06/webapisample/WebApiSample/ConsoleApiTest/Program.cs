using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApiTest
{
    class Program
    {
        static async void Download()
        {
            HttpClient client = new HttpClient();

            string url = "http://localhost:49436/api/student";
            string result = (await ((await client.GetAsync(url)).Content).ReadAsStringAsync());
            Console.WriteLine(result);
        }
        static  void Main(string[] args)
        {
            Download();


            Thread.Sleep(5000);
        }
    }
}
