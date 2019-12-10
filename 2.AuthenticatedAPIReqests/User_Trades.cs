using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ExmoApiX.Communications;

namespace ExmoApiX.AuthenticatedAPIRequests
{
    /// <summary>
    /// Request of the list of user’s deals.
    /// </summary>
    class User_Trades : Request
    {
        /// <summary>
        /// lists of user’s deals (for each pair).
        /// </summary>
        public Dictionary<string,List<Order>> userTrades { get; } = new Dictionary<string, List<Order>>();


        /// <summary>
        /// Constructor of the list of user’s deals.
        /// </summary>
        /// <param name="user"> Exmo exchange user</param>
        /// <param name="pairs"> one or various currency pairs separated by commas (example: BTC_USD,BTC_EUR)</param>
        /// <param name="limit"> the number of returned deals (default: 100, мmaximum: 10 000)</param>
        /// <param name="offset"> last deal offset (default: 0)</param>
        public User_Trades(User user, List<string> pairs, int limit = 100, int offset = 0)
        {
            if (pairs == null)
            {
                RequestSucceed = false;
                RequestError = "Pair is not specified.";
                return;
            }

            string pairsString = "";
            int counter = 0;
            foreach (string pair in pairs)
            {
                pairsString += pair;
                if (++counter != pairs.Count) pairsString += ",";
            }
            Dictionary<string, string> parameters = new Dictionary<string, string>
            { {"pair", pairsString }, { "limit", limit.ToString()} , { "offset", offset.ToString()} };
            JObject jObject = (JObject)ExmoAPI.GetJSONObject("user_trades", parameters, user);

            if (!CheckResult(ref jObject))
            {
                userTrades.Add(jObject.First.ToString(),
                    new List<Order> { new Order(RequestError) });
                return;
            }
            foreach (string pair in pairs)
            {
                try
                {
                    List<Order> pairOrders =
                        JsonConvert.DeserializeObject<List<Order>>(jObject[pair].ToString());
                    userTrades.Add(pair, pairOrders);
                }
                catch (Exception ex)
                {
                    userTrades.Add(pair + ": " + ex.Message, new List<Order>());
                };
            }
        }
    }
}
