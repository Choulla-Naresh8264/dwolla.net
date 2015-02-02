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
            dwolla.Properties.Settings.Default.access_token = "snip";
            var c = new dwolla.Contacts();
            var result = c.get();
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}
