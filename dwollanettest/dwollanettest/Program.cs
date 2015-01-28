using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dwolla;

namespace dwollanettest
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> doodles = new Dictionary<string, string>(){
            {"client_id", "abcd"}, {"client_secret", "dongs"}};
            var client = new dwolla.net.Rest();
            Console.WriteLine(client.get("/contacts", doodles).Result);
            Console.ReadLine();
        }
    }
}
