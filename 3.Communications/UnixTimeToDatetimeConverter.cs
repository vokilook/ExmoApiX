﻿using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ExmoApiX.Communications
{
    /// <summary>
    /// JSON unix time to Datetime information converter.
    /// </summary>
    /// <remarks> WriteJson function is not implemented!!!</remarks>
    class UnixTimeToDatetimeConverter : DateTimeConverterBase
    {
        private static readonly DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);


        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, 
            object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
            {
                return null;
            }

            return _epoch.AddSeconds(Convert.ToDouble(reader.Value)).ToLocalTime();
        }


    }
}
