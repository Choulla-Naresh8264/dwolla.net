using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Dwolla;
using Dwolla.SerializableTypes;

namespace dwolla.net.test
{
    [TestClass]
    public class CheckoutsTest
    {
        public Checkouts c = new Checkouts();

        /// <summary>
        /// We create a request, grab its info, then cancel it.
        /// </summary>
        [TestMethod]
        public void TestCreateInfoCancel()
        {
            var order = new PurchaseOrder();
            order.destinationId = "812-174-9528";
            order.total = 5.50;
            var url = c.Create(order, new Dictionary<string, object>{{"redirect", "http://www.google.com"}});
            Console.WriteLine(url);
        }
    }
}
