using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using RamowkiWithOpenIddictAuth.Models;

namespace RamowkiWithOpenIddictAuth.Utils
{
    public class DayConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Day).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject item = JObject.Load(reader);
            if( item["Day"] != null )
            {
                return item.ToObject<WeekDay>();
            }
            else if (item["Date"] != null)
            {
                return item.ToObject<DateDay>();
            }
            else throw new JsonSerializationException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {}
    }
}
