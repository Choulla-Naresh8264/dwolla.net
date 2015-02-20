using System;
using System.IO;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.Script.Serialization;

using Dwolla.SerializableTypes;

namespace Dwolla
{
    /// <summary>
    ///     Handles POST and GET requests, as well as serialization
    /// </summary>
    public class Rest
    {
        /// <summary>
        ///     An instance of the configuration class, which wraps around
        ///     ConfigurationManager
        /// </summary>
        public Config C = new Config();

        /// <summary>
        ///     WCF serializer
        /// </summary>
        public JavaScriptSerializer Jss = new JavaScriptSerializer();

        /// <summary>
        ///     Fully parses result out of Dwolla envelope into easily
        ///     usable serializable type. Verifies response 
        ///     and raises error if API exception encountered.
        /// </summary>
        /// <typeparam name="T">Type of serializable data</typeparam>
        /// <param name="response">JSON response string</param>
        /// <param name="noEnvelope">Some endpoints have no Dwolla "envelope"</param>
        /// <returns>
        ///     Can either be a single object or a serializable
        ///     type as a part of a collection
        /// </returns>
        protected T DwollaParse<T>(Task<string> response, bool noEnvelope=false)
        {
            if (noEnvelope) return Jss.Deserialize<T>(response.Result);
            var r = Jss.Deserialize<DwollaResponse<T>>(response.Result);
            if (r.Success) return r.Response;
            throw new ApiException(r.Message);
        }

        /// <summary>
        ///     Asynchronous POST request wrapper around HttpClient
        /// </summary>
        /// <param name="endpoint">Dwolla API endpoint</param>
        /// <param name="parameters">A Dictionary with the parameters</param>
        /// <param name="altPostfix">Alternate REST postfix</param>
        /// <returns>C# task response, raw JSON string.</returns>
        protected async Task<string> Post(string endpoint, Dictionary<string, string> parameters, string altPostfix = null)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage request = await client.PostAsync(
                        (C.sandbox ? C.sandbox_host : C.production_host)
                        + (altPostfix ?? C.default_postfix) + endpoint, new StringContent(Jss.Serialize(parameters), Encoding.UTF8, "application/json"));
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

        protected async Task<string> PostSpecial(string endpoint, Dictionary<string, object> parameters, string altPostfix = null)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage request = await client.PostAsync(
                        (C.sandbox ? C.sandbox_host : C.production_host)
                        + (altPostfix ?? C.default_postfix) + endpoint, new StringContent(Jss.Serialize(parameters), Encoding.UTF8, "application/json"));
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
        /// <param name="altPostfix">Alternate REST postfix</param>
        /// <returns>C# task response, raw JSON string.</returns>
        protected async Task<string> Get(string endpoint, Dictionary<string, string> parameters, string altPostfix = null)
        {
            using (var client = new HttpClient())
            {
                var builder = new UriBuilder(
                    (C.sandbox ? C.sandbox_host : C.production_host)
                    + (altPostfix ?? C.default_postfix) + endpoint);

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