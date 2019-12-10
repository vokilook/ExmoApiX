using System;
using System.Collections.Generic;
using ExmoApiX.Communications;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ExmoApiX.PublicAPIRequests
{
    /// <summary>
    /// List of the deals on currency pairs request.
    /// https://exmo.me/en/api#public_api
    /// </summary>
    class Trades : Request
    {
        /// <summary>
        /// Dictionary with pairs and appropriate deals.
        /// </summary>
        public Dictionary<string, List<Trade>> trades { get; } = new Dictionary<string, List<Trade>>();
        /// <summary>
        /// Trades constructor with incoming list of pairs.
        /// </summary>
        public Trades(List<string> pairs, int limit = 100)
        {
            if (pairs == null)
            {
                RequestSucceed = false;
                RequestError = "Pair is not specified.";
                return;
            }

            string request = "trades/?pair=";
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
                trades.Add(RequestError, new List<Trade>());
                return;
            }

            foreach (string pair in pairs)
            {
                try
                {
                    List<Trade> pairTrades =
                        JsonConvert.DeserializeObject<List<Trade>>(jObject[pair].ToString());
                    trades.Add(pair, pairTrades);
                }
                catch (Exception ex)
                {
                    trades.Add(pair + ": " + ex.Message, new List<Trade>());
                };
            }
        }
    }
    /// <summary>
    /// Deal description class.
    /// </summary>
    class Trade
    {
        /// <summary>
        /// deal identifier.
        /// </summary>
        public long trade_id { get; set; }
        /// <summary>
        /// type of the deal.
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// deal price.
        /// </summary>
        public double price { get; set; }
        /// <summary>
        /// currency quantity.
        /// </summary>
        public double quantity { get; set; }
        /// <summary>
        /// total sum of the deal.
        /// </summary>
        public double amount { get; set; }
        /// <summary>
        /// date and time of the deal.
        /// </summary>
        [JsonConverter(typeof(UnixTimeToDatetimeConverter))]
        public DateTime date { get; set; }
    }
}
