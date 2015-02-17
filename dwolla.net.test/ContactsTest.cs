using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Dwolla;
using Dwolla.SerializableTypes;

namespace dwolla.net.test
{
    [TestClass]
    public class ContactsTest
    {
        public Contacts c = new Contacts();
        public JavaScriptSerializer j = new JavaScriptSerializer();

        [TestMethod]
        public void TestGet()
        {
            var result = c.Get(altToken: "J5GyKV4STxJJcVaJvdHXIOCojjEptVurmOtP8LaHk+Q8RGX6M7");
            Assert.IsInstanceOfType(result, typeof(List<Contact>));
        }

        [TestMethod]
        public void TestNearby()
        {
            var result = c.Nearby(0, 0);
            Assert.IsInstanceOfType(result, typeof(List<UserNearby>));
        }
    }
}
