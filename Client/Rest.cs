using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web;
using System.IO;
using System.Web.Script.Serialization;

namespace dwolla
{
    public class Rest
    {
        /// <summary>
        /// WCF serializer
        /// </summary>
        public JavaScriptSerializer jss = new JavaScriptSerializer();

        /// <summary>
        /// Asynchronous POST request wrapper around HttpClient
        /// </summary>
        /// <param name="endpoint">Dwolla API endpoint</param>
        /// <param name="parameters">A Dictionary with the parameters</param>
        /// <returns></returns>

        public async Task<string> post(string endpoint, Dictionary<string, string> parameters)
        {
            using (HttpClient client = new HttpClient())
            {
                var data = new FormUrlEncodedContent(parameters);
                try
                {
                    var request = await client.PostAsync(
                    (Properties.Settings.Default.sandbox ?
                    Properties.Settings.Default.sandbox_host : Properties.Settings.Default.production_host)
                    + Properties.Settings.Default.default_postfix + endpoint, data);
                    return await request.Content.ReadAsStringAsync();
                }
                catch (Exception wtf)
                {
                    Console.WriteLine("dwolla.net: An exception has occurred while making a POST request.");
                    Console.WriteLine(wtf.ToString());
                    return null;
                }
            }
        }

        /// <summary>
        /// Asynchronous GET request wrapper around HttpClient
        /// </summary>
        /// <param name="endpoint">Dwolla API endpoint</param>
        /// <param name="parameters">A Dictionary with the parameters</param>
        /// <returns></returns>
        public async Task<string> get(string endpoint, Dictionary<string, string> parameters)
        {
            using (HttpClient client = new HttpClient())
            {
                var builder = new UriBuilder(
                    (Properties.Settings.Default.sandbox ?
                    Properties.Settings.Default.sandbox_host : Properties.Settings.Default.production_host) 
                    + Properties.Settings.Default.default_postfix + endpoint);

                var query = HttpUtility.ParseQueryString(builder.Query);

                foreach (var k in parameters.Keys)
                    query[k] = parameters[k];

                builder.Query = query.ToString();
                
                try
                {
                    var reply = await client.GetStreamAsync(builder.Uri);
                    return await client.GetStringAsync(builder.Uri);
                }
                catch (Exception wtf)
                {
                    Console.WriteLine("dwolla.net: An exception has occurred while making a POST request.");
                    Console.WriteLine(wtf.ToString());
                    return null;
                }
            }
        }
    }
}
