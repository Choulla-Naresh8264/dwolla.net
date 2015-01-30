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
            dwolla.Properties.Settings.Default.access_token = "Mmxr4BAsDduy7FiA43hM1wjArJe8JWnOljwYXmNmApl+reG38r";
            var bullshit = new dwolla.Contacts();
            var result = bullshit.get();
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}
