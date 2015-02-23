using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Dwolla;
using Dwolla.SerializableTypes;

namespace dwolla.net.test
{
    [TestClass]
    public class MassPayTest
    {
        public MassPay m = new MassPay();

        [TestMethod]
        public void TestAll()
        {
            var items = new List<MassPayItem>() 
            {{
                new MassPayItem()
                {
                    amount = 5.50,
                    destination = "812-174-9528",
                    destinationType = "Dwolla",
                    notes = "Mmm, a unit test!"
                }
              }
            };
            var job = m.Create("Balance", items, altToken: "J5GyKV4STxJJcVaJvdHXIOCojjEptVurmOtP8LaHk+Q8RGX6M7", altPin: 1337);
            Assert.IsInstanceOfType(job, typeof(MassPayJob));

            var retjob = m.GetJob(job.Id, "J5GyKV4STxJJcVaJvdHXIOCojjEptVurmOtP8LaHk+Q8RGX6M7");
            Assert.IsInstanceOfType(retjob, typeof(MassPayJob));

            var jobItems = m.GetJobItems(job.Id, altToken: "J5GyKV4STxJJcVaJvdHXIOCojjEptVurmOtP8LaHk+Q8RGX6M7");
            Assert.IsInstanceOfType(jobItems, typeof(List<MassPayRetrievedItem>));

            var item = m.GetItem(job.Id, jobItems[0].ItemId, altToken: "J5GyKV4STxJJcVaJvdHXIOCojjEptVurmOtP8LaHk+Q8RGX6M7");
            Assert.IsInstanceOfType(item, typeof(MassPayRetrievedItem));

            var jobList = m.ListJobs("J5GyKV4STxJJcVaJvdHXIOCojjEptVurmOtP8LaHk+Q8RGX6M7");
            Assert.IsInstanceOfType(jobList, typeof(List<MassPayJob>));
        }
    }
}
