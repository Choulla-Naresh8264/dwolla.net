using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using dwolla.SerializableTypes;

namespace dwolla
{
    public class Contacts : Rest
    {
        /// <summary>
        /// Gets contacts from user associated with OAuth token
        /// </summary>
        /// <param name="aparams">Dictionary with additional parameters</param>
        /// <param name="alt_token">Alternate OAuth token</param>
        /// <returns></returns>
        public List<Contact> get(Dictionary<string, string> aparams = null, string alt_token = null)
        {
            Dictionary<string, string> data = new Dictionary<string, string>()
            {
                { "oauth_token", alt_token == null ? Properties.Settings.Default.access_token : alt_token }
            };

            if (aparams != null) data = aparams.Union(data).ToDictionary(k => k.Key, v => v.Value);
            return DwollaParse<List<Contact>>(get("/contacts", data));
        }
    }
}
