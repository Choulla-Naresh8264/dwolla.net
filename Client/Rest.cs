using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web;
using System.Web.Script.Serialization;

using Dwolla.SerializableTypes;

namespace Dwolla
{
    public class Rest
    {
        /// <summary>
        /// WCF serializer
        /// </summary>
        public JavaScriptSerializer Jss = new JavaScriptSerializer();

        /// <summary>
        /// Fully parses result out of Dwolla envelope into easily 
        /// usable serializable type. Verifies response and raises 
        /// error if API exception encountered. 
        /// </summary>
        /// <typeparam name="T">Type of serializable data</typeparam>
        /// <param name="response">JSON response string</param>
        /// <returns>Can either be a single object or a serializable
        /// type as a part of a collection</returns>
        public T DwollaParse<T>(Task<string> response)
        {
            Console.WriteLine(response.Result);
            var r = Jss.Deserialize<DwollaResponse<T>>(response.Result);
            if (r.Success) return r.Response;
            else throw new Dwolla.ApiException(r.Message);
        }

        /// <summary>
        /// Asynchronous POST request wrapper around HttpClient
        /// </summary>
        /// <param name="endpoint">Dwolla API endpoint</param>
        /// <param name="parameters">A Dictionary with the parameters</param>
        /// <returns>C# task response, raw JSON string.</returns>
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
        /// <returns>C# task response, raw JSON string.</returns>
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
