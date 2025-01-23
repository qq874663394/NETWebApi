using Newtonsoft.Json;
using System;
using WebApi.Utilities.Extensions;

namespace WebApi.Utilities.Converter
{
    public class DynamicConvert : Newtonsoft.Json.JsonConverter
    {
        public DynamicConvert()
        {

        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            bool isNullable = IsNullableType(objectType);

            Type t = isNullable ? Nullable.GetUnderlyingType(objectType) : objectType;

            if (reader.TokenType == JsonToken.Null)
            {
                if (!IsNullableType(objectType))
                {
                    throw new Exception(string.Format("不能转换null value to {0}.", objectType));
                }

                return null;
            }

            try
            {
                //数值
                return reader.Value;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error converting value {0} to type '{1}'", reader.Value, objectType), ex);
            }

            throw new Exception(string.Format("Unexpected token {0} when parsing enum", reader.TokenType));
        }

        /// <summary>
        /// 判断是否可以转换
        /// </summary>
        /// <param name="objectType">类型</param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return true;
        }


        public bool IsNullableType(Type t)
        {
            if (t == null)
            {
                throw new ArgumentNullException("t");
            }

            return t.FullName == "System.ValueType" && t.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            if (value is byte)
                writer.WriteValue(((byte)value).ToString("G"));
            else if (value is byte[])
                writer.WriteValue((value as byte[]).ToHexStr());
            else if (value is ushort || value is uint || value is int)
                writer.WriteValue(value.ToString());
            else if (value is decimal)
                writer.WriteValue(((decimal)value).ToStringByDigit(2));
            else if (value is string)
                writer.WriteValue(value);
            else if (value is DateTime)
                writer.WriteValue(((DateTime)value).ToString("yyMMddHHmmss"));
        }
    }
}
