using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;


using Dwolla;
using Dwolla.SerializableTypes;

namespace dwolla.net.test
{
    [TestClass]
    public class AccountsTest
    {
        public Accounts a = new Accounts();

        [TestMethod]
        public void TestBasic()
        {
            var result = a.Basic("812-111-7219");
            Assert.IsInstanceOfType(result, typeof(UserBasic));
        }

        [TestMethod]
        public void TestFull()
        {
            var result = a.Full("J5GyKV4STxJJcVaJvdHXIOCojjEptVurmOtP8LaHk+Q8RGX6M7");
            Assert.IsInstanceOfType(result, typeof(UserFull));
        }    

        [TestMethod]
        public void TestBalance()
        {
            var result = a.Balance("J5GyKV4STxJJcVaJvdHXIOCojjEptVurmOtP8LaHk+Q8RGX6M7");
            Assert.IsInstanceOfType(result, typeof(double));
        }

        [TestMethod]
        public void TestNearby()
        {
            var result = a.Nearby(25, 25);
            Assert.IsInstanceOfType(result, typeof(List<UserNearby>));
        }

        [TestMethod]
        public void TestAWS()
        {
            var result = a.GetAutoWithdrawalStatus("J5GyKV4STxJJcVaJvdHXIOCojjEptVurmOtP8LaHk+Q8RGX6M7");
            Assert.IsInstanceOfType(result, typeof(AutoWithdrawalStatus));
        }
    }
}
