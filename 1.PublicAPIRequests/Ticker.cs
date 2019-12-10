using System;
using System.Collections.Generic;
using ExmoApiX.Communications;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ExmoApiX.PublicAPIRequests
{
    /// <summary>
    /// Statistics on prices and volume of trades by currency pairs request.
    /// https://exmo.me/en/api#public_api
    /// </summary>
    class Ticker : Request
    {
        /// <summary>
        /// Dictionary with pairs and appropriate statistics.
        /// </summary>
        public Dictionary<string, Tick> ticker { get; } = new Dictionary<string, Tick>();
        /// <summary>
        /// Ticker constructor.
        /// </summary>
        public Ticker()
        {
            JObject jObject = (JObject)ExmoAPI.GetJSONObject("ticker/");
            if (!CheckResult(ref jObject))
            {
                ticker.Add(RequestError, new Tick());
                return;
            }

            foreach (var pair in jObject)
            {
                try
                {
                    Tick pairTick = JsonConvert.DeserializeObject<Tick>(pair.Value.ToString());
                    ticker.Add(pair.Key, pairTick);
                }
                catch (Exception ex)
                {
                    ticker.Add(pair + ": " + ex.Message, new Tick());
                };
            }

        }
    }
    /// <summary>
    /// One "tick" description class.
    /// </summary>
    class Tick
    {
        /// <summary>
        /// maximum deal price within the last 24 hours.
        /// </summary>
        public double high { get; set; }
        /// <summary>
        /// minimum deal price within the last 24 hours.
        /// </summary>
        public double low { get; set; }
        /// <summary>
        /// average deal price within the last 24 hours.
        /// </summary>
        public double avg { get; set; }
        /// <summary>
        /// the volume of deals within the last 24 hours.
        /// </summary>
        public double vol { get; set; }
        /// <summary>
        /// the total value of all deals within the last 24 hours.
        /// </summary>
        public double vol_curr { get; set; }
        /// <summary>
        /// last deal price.
        /// </summary>
        public double last_trade { get; set; }
        /// <summary>
        /// current maximum buy price.
        /// </summary>
        public double buy_price { get; set; }
        /// <summary>
        /// current minimum sell price.
        /// </summary>
        public double sell_price { get; set; }
        /// <summary>
        /// date and time of data update.
        /// </summary>
        [JsonConverter(typeof(UnixTimeToDatetimeConverter))]
        public DateTime updated { get; set; }
    }
}
