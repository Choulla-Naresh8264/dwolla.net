using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Dwolla;
using Dwolla.SerializableTypes;

namespace dwolla.net.test
{
    [TestClass]
    public class RequestsTest
    {
        public Requests r = new Requests();

        [TestMethod]
        public void TestCreate()
        {
            var result = r.Create("812-174-9528", 0.01, altToken: "J5GyKV4STxJJcVaJvdHXIOCojjEptVurmOtP8LaHk+Q8RGX6M7");
            Assert.IsInstanceOfType(result, typeof(int));
        }
    }
}
