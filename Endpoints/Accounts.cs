using System.Collections.Generic;
using Dwolla.Properties;
using Dwolla.SerializableTypes;

namespace Dwolla
{
    public class Accounts : Rest
    {
        /// <summary>
        ///     Returns basic account info for the passed account ID.
        /// </summary>
        /// <param name="accountId">Account ID</param>
        /// <returns>UserBasic object</returns>
        public UserBasic Basic(string accountId)
        {
            return DwollaParse<UserBasic>(get("/users/" + accountId,
                new Dictionary<string, string>
                {
                    {"client_id", Settings.Default.client_id},
                    {"client_secret", Settings.Default.client_secret},
                }));
        }

        /// <summary>
        ///     Returns full account information for the user associated
        ///     with the currently set OAuth token.
        /// </summary>
        /// <param name="altToken">Alternate OAuth token</param>
        /// <returns>UserFull object</returns>
        public UserFull Full(string altToken = null)
        {
            return DwollaParse<UserFull>(get("/users",
                new Dictionary<string, string>
                {
                    {"oauth_token", altToken ?? Settings.Default.access_token}
                }));
        }

        /// <summary>
        ///     Returns balance for the account associated with the
        ///     currently set OAuth token.
        /// </summary>
        /// <param name="altToken">Alternate OAuth token</param>
        /// <returns>Balance as double</returns>
        public double Balance(string altToken = null)
        {
            return DwollaParse<double>(get("/balance",
                new Dictionary<string, string>
                {
                    {"oauth_token", altToken ?? Settings.Default.access_token}
                }));
        }

        /// <summary>
        ///     Returns users and venues near a location.
        /// </summary>
        /// <param name="lat">Latitudinal coordinates</param>
        /// <param name="lon">Longitudinal coordinates</param>
        /// <returns>UserNearby object</returns>
        public UserNearby Nearby(double lat, double lon)
        {
            return DwollaParse<UserNearby>(get("/users/nearby", new Dictionary<string, string>
            {
                {"client_id", Settings.Default.client_id},
                {"client_secret", Settings.Default.client_secret},
                {"latitude", lat.ToString()},
                {"longitude", lon.ToString()}
            }));
        }

        /// <summary>
        ///     Gets auto withdrawal status for the account associated
        ///     with the currently set OAuth token.
        /// </summary>
        /// <param name="altToken">Alternate OAuth token</param>
        /// <returns>AutoWithdrawalStatus object</returns>
        public AutoWithdrawalStatus GetAutoWithdrawalStatus(string altToken = null)
        {
            return DwollaParse<AutoWithdrawalStatus>(get("/accounts/features/auto_withdrawl",
                new Dictionary<string, string>
                {
                    {"oauth_token", altToken ?? Settings.Default.access_token}
                }));
        }

        /// <summary>
        ///     Sets auto-withdrawal status of the account associated
        ///     with the current OAuth token under the specified
        ///     funding ID.
        /// </summary>
        /// <param name="status">Enable toggle</param>
        /// <param name="fundingId">Target funding ID</param>
        /// <param name="altToken">Alternate OAuth token</param>
        /// <returns></returns>
        public bool ToggleAutoWithdrawalStatus(bool status, string fundingId, string altToken = null)
        {
            var r = DwollaParse<string>(post("/accounts/features/auto_withdrawl",
                new Dictionary<string, string>
                {
                    {"oauth_token", altToken ?? Settings.Default.access_token}
                }));

            // I figure this will be more useful than the string
            return r == "Enabled" ? true : false;
        }
    }
}