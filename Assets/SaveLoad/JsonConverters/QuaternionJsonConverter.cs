using Newtonsoft.Json;
using System;
using UnityEngine;

namespace SaveLoad
{
    public class QuaternionJsonConverter : JsonConverter
    {
        public QuaternionJsonConverter()
        {
        }

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Quaternion));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var objFromJson = serializer.Deserialize(reader);
            return JsonConvert.DeserializeObject<Quaternion>(objFromJson.ToString());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Quaternion q = (Quaternion)value;

            writer.WriteStartObject();
            writer.WritePropertyName("x");
            writer.WriteValue(q.x);
            writer.WritePropertyName("y");
            writer.WriteValue(q.y);
            writer.WritePropertyName("z");
            writer.WriteValue(q.z);
            writer.WritePropertyName("w");
            writer.WriteValue(q.w);
            writer.WriteEndObject();
        }
    }
}