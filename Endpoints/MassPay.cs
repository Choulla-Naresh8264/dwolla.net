using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dwolla.SerializableTypes;

namespace Dwolla
{
    class MassPay : Rest
    {
        public MassPayJob Create(string fundingSource, List<MassPayItem> items, Dictionary<string, string> aParams = null, string altToken = null,
            int? altPin = null)
        {
            var data = new Dictionary<string, string>
            {
                {"oauth_token", altToken ?? C.access_token},
                {"pin", altPin.ToString() ?? C.pin.ToString()},
                {"fundsSource", fundingSource},
            };

            var itemDict = new Dictionary<string, List<MassPayItem>>
            {
                {"items", items}
            };

            data = itemDict.Union(data).ToDictionary(k => k.Key, v => v.Value);

            if (aParams != null) data = aParams.Union(data).ToDictionary(k => k.Key, v => v.Value);
            return DwollaParse<MassPayJob>(Post("/transactions/refund", data));
        }

        /// <summary>
        ///     Check the status of an existing MassPay job and
        ///     returns additional information.
        /// </summary>
        /// <param name="id">Target job ID</param>
        /// <param name="altToken">Alternate OAuth token</param>
        /// <returns>MassPayJob item</returns>
        public MassPayJob GetJob(string id, string altToken = null)
        {
            return DwollaParse<MassPayJob>(Get("/masspay/" + id,
            new Dictionary<string, string>
                {
                    {"oauth_token", altToken ?? C.access_token}
                }));
        }

        /// <summary>
        /// Gets all items for a created MassPay job
        /// </summary>
        /// <param name="id">Target MassPay job ID</param>
        /// <param name="aParams">Dictionary with additional parameters</param>
        /// <param name="altToken">Alternate OAuth token</param>
        /// <returns>A List of MassPayItem objects</returns>
        public List<MassPayItem> GetJobItems(string id, Dictionary<string, string> aParams = null,
            string altToken = null)
        {
            var data = new Dictionary<string, string>
            {
                {"oauth_token", altToken ?? C.access_token},
            };

            if (aParams != null) data = aParams.Union(data).ToDictionary(k => k.Key, v => v.Value);
            return DwollaParse<List<MassPayItem>>(Post("/masspay/" + id + "/items", data));
        }

        /// <summary>
        /// Gets an item from a created MassPay job
        /// </summary>
        /// <param name="jobId">Target MassPay job ID</param>
        /// <param name="itemId">Target MassPay item ID</param>
        /// <param name="altToken">Alternate OAuth token</param>
        /// <returns>MassPayItem object</returns>
        public MassPayItem GetItem(string jobId, string itemId, string altToken = null)
        {
            return DwollaParse<MassPayItem>(Get("/masspay/" + jobId + "/items/" + itemId,
            new Dictionary<string, string>
                {
                    {"oauth_token", altToken ?? C.access_token}
                }));
        }

        /// <summary>
        /// Lists all MassPay jobs for the user
        /// associated with the current OAuth token
        /// </summary>
        /// <param name="altToken">Alternate OAuth token</param>
        /// <returns>List of MassPayJob objects</returns>
        public List<MassPayJob> ListJobs(string altToken = null)
        {
            return DwollaParse<List<MassPayJob>>(Get("/masspay",
            new Dictionary<string, string>
                {
                    {"oauth_token", altToken ?? C.access_token}
                }));
        }
    }
}
