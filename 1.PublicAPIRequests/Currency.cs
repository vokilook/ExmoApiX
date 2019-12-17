using System;
using System.Collections.Generic;
using ExmoApiX.Communications;
using Newtonsoft.Json.Linq;

namespace ExmoApiX.PublicAPIRequests
{
    /// <summary>
    /// Currencies list request.
    /// https://exmo.me/en/api#public_api
    /// </summary>
    class Currency : Request
    {
        /// <summary>
        /// List with available currencies.
        /// </summary>
        public List<string> currencies { get; } = new List<string>();
        /// <summary>
        /// Currency list constructor.
        /// </summary>
        public Currency()
        {
            JToken jArray = ExmoAPI.GetJSONObject("currency");
            if (jArray is JArray)
                currencies = jArray.ToObject<List<string>>();
            else
            {
                JObject jObject = (JObject)jArray;
                if (jObject.ContainsKey("result"))
                    RequestSucceed = Convert.ToBoolean(jObject["result"]);
                else
                    RequestSucceed = false;
                if (jObject.ContainsKey("error"))
                    RequestError = jObject["error"].ToString();
                foreach (KeyValuePair<string, JToken> item in jObject)
                    currencies.Add(item.ToString());
            }

        }
    }
}