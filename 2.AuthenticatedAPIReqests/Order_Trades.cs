using System;
using System.Collections.Generic;
using ExmoApiX.Communications;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ExmoApiX.AuthenticatedAPIRequests
{
    /// <summary>
    /// Request of the history of deals with the order.
    /// https://exmo.me/ru/api#/authenticated_api
    /// </summary>
    class Order_Trades : Request
    {
        /// <summary>
        /// Order trade information.
        /// </summary>
        public Trade orderTrade = new Trade();


        /// <summary>
        /// Constructor of order history.
        /// </summary>
        /// <param name="user"> Exmo exchange user</param>
        /// <param name="order_id"> order identifier</param>
        public Order_Trades(User user, string order_id)
        {
            JObject jObject = (JObject)ExmoAPI.GetJSONObject("order_trades",
                new Dictionary<string, string> { { "order_id", order_id } }, user);
            if (!CheckResult(ref jObject))
            {
                orderTrade.type = RequestError;
                return;
            }
            try
            {
                orderTrade = JsonConvert.DeserializeObject<Trade>(jObject.ToString());
            }
            catch (Exception ex)
            {
                orderTrade.type = ex.Message;
            }

        }
    }
    /// <summary>
    /// Trade description class.
    /// </summary>
    class Trade
    {
        /// <summary>
        /// type of order.
        /// </summary>
        public string type { get; set; } = "";
        /// <summary>
        /// incoming currency.
        /// </summary>
        public string in_currency { get; set; } = "";
        /// <summary>
        /// amount of incoming currency. -1 in case of failure;
        /// </summary>
        public double in_amount { get; set; } = -1.0;
        /// <summary>
        /// outcoming currency.
        /// </summary>
        public string out_currency { get; set; } = "";
        /// <summary>
        /// amount of outcoming currency. -1 in case of failure;
        /// </summary>
        public double out_amount { get; set; } = -1.0;
        /// <summary>
        /// Deals array with standard Order type.
        /// </summary>
        public List<Order> trades = new List<Order>();
    }
}
