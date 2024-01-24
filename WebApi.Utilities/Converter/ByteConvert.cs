using Newtonsoft.Json;
using System;
using WebApi.Utilities.Extensions;

namespace WebApi.Utilities.Converter
{
    public class ByteConvert : Newtonsoft.Json.JsonConverter
    {
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
                return reader.Value.ToString().ToHexByte();
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

            writer.WriteValue(((byte)value).ToHexStr());
        }
    }
}
