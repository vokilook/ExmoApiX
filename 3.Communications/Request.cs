using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace ExmoApiX.Communications
{
    /// <summary>
    /// Base class for all public and authenticated API requests.
    /// </summary>
    class Request
    {
        /// <summary>
        /// Indicator of request succeedence.
        /// </summary>
        public bool RequestSucceed { get; protected set; } = true;
        /// <summary>
        /// Include request error in case of failure.
        /// </summary>
        public string RequestError { get; protected set; } = "";


        /// <summary>
        /// Analyze server answer if it's an error.
        /// </summary>
        /// <param name="jObject"> Server answer in JObject format</param>
        protected bool CheckResult(ref JObject jObject)
        {
            if (jObject.Count == 2 && jObject.First.ToString() == "\"result\": false")
            { 
                RequestError = jObject.Last.ToString();
                return RequestSucceed = false; 
            }
            return RequestSucceed = true;
        }
    }
}
