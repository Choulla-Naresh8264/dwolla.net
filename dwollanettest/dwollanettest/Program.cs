using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dwolla;

namespace dwollanettest
{
    class Program
    {
        static void Main(string[] args)
        {
            Dwolla.Properties.Settings.Default.access_token = "snip";
            var c = new Dwolla.Contacts();
            var result = c.Get();
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}
