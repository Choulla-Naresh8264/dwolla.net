using System;
using System.Web;
using System.Text;
using System.Net.Http;
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
        protected T DwollaParse<T>(string response, bool noEnvelope=false)
        {
            if (noEnvelope) return Jss.Deserialize<T>(response);
            var r = Jss.Deserialize<DwollaResponse<T>>(response);
            if (r.Success) return r.Response;
            throw new ApiException(r.Message);
        }

        /// <summary>
        ///     Synchronous POST request wrapper around HttpClient
        /// </summary>
        /// <param name="endpoint">Dwolla API endpoint</param>
        /// <param name="parameters">A Dictionary with the parameters</param>
        /// <param name="altPostfix">Alternate REST postfix</param>
        /// <returns>JSON-encoded string with API response</returns>
        protected string Post(string endpoint, Dictionary<string, string> parameters, string altPostfix = null)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage request = client.PostAsync(
                        (C.sandbox ? C.sandbox_host : C.production_host)
                        + (altPostfix ?? C.default_postfix) + endpoint, new StringContent(Jss.Serialize(parameters), Encoding.UTF8, "application/json")).Result;
                    return request.Content.ReadAsStringAsync().Result;
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
        ///     A variation of the POST method wherein the only difference
        ///     is that we post a Dictionary<string,object>. Used for MassPay 
        ///     and off-site gateway checkouts. 
        /// </summary>
        /// <param name="endpoint">Dwolla API endpoint</param>
        /// <param name="parameters">A Dictionary with the parameters</param>
        /// <param name="altPostfix">Alternate REST postfix</param>
        /// <returns>JSON-encoded string with API response</returns>
        protected string PostSpecial(string endpoint, Dictionary<string, object> parameters, string altPostfix = null)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage request = client.PostAsync(
                        (C.sandbox ? C.sandbox_host : C.production_host)
                        + (altPostfix ?? C.default_postfix) + endpoint, new StringContent(Jss.Serialize(parameters), Encoding.UTF8, "application/json")).Result;
                    return request.Content.ReadAsStringAsync().Result;
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
        ///     Synchronous GET request wrapper around HttpClient
        /// </summary>
        /// <param name="endpoint">Dwolla API endpoint</param>
        /// <param name="parameters">A Dictionary with the parameters</param>
        /// <param name="altPostfix">Alternate REST postfix</param>
        /// <returns>JSON-encoded string with API response</returns>
        protected string Get(string endpoint, Dictionary<string, string> parameters, string altPostfix = null)
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
                    return client.GetStringAsync(builder.Uri).Result;
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