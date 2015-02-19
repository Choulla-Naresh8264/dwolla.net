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

        /// <summary>
        /// Instead of creating an ordered test, this is more readable
        /// and makes sense to do chronologically (e.g send then check)
        /// </summary>
        [TestMethod]
        public void TestSendandInfo()
        {
            var send = t.Send("812-174-9528", 0.01, altToken: "J5GyKV4STxJJcVaJvdHXIOCojjEptVurmOtP8LaHk+Q8RGX6M7", altPin: 1337);
            Assert.IsInstanceOfType(send, typeof(int));

            var info = t.Info(send.ToString(), "J5GyKV4STxJJcVaJvdHXIOCojjEptVurmOtP8LaHk+Q8RGX6M7");
            Assert.IsInstanceOfType(info, typeof(Transaction));
        }

        [TestMethod]
        public void TestGet()
        {
            var result = t.Get(altToken: "J5GyKV4STxJJcVaJvdHXIOCojjEptVurmOtP8LaHk+Q8RGX6M7");
            Assert.IsInstanceOfType(result, typeof(List<Transaction>));
        }

        [TestMethod]
        public void TestStats()
        {
            var result = t.Stats(altToken: "J5GyKV4STxJJcVaJvdHXIOCojjEptVurmOtP8LaHk+Q8RGX6M7");
            Assert.IsInstanceOfType(result, typeof(TransactionStats));
        }
    }
}
