using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web;
using System.IO;

namespace dwolla.net
{
    public class Rest
    {

        //public string DwollaParse(string response);

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
                var request = await client.PostAsync(
                    (Properties.Settings.Default.sandbox ? Properties.Settings.Default.sandbox_host : Properties.Settings.Default.production_host) + 
                    Properties.Settings.Default.default_postfix + endpoint, data);
                return await request.Content.ReadAsStringAsync();
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
                var builder = new UriBuilder((Properties.Settings.Default.sandbox ? Properties.Settings.Default.sandbox_host : Properties.Settings.Default.production_host) +
                    Properties.Settings.Default.default_postfix + endpoint);

                var query = HttpUtility.ParseQueryString(builder.Query);

                foreach (var k in parameters.Keys)
                    query[k] = parameters[k];

                builder.Query = query.ToString();

                Console.WriteLine("dwolla.net: GET on " + builder.Uri.ToString());
                return await client.GetStringAsync(builder.Uri);
            }
        }
    }
}
