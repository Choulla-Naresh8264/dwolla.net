using System;
using System.Collections.Generic;
using Dwolla;
using Dwolla.SerializableTypes;

namespace dwollanettest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var c = new Contacts();
            c.C.access_token = "OLJVhTXgrbRjnS546gWtgNyFXjkQoP21Jp3wP7wY1hCJzbJZmG";
            List<Contact> result = c.Get();
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}