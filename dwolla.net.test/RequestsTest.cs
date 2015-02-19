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

        /// <summary>
        /// We create a request, grab its info, then cancel it. s
        /// </summary>
        [TestMethod]
        public void TestCreateInfoCancel()
        {
            var create = r.Create("812-174-9528", 0.01, altToken: "J5GyKV4STxJJcVaJvdHXIOCojjEptVurmOtP8LaHk+Q8RGX6M7");
            Assert.IsInstanceOfType(create, typeof(int));

            var info = r.Info(create.ToString(), altToken: "J5GyKV4STxJJcVaJvdHXIOCojjEptVurmOtP8LaHk+Q8RGX6M7");
            Assert.IsInstanceOfType(info, typeof(Request));

            var cancel = r.Cancel(create.ToString(), altToken: "J5GyKV4STxJJcVaJvdHXIOCojjEptVurmOtP8LaHk+Q8RGX6M7");
            Assert.IsInstanceOfType(cancel, typeof(string));
        }

        [TestMethod]
        public void TestGet()
        {
            var result = r.Get(altToken: "J5GyKV4STxJJcVaJvdHXIOCojjEptVurmOtP8LaHk+Q8RGX6M7");
            Assert.IsInstanceOfType(result, typeof(List<Request>));
        }
    }
}
