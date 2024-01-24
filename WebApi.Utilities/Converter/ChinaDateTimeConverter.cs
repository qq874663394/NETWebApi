using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace WebApi.Utilities.Converter
{
    public class ChinaDateTimeConverter : DateTimeConverterBase
    {
        IsoDateTimeConverter convert;

        public ChinaDateTimeConverter()
        {
            convert = dtConverterLong;
        }

        public ChinaDateTimeConverter(string type)
        {
            if (type.ToLower() == "long")
            {
                convert = dtConverterLong;
            }
            else if (type.ToLower() == "date")
            {
                convert = dtConverterDate;
            }
            else
            {
                convert = dtConverterShort;
            }
        }

        private static IsoDateTimeConverter dtConverterLong = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fffffff" };
        private static IsoDateTimeConverter dtConverterShort = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
        private static IsoDateTimeConverter dtConverterDate = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd" };

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value.ToString().Length == 19)
                return dtConverterShort.ReadJson(reader, objectType, existingValue, serializer);
            else if (reader.Value.ToString().Length == 10)
                return dtConverterDate.ReadJson(reader, objectType, existingValue, serializer);
            else
                return dtConverterLong.ReadJson(reader, objectType, existingValue, serializer);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            convert.WriteJson(writer, value, serializer);
        }
    }
}
