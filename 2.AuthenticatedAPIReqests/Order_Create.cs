using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using ExmoApiX.Communications;

namespace ExmoApiX.AuthenticatedAPIRequests
{
    /// <summary>
    /// Request of order creation.
    /// https://exmo.me/ru/api#/authenticated_api
    /// </summary>
    class Order_Create : Request
    {
        /// <summary>
        /// order identifier.
        /// </summary>
        public string order_id = "";


        /// <summary>
        /// Constructor of order creation.
        /// </summary>
        /// <param name="user"> Exmo exchange user</param>
        /// <param name="pair"> currency pair</param>
        /// <param name="quantity"> quantity for the order</param>
        /// <param name="price"> price for the order</param>
        /// <param name="type"> type of order, can have the following values
        /// buy - buy order
        /// sell - sell order
        /// market_buy - market buy-order
        /// market_sell - market sell-order
        /// market_buy_total - market buy-order for a certain amount
        /// market_sell_total - market sell-order for a certain amount
        /// </param>
        public Order_Create(User user, string pair, double quantity, double price, string type)
        {
            Dictionary<string, string> req = new Dictionary<string, string>
                { { "pair",  pair}, { "quantity", quantity.ToString().Replace(",", ".") },
                { "price", price.ToString().Replace(",", ".")}, {"type", type.ToString() } };
            JObject jObject = (JObject)ExmoAPI.GetJSONObject("order_create", req, user);
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
            if (jObject.ContainsKey("order_id"))
                order_id = jObject["order_id"].ToString();

        }
    }
    /// <summary>
    /// Common structure with standard order (deal) information.
    /// </summary>
    public struct Order
    {
        /// <summary>
        /// date and time of order creation.
        /// </summary>
        public long date { get; set; }
        /// <summary>
        ///  order identifier.
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        ///  order type.
        /// </summary>
        public string type { get; set; }
        /// <summary>
        ///  currency pair.
        /// </summary>
        public string pair { get; set; }
        /// <summary>
        ///  price in the order.
        /// </summary>
        public double price { get; set; }
        /// <summary>
        ///  quantity in the order.
        /// </summary>
        public double quantity { get; set; }
        /// <summary>
        ///  sum of the order.
        /// </summary>
        public double amount { get; set; }
        /// <summary>
        ///  Constructor for error message creation.
        /// </summary>
        public Order(string error_message)
        {
            order_id = error_message;
            date = 0;
            type = "";
            pair = "";
            price = 0;
            quantity = 0;
            amount = 0;
        }
    }
    /// <summary>
    /// Availabel order types.
    /// </summary>
    public struct TypeOfOrder
    {
        public const string buy = "buy";
        public const string sell = "sell";
        public const string market_buy = "market_buy";
        public const string market_sell = "market_sell";
        public const string market_buy_total = "market_buy_total";
        public const string market_sell_total = "market_sell_total";
    }


}
