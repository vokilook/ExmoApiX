using System.Collections.Generic;
using ExmoApiX.Communications;
using Newtonsoft.Json.Linq;

namespace ExmoApiX.AuthenticatedAPIRequests
{
    /// <summary>
    ///  Request of the list of addresses for cryptocurrency deposit.
    /// </summary>
    class Deposit_Address : Request
    {
        /// <summary>
        /// list of addresses for cryptocurrency deposit.
        /// </summary>
        public Dictionary<string, string> depositAddress { get; } = new Dictionary<string, string>();


        /// <summary>
        /// Constructor of the list of addresses for cryptocurrency deposit.
        /// </summary>
        /// <param name="user"> Exmo exchange user</param>
        public Deposit_Address(User user)
        {
            JObject jObject = (JObject)ExmoAPI.GetJSONObject(
                "deposit_address", new Dictionary<string, string>(), user);
            if (!CheckResult(ref jObject))
            {
                depositAddress.Add(RequestError, jObject.Last.ToString());
                return;
            }
            foreach (var pair in jObject)
                depositAddress.Add(pair.Key, pair.Value.ToString());

        }
    }
}