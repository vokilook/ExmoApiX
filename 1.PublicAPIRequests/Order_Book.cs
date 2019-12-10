using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ExmoApiX.Communications;

namespace ExmoApiX.PublicAPIRequests
{
    /// <summary>
    /// The book of actual orders on the appropriate currency pairs request.
    /// https://exmo.me/en/api#public_api
    /// </summary>
    class Order_Book : Request
    {
        /// <summary>
        /// Dictionary with pairs and appropriate orders.
        /// </summary>
        public Dictionary<string, Orders> orderBook { get; } = new Dictionary<string, Orders>();
        /// <summary>
        /// Orders book constructor with incoming list of pairs.
        /// </summary>
        /// <param name="pairs"> one or various currency pairs separated by commas (example: BTC_USD,BTC_EUR)</param>
        /// <param name="limit"> the number of displayed positions (default: 100, max: 1000)</param>
        public Order_Book (List<string> pairs, int limit = 100)
        {
            if (pairs == null)
            {
                RequestSucceed = false;
                RequestError = "Pair is not specified.";
                return;
            }

            string request = "order_book/?pair=";
            int counter = 0;
            foreach (string pair in pairs)
            {
                request += pair;
                if (++counter != pairs.Count) request += ",";
            }
            request += "&limit=" + limit;

            JObject jObject = (JObject)ExmoAPI.GetJSONObject(request);
            if (!CheckResult(ref jObject))
            {
                orderBook.Add(RequestError, new Orders());
                return;
            }

            foreach (string pair in pairs)
            {
                try
                {
                    Orders pairOrders =
                        JsonConvert.DeserializeObject<Orders>(jObject[pair].ToString());
                    orderBook.Add(pair, pairOrders);
                }
                catch (Exception ex)
                {
                    orderBook.Add(pair + ": " + ex.Message, new Orders());
                };
            }

        }
    }
    /// <summary>
    /// Orders description class.
    /// </summary>
    class Orders
    {
        /// <summary>
        /// the sum of all quantity values in sell orders.
        /// </summary>
        public double ask_quantity { get; set; }
        /// <summary>
        /// the sum of all total sum values in sell orders.
        /// </summary>
        public double ask_amount { get; set; }
        /// <summary>
        /// minimum sell price.
        /// </summary>
        public double ask_top { get; set; }
        /// <summary>
        /// the sum of all quantity values in buy orders.
        /// </summary>
        public double bid_quantity { get; set; }
        /// <summary>
        /// the sum of all total sum values in buy orders.
        /// </summary>
        public double bid_amount { get; set; }
        /// <summary>
        /// maximum buy price.
        /// </summary>
        public double bid_top { get; set; }
        /// <summary>
        /// the list of sell orders where every field is: price, quantity and amount.
        /// </summary>
        public List<List<double>> ask { get; set; }
        /// <summary>
        /// the list of buy orders where every field is: price, quantity and amount.
        /// </summary>
        public List<List<double>> bid { get; set; }

    }
}
