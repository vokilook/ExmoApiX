using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ExmoApiX.Communications;

namespace ExmoApiX.AuthenticatedAPIRequests
{
    /// <summary>
    /// Request of the list of user’s active orders.
    /// </summary>
    class User_Open_Orders : Request
    {
        /// <summary>
        /// list of user’s active orders (for each pair).
        /// </summary>
        public Dictionary<string, List<Order>> userOpenOrders { get; } =
            new Dictionary<string, List<Order>>();


        /// <summary>
        /// Constructor of the list of user’s active orders.
        /// </summary>
        /// <param name="user"> Exmo exchange user</param>
        public User_Open_Orders(User user)
        {
            JObject jObject = (JObject)ExmoAPI.GetJSONObject("user_open_orders", 
                new Dictionary<string, string>(), user);
            if (!CheckResult(ref jObject))
            {
                userOpenOrders.Add(jObject.First.ToString(),
                    new List<Order> { new Order(RequestError) });
                return;
            }
            foreach (var pair in jObject)
                try
                {
                    List<Order> ordersList = 
                        JsonConvert.DeserializeObject<List<Order>>(pair.Value.ToString());
                    userOpenOrders.Add(pair.Key, ordersList);
                }
                catch (Exception ex)
                {
                    userOpenOrders.Add(ex.Message, new List<Order>());
                }

        }
    }
}
