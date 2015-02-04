using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using Dwolla.SerializableTypes;

namespace Dwolla
{
    /// <summary>
    ///     Handles POST and GET requests, as well as serialization
    ///     Handles POST and GET requests, as well as serialization
    /// </summary>
    public class Rest
    {
        /// <summary>
        ///     An instance of the configuration class, which wraps around
        ///     ConfigurationManager
        /// </summary>
        public Config C;

        /// <summary>
        ///     WCF serializer
        /// </summary>
        public JavaScriptSerializer Jss = new JavaScriptSerializer();

        /// <summary>
        ///     Fully parses result out of Dwolla envelope into easily
        ///     usable serializable type. Verifies response and raises
        ///     error if API exception encountered.
        /// </summary>
        /// <typeparam name="T">Type of serializable data</typeparam>
        /// <param name="response">JSON response string</param>
        /// <returns>
        ///     Can either be a single object or a serializable
        ///     type as a part of a collection
        /// </returns>
        protected T DwollaParse<T>(Task<string> response)
        {
            Console.WriteLine(response.Result);
            var r = Jss.Deserialize<DwollaResponse<T>>(response.Result);
            if (r.Success) return r.Response;
            throw new ApiException(r.Message);
        }

        /// <summary>
        ///     Asynchronous POST request wrapper around HttpClient
        /// </summary>
        /// <param name="endpoint">Dwolla API endpoint</param>
        /// <param name="parameters">A Dictionary with the parameters</param>
        /// <returns>C# task response, raw JSON string.</returns>
        protected async Task<string> Post(string endpoint, Dictionary<string, string> parameters)
        {
            using (var client = new HttpClient())
            {
                var data = new FormUrlEncodedContent(parameters);
                try
                {
                    HttpResponseMessage request = await client.PostAsync(
                        (C.sandbox ? C.sandbox_host : C.production_host)
                        + C.default_postfix + endpoint, data);
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
        ///     Asynchronous GET request wrapper around HttpClient
        /// </summary>
        /// <param name="endpoint">Dwolla API endpoint</param>
        /// <param name="parameters">A Dictionary with the parameters</param>
        /// <returns>C# task response, raw JSON string.</returns>
        protected async Task<string> Get(string endpoint, Dictionary<string, string> parameters)
        {
            using (var client = new HttpClient())
            {
                var builder = new UriBuilder(
                    (C.sandbox ? C.sandbox_host : C.production_host)
                    + C.default_postfix + endpoint);

                NameValueCollection query = HttpUtility.ParseQueryString(builder.Query);

                foreach (string k in parameters.Keys)
                    query[k] = parameters[k];

                builder.Query = query.ToString();

                try
                {
                    Stream reply = await client.GetStreamAsync(builder.Uri);
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