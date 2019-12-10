using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using ExmoApiX.Communications;

namespace ExmoApiX.AuthenticatedAPIRequests
{
    /// <summary>
    /// Request of the list of user’s cancelled orders.
    /// </summary>
    class User_Cancelled_Orders : Request
    {
        /// <summary>
        /// list of user’s cancelled orders.
        /// </summary>
        public List<Order> userCancelledOrders { get; } = new List<Order>();


        /// <summary>
        /// Constructor of the list of user’s cancelled orders.
        /// </summary>
        /// <param name="user"> Exmo exchange user</param>
        /// <param name="limit"> the number of returned deals (default: 100, мmaximum: 10 000)</param>
        /// <param name="offset"> last deal offset (default: 0)</param>
        public User_Cancelled_Orders(User user, int limit = 100, int offset = 0)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>
            { { "limit", limit.ToString()} , { "offset", offset.ToString()} };
            JToken jArray = ExmoAPI.GetJSONObject("user_cancelled_orders",
                parameters, user);
            if (jArray is JArray)
                userCancelledOrders = jArray.ToObject<List<Order>>();
            else
            {
                JObject jObject = (JObject)jArray;
                if (jObject.ContainsKey("result"))
                    RequestSucceed = Convert.ToBoolean(jObject["result"]);
                else
                    RequestSucceed = false;
                if (jObject.ContainsKey("error"))
                    RequestError = jObject["error"].ToString();
                foreach (var item in jObject)
                    userCancelledOrders.Add(new Order(item.ToString()));
            }
        }
    }
}
