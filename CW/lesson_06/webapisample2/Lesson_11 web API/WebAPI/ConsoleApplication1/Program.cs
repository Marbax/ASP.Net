using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }

    }
    class Program
    {
        static void Main(string[] args)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:27618/api/Product");
            string answer = string.Empty;
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    answer = rd.ReadToEnd();
                }
            }

            Console.WriteLine(answer);
            var servicedata = JsonConvert.DeserializeObject<IEnumerable<Product>>(answer, 
                   new IsoDateTimeConverter { DateTimeFormat = "dd.MM.yyyy" });
            foreach (var item in servicedata)
            {
                Console.WriteLine($"{item.Name} {item.Price}");
            }
        }
    }
}
