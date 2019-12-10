using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using ExmoApiX.Communications;

namespace ExmoApiX.AuthenticatedAPIRequests
{
    /// <summary>
    /// Request of order cancellation.
    /// https://exmo.me/ru/api#/authenticated_api
    /// </summary>
    class Order_Cancel : Request
    {
        /// <summary>
        /// Constructor of order cancellation request.
        /// </summary>
        /// <param name="user"> Exmo exchange user</param>
        /// <param name="order_id"> order identifier</param>
        public Order_Cancel(User user, string order_id)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>
                { {"order_id", order_id} };
            JObject jObject = (JObject)ExmoAPI.GetJSONObject("order_cancel", parameters, user);
            if (jObject.ContainsKey("result"))
                RequestSucceed = Convert.ToBoolean(jObject["result"]);
            else
            {
                RequestSucceed = false;
                RequestError = "Unexpected server answer.";
                return;
            }
            if (jObject.ContainsKey("error"))
                RequestError = jObject["error"].ToString();

        }

    }
}
