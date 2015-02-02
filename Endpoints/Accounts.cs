using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using dwolla.SerializableTypes;

namespace dwolla
{
    public class Accounts : Rest
    {
        /// <summary>
        /// Returns basic account info for the passed account ID.
        /// </summary>
        /// <param name="account_id">Account ID</param>
        /// <returns>UserBasic object</returns>
        public UserBasic basic(string account_id)
        {
            return DwollaParse<UserBasic>(get("/users/" + account_id, 
            new Dictionary<string, string>()
            {
                { "client_id", Properties.Settings.Default.client_id },
                { "client_secret", Properties.Settings.Default.client_secret },
            }));
        }

        /// <summary>
        /// Returns full account information for the user associated
        /// with the currently set OAuth token.
        /// </summary>
        /// <param name="alt_token">Alternate OAuth token</param>
        /// <returns>UserFull object</returns>
        public UserFull full(string alt_token = null)
        {
            return DwollaParse<UserFull>(get("/users",
            new Dictionary<string, string>()
            {
                { "oauth_token", alt_token == null ? Properties.Settings.Default.access_token : alt_token }
            }));
        }

        /// <summary>
        /// Returns balance for the account associated with the 
        /// currently set OAuth token.
        /// </summary>
        /// <param name="alt_token">Alternate OAuth token</param>
        /// <returns>Balance as double</returns>
        public double balance(string alt_token = null)
        {
            return DwollaParse<double>(get("/balance",
            new Dictionary<string, string>()
            {
                { "oauth_token", alt_token == null ? Properties.Settings.Default.access_token : alt_token }
            }));
        }

        /// <summary>
        /// Returns users and venues near a location.
        /// </summary>
        /// <param name="lat">Latitudinal coordinates</param>
        /// <param name="lon">Longitudinal coordinates</param>
        /// <returns>UserNearby object</returns>
        public UserNearby nearby(double lat, double lon)
        {
            return DwollaParse<UserNearby>(get("/users/nearby", new Dictionary<string, string>()
            {
                { "client_id", Properties.Settings.Default.client_id },
                { "client_secret", Properties.Settings.Default.client_secret },
                { "latitude", lat.ToString() },
                { "longitude", lon.ToString() }
            }));
        }

        /// <summary>
        /// Gets auto withdrawal status for the account associated
        /// with the currently set OAuth token.
        /// </summary>
        /// <param name="alt_token">Alternate OAuth token</param>
        /// <returns>AutoWithdrawalStatus object</returns>
        public AutoWithdrawalStatus getAutoWithdrawalStatus(string alt_token = null)
        {
            return DwollaParse<AutoWithdrawalStatus>(get("/accounts/features/auto_withdrawl",
            new Dictionary<string, string>()
            {
                { "oauth_token", alt_token == null ? Properties.Settings.Default.access_token : alt_token }
            }));
        }

        /// <summary>
        /// Sets auto-withdrawal status of the account associated
        /// with the current OAuth token under the specified
        /// funding ID.
        /// </summary>
        /// <param name="status">Enable toggle</param>
        /// <param name="funding_id">Target funding ID</param>
        /// <param name="alt_token">Alternate OAuth token</param>
        /// <returns></returns>
        public bool toggleAutoWithdrawalStatus(bool status, string funding_id, string alt_token = null)
        {
            var r =  DwollaParse<string>(post("/accounts/features/auto_withdrawl",
            new Dictionary<string, string>()
            {
                { "oauth_token", alt_token == null ? Properties.Settings.Default.access_token : alt_token }
            }));

            // I figure this will be more useful than the string
            return r == "Enabled" ? true : false;
        }
    }
}
