using System;
using System.Linq;
using System.Collections.Generic;
using Dwolla.SerializableTypes;

namespace Dwolla
{
    public class Transactions : Rest
    {
        /// <summary>
        /// Sends money to specified destination user
        /// </summary>
        /// <param name="destinationId">Destination Dwolla ID</param>
        /// <param name="amount">Amount to send</param>
        /// <param name="aParams">Additional parameters</param>
        /// <param name="altToken">Alternate OAuth token</param>
        /// <param name="altPin">Alternate pin</param>
        /// <returns>Resulting transaction ID</returns>
        public int? Send(string destinationId, double amount, Dictionary<string, string> aParams = null,
            string altToken = null, int? altPin = null)
        {
            var data = new Dictionary<string, string>
            {
                {"oauth_token", altToken ?? C.access_token},
                {"pin", altPin.ToString() ?? C.pin.ToString()},
                {"destinationId", destinationId},
                {"amount", amount.ToString()}
            };

            if (aParams != null) data = aParams.Union(data).ToDictionary(k => k.Key, v => v.Value);
            return DwollaParse<int?>(Post("/transactions/send", data));
        }

        /// <summary>
        ///     Lists transactions for the user associated with
        ///     the currently set OAuth token.
        /// </summary>
        /// <param name="aParams">Additional parameters</param>
        /// <param name="altToken">Alternate OAuth token</param>
        /// <returns>A List of Transaction objects</returns>
        public List<Transaction> Get(Dictionary<string, string> aParams = null, string altToken = null)
        {
            var data = new Dictionary<string, string>
            {
                {"oauth_token", altToken ?? C.access_token},
                {"client_id", C.client_id},
                {"client_secret", C.client_secret}
            };

            if (aParams != null) data = aParams.Union(data).ToDictionary(k => k.Key, v => v.Value);
            return DwollaParse<List<Transaction>>(Get("/transactions", data));
        }

        /// <summary>
        ///     Returns transaction information for the transaction
        ///     associated with the passed transaction ID
        /// </summary>
        /// <param name="transactionId">Target transaction ID</param>
        /// <param name="altToken">Alternate OAuth token</param>
        /// <returns>Transaction object</returns>
        public Transaction Info(string transactionId, string altToken = null)
        {
            return DwollaParse<Transaction>(Get("/transactions/" + transactionId,
            new Dictionary<string, string>
            {
                {"oauth_token", altToken ?? C.access_token},
                {"client_id", C.client_id},
                {"client_secret", C.client_secret}
            }));
        }

        /// <summary>
        ///     Refunds (either completely or partially) funds to
        ///     the sending user for a transaction.
        /// </summary>
        /// <param name="transactionId">Transaction ID</param>
        /// <param name="fundingSource">Funding source to fund transaction</param>
        /// <param name="amount">Amount to refund</param>
        /// <param name="aParams">Additional parameters</param>
        /// <param name="altToken">Alternate OAuth token</param>
        /// <param name="altPin">Alternate parameters</param>
        /// <returns>Refund object</returns>
        public Refund Refund(string transactionId, string fundingSource, double amount,
            Dictionary<string, string> aParams, string altToken = null, int? altPin = null)
        {
            var data = new Dictionary<string, string>
            {
                {"oauth_token", altToken ?? C.access_token},
                {"pin", altPin.ToString() ?? C.pin.ToString()},
                {"fundsSource", fundingSource},
                {"transactionId", transactionId},
                {"amount", amount.ToString()}
            };

            if (aParams != null) data = aParams.Union(data).ToDictionary(k => k.Key, v => v.Value);
            return DwollaParse<Refund>(Post("/transactions/refund", data));
        }

        /// <summary>
        ///     Retrieves transaction statistics for
        ///     the user associated with the current OAuth token.
        /// </summary>
        /// <param name="aParams">Additional parameters</param>
        /// <param name="altToken">Alternate OAuth token</param>
        /// <returns>TransactionStats object</returns>
        public TransactionStats Stats(Dictionary<string, string> aParams = null, string altToken = null)
        {
            var data = new Dictionary<string, string>
            {
                {"oauth_token", altToken ?? C.access_token}
            };

            if (aParams != null) data = aParams.Union(data).ToDictionary(k => k.Key, v => v.Value);
            return DwollaParse<TransactionStats>(Get("/transactions/stats", data));
        }
    }
}
