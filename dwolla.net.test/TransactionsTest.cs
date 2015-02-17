using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Dwolla;
using Dwolla.SerializableTypes;

namespace dwolla.net.test
{
    [TestClass]
    public class TransactionsTest
    {
        public Transactions t = new Transactions();
        public int tnum;

        [TestMethod]
        public void TestSend()
        {
            var result = t.Send("812-174-9528", 0.01, altToken: "J5GyKV4STxJJcVaJvdHXIOCojjEptVurmOtP8LaHk+Q8RGX6M7", altPin: 1337);
            Assert.IsInstanceOfType(result, typeof(int));
            tnum = result;
        }

        [TestMethod]
        public void TestGet()
        {
            var result = t.Get(altToken: "J5GyKV4STxJJcVaJvdHXIOCojjEptVurmOtP8LaHk+Q8RGX6M7");
            Assert.IsInstanceOfType(result, typeof(List<Transaction>));
        }

        [TestMethod]
        public void TestInfo()
        {
            var result = t.Info(tnum.ToString(), "J5GyKV4STxJJcVaJvdHXIOCojjEptVurmOtP8LaHk+Q8RGX6M7");
            Assert.IsInstanceOfType(result, typeof(Transaction));
        }
    }
}
