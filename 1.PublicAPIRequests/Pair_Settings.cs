using System;
using System.Collections.Generic;
using ExmoApiX.Communications;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ExmoApiX.PublicAPIRequests
{
    /// <summary>
    /// Currency pairs settings request.
    /// https://exmo.me/en/api#public_api
    /// </summary>
    class Pair_Settings : Request
    {
        /// <summary>
        /// Dictionary with pairs and appropriate settings.
        /// </summary>
        public Dictionary<string, Settings> pair_settings { get; } = 
            new Dictionary<string, Settings>();
        /// <summary>
        /// Pair_Settings constructor.
        /// </summary>
        public Pair_Settings()
        {
            JObject jObject = (JObject)ExmoAPI.GetJSONObject("pair_settings/");
            if (!CheckResult(ref jObject))
            {
                pair_settings.Add(RequestError, new Settings());
                return;
            }

            foreach (var pair in jObject)
            {
                try
                {
                    Settings pairSettings = 
                        JsonConvert.DeserializeObject<Settings>(pair.Value.ToString());
                    pair_settings.Add(pair.Key, pairSettings);
                }
                catch (Exception ex)
                {
                    pair_settings.Add(pair + ": " + ex.Message, new Settings());
                };
            }
        }
    }
    /// <summary>
    /// Settings description class.
    /// </summary>
    class Settings
    {
        /// <summary>
        /// minimum quantity for the order.
        /// </summary>
        public string min_quantity { get; set; }
        /// <summary>
        /// maximum quantity for the order.
        /// </summary>
        public string max_quantity { get; set; }
        /// <summary>
        /// minimum price for the order.
        /// </summary>
        public string min_price { get; set; }
        /// <summary>
        /// maximum price for the order.
        /// </summary>
        public string max_price { get; set; }
        /// <summary>
        /// minimum total sum for the order.
        /// </summary>
        public string min_amount { get; set; }
        /// <summary>
        /// maximum total sum for the order.
        /// </summary>
        public string max_amount { get; set; }

    }
}
