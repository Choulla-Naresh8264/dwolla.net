using System.Linq;
using System.Collections.Generic;

using Dwolla.SerializableTypes;

namespace Dwolla
{
    class Checkouts : Rest
    {
        /// <summary>
        /// Creates an off-site gateway checkout session
        /// </summary>
        /// <param name="po">PurchaseOrder object</param>
        /// <param name="aParams">Additional parameters</param>
        /// <returns>Checkout URL</returns>
        public string Create(PurchaseOrder po, Dictionary<string, string> aParams)
        {
            var data = new Dictionary<string, string>
            {
                {"client_id", C.client_id},
                {"client_secret", C.client_secret},
                {"purchaseOrder", Jss.Serialize(po)}
            };

            if (aParams != null) data = aParams.Union(data).ToDictionary(k => k.Key, v => v.Value);
            return C.sandbox ? C.sandbox_host : C.production_host 
                + "payment/checkout" + DwollaParse<string>(Post("/offsitegateway/checkouts", data));
        }

        /// <summary>
        ///     Retrieves information (status, etc.) from an existing checkout
        /// </summary>
        /// <param name="checkoutId">Checkout ID</param>
        /// <returns>Checkout object</returns>
        public Checkout Get(string checkoutId)
        {
            return DwollaParse<Checkout>(Get("/offsitegateway/checkouts/" + checkoutId,
            new Dictionary<string, string>
            {
                {"client_id", C.client_id},
                {"client_secret", C.client_secret}
            }));
        }

        /// <summary>
        /// Completes an offsite-gateway "Pay Later" checkout session.
        /// </summary>
        /// <param name="checkoutId"></param>
        /// <returns></returns>
        public CheckoutComplete Complete(string checkoutId)
        {
            return DwollaParse<CheckoutComplete>(Get("/offsitegateway/checkouts/" + checkoutId + "/complete",
            new Dictionary<string, string>
            {
                {"client_id", C.client_id},
                {"client_secret", C.client_secret}
            }));
        }
    }
}
