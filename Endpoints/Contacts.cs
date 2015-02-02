using System.Collections.Generic;
using System.Linq;
using Dwolla.Properties;
using Dwolla.SerializableTypes;

namespace Dwolla
{
    public class Contacts : Rest
    {
        /// <summary>
        ///     Gets contacts from user associated with OAuth token
        /// </summary>
        /// <param name="aParams">Dictionary with additional parameters</param>
        /// <param name="altToken">Alternate OAuth token</param>
        /// <returns>List of Contacts</returns>
        public List<Contact> Get(Dictionary<string, string> aParams = null, string altToken = null)
        {
            var data = new Dictionary<string, string>
            {
                {"oauth_token", altToken ?? Settings.Default.access_token}
            };

            if (aParams != null) data = aParams.Union(data).ToDictionary(k => k.Key, v => v.Value);
            return DwollaParse<List<Contact>>(get("/contacts", data));
        }

        /// <summary>
        ///     Returns Dwolla spots near the specified geographical location
        /// </summary>
        /// <param name="lat">Latitudinal coordinates</param>
        /// <param name="lon">Longitudinal coordinates</param>
        /// <param name="aParams">Dictionary with additional parameters</param>
        /// <returns>List of UserNearby</returns>
        public List<UserNearby> Nearby(double lat, double lon, Dictionary<string, string> aParams = null)
        {
            var data = new Dictionary<string, string>
            {
                {"client_id", Settings.Default.client_id},
                {"client_secret", Settings.Default.client_secret},
                {"latitude", lat.ToString()},
                {"longitude", lon.ToString()}
            };

            if (aParams != null) data = aParams.Union(data).ToDictionary(k => k.Key, v => v.Value);
            return DwollaParse<List<UserNearby>>(get("/contacts/nearby", data));
        }
    }
}