using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ExmoApiX.Communications;

namespace ExmoApiX.AuthenticatedAPIRequests
{
    /// <summary>
    /// Getting information about user's account.
    /// https://exmo.me/ru/api#/authenticated_api
    /// </summary>
    class User_Info : Request
    {
        /// <summary>
        /// user identificator.
        /// </summary>
        public string uid { get; }
        /// <summary>
        /// server date and time.
        /// </summary>
        public long server_date { get; }
        /// <summary>
        /// user's available balance.
        /// </summary>
        public Dictionary<string, double> balances { get; } = new Dictionary<string, double>();
        /// <summary>
        /// user's balance in orders.
        /// </summary>
        public Dictionary<string, double> reserved { get; } = new Dictionary<string, double>();
  
        
        /// <summary>
        /// Constructor of information about user's account.
        /// </summary>
        /// <param name="user"> Exmo exchange user</param>
        /// <remarks> User marked as ref for ID field filling</remarks>
        public User_Info (ref User user)
        {
            JObject jObject = (JObject)ExmoAPI.GetJSONObject(
                "user_info", new Dictionary<string, string>(), user);
            if (!CheckResult(ref jObject))
            {
                uid = RequestError;
                return;
            }

            user.userID = uid = jObject.ContainsKey("uid") ? 
                jObject["uid"].ToString() : "";
            server_date = jObject.ContainsKey("server_date") ?
                Convert.ToInt64(jObject["server_date"].ToString()) : 0;
            
            if (jObject.ContainsKey("balances"))
                balances = JsonConvert.DeserializeObject<Dictionary<string, double>>
                    (jObject["balances"].ToString());
            if (jObject.ContainsKey("reserved"))
                reserved = JsonConvert.DeserializeObject<Dictionary<string, double>>
                    (jObject["reserved"].ToString());
        }
    }
    /// <summary>
    /// User description class.
    /// </summary>
    class User
    {
        public string userID { get; set; }
        public string userName { get; }
        public string userPasword { get; }
        public string key { get; }
        public string secret { get; }
        public User(string userName, string userPasword, string key, string secret) : this(key, secret)
        {
            this.userName = userName;
            this.userPasword = userPasword;
        }
        public User(string key, string secret)
        {
            this.key = key;
            this.secret = secret;
        }
    }

}

