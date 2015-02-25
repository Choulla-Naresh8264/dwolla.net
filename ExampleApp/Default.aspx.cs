using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading.Tasks;

using Dwolla;
using Dwolla.SerializableTypes;

namespace ExampleApp
{
    public partial class _Default : Page
    {
        protected void tHistButton_Click(object sender, EventArgs e)
        {
            var t = new Transactions();
            foreach (Transaction z in t.Get(altToken: "J5GyKV4STxJJcVaJvdHXIOCojjEptVurmOtP8LaHk+Q8RGX6M7"))
            {
                Console.WriteLine("Transaction found");
                TransHistoryLB.Items.Add(new ListItem(String.Format("ID: {0}, Amount: {1}", z.Id, z.Amount.ToString())));
            }
        }

        protected void SendMoneyButton_Click(object sender, EventArgs e)
        {
            var t = new Transactions();
            try
            {
                t.Send(DestTB.Text, Convert.ToDouble(AmountTB.Text), new Dictionary<string, string> { { "destinationType", destDD.SelectedIndex == 0 ? "Dwolla" : "Email" } }, "J5GyKV4STxJJcVaJvdHXIOCojjEptVurmOtP8LaHk+Q8RGX6M7");
            }
            catch (ApiException a)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + a.Message + "')", true);
            }
        }
    }
}