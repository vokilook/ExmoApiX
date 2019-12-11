using System;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ExmoApiX.AuthenticatedAPIRequests;

namespace ExmoApiX.Communications
{
    /// <summary>
    /// Order resolver realization for "user_open_order" function.
    /// </summary>
    /// <remarks> Answer of "user_open_order" includes param "created" instead of "date".</remarks>
    class OrderDataContractResolver : DefaultContractResolver
    {
        public static readonly OrderDataContractResolver Instance = new OrderDataContractResolver();

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            if (property.DeclaringType == typeof(Order))
            {
                if (property.PropertyName.Equals("date", StringComparison.OrdinalIgnoreCase))
                {
                    property.PropertyName = "created";
                }
            }
            return property;
        }
    }
}
