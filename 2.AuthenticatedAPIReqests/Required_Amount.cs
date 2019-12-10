using System;
using System.Collections.Generic;
using ExmoApiX.Communications;
using Newtonsoft.Json.Linq;

namespace ExmoApiX.AuthenticatedAPIRequests
{
    /// <summary>
    ///  Request of calculating the sum of buying a certain amount of currency 
    ///  for the particular currency pair.
    /// </summary>
    class Required_Amount : Request
    {
        /// <summary>
        ///  quantity you can to buy. -1 in case of request failure
        /// </summary>
        public double quantity { get; } = -1;
        /// <summary>
        ///  the sum you will spend. -1 in case of request failure
        /// </summary>
        public double amount { get; } = -1;
        /// <summary>
        ///  average buy price. -1 in case of request failure
        /// </summary>
        public double avg_price { get; } = -1;


        /// <summary>
        /// Constructor of order cancellation.
        /// </summary>
        /// <param name="pair"> currency pair</param>
        /// <param name="quantity"> quantity to buy</param>
        public Required_Amount(string pair, double quantity)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>
                { {"pair", pair}, {"quantity", quantity.ToString().Replace(',', '.') } };
            JObject jObject = (JObject)ExmoAPI.GetJSONObject(
                "required_amount", parameters, new User("",""));
            if (!CheckResult(ref jObject)) return;

            if (jObject.ContainsKey("quantity"))
                quantity = Convert.ToDouble(jObject["quantity"]);
            if (jObject.ContainsKey("amount"))
                amount = Convert.ToDouble(jObject["amount"]);
            if (jObject.ContainsKey("avg_price"))
                avg_price = Convert.ToDouble(jObject["avg_price"]);
        }
    }
}
