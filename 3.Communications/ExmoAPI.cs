using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Collections.Specialized;
using System.Web;
using System.Linq;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using ExmoApiX.AuthenticatedAPIRequests;

namespace ExmoApiX.Communications
{
    /// <summary>
    /// C# Exmo-dev / exmo_api_lib functions.
    /// https://github.com/exmo-dev/exmo_api_lib
    /// </summary>
    static class ExmoAPI
    {
        private static long _nounce;

        static ExmoAPI()
        {
        _nounce = Helpers.GetTimestamp();
        }
        public static JToken GetJSONObject(string command,
            IDictionary<string, string> parameters = null, User user = null)
        {
            JObject jObject = new JObject();
            string answer;
            try
            {
                answer = GetString(command, parameters, user);
            }
            catch (Exception ex)
            {
                jObject.Add("result", false);
                jObject.Add("error", ex.Message);
                return jObject;
            }
            try
            {
                if (answer[0] == '[')
                {
                    JArray jArray = new JArray();
                    jArray = JArray.Parse(answer);
                    return jArray;
                }
                else
                {
                    jObject = JObject.Parse(answer);
                }
            }
            catch (Exception ex)
            {
                jObject.Add("result", false);
                jObject.Add("error", ex.Message);
            };
            return jObject;
        }
        public static string GetString(string command,
            IDictionary<string, string> parameters = null, User user = null)
        {
            string _url = "http://api.exmo.com/v1/{0}";
            var wb = new WebClient();
            byte[] response;
            if (parameters != null && user != null)
            {
                parameters.Add("nonce", Convert.ToString(_nounce++));
                var message = ToQueryString(parameters);

                var sign = Sign(user.secret, message);

                wb.Headers.Add("Sign", sign);
                wb.Headers.Add("Key", user.key);

                var data = parameters.ToNameValueCollection();
                response = wb.UploadValues(string.Format(_url, command), "POST", data);
                return Encoding.UTF8.GetString(response);

            }
            else
                response = wb.DownloadData(string.Format(_url, command));

            return Encoding.UTF8.GetString(response);

        }
        private static string ToQueryString(IDictionary<string, string> dic)
        {
            var array = (from key in dic.Keys
                         select string.Format("{0}={1}",
                         HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(dic[key])))
                .ToArray();
            return string.Join("&", array);
        }

        public static string Sign(string key, string message)
        {
            using (HMACSHA512 hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key)))
            {
                byte[] b = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
                return ByteToString(b);
            }
        }
        private static string ByteToString(byte[] buff)
        {
            string sbinary = "";

            for (int i = 0; i < buff.Length; i++)
            {
                sbinary += buff[i].ToString("X2"); // hex format
            }
            return (sbinary).ToLowerInvariant();
        }
    }
}
/// <summary>
/// C# Exmo-dev / exmo_api_lib helper functions.
/// https://github.com/exmo-dev/exmo_api_lib
/// </summary>
public static class Helpers
{
    public static NameValueCollection ToNameValueCollection<TKey, TValue>(this IDictionary<TKey, TValue> dict)
    {
        var nameValueCollection = new NameValueCollection();

        foreach (var kvp in dict)
        {
            string value = string.Empty;
            if (kvp.Value != null)
                value = kvp.Value.ToString();

            nameValueCollection.Add(kvp.Key.ToString(), value);
        }

        return nameValueCollection;
    }

    public static long GetTimestamp()
    {
        var d = (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds;
        return (long)d;
    }
}
