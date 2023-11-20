using System;
using Newtonsoft.Json;

namespace Ogopogo.FixedMath {
    public class FNumberConverter : JsonConverter<FNumber> {
        public override void WriteJson(JsonWriter writer, FNumber value, JsonSerializer serializer) {
            writer.WriteValue(value.ToString());
        }
    
        public override FNumber ReadJson(JsonReader reader, Type objectType, FNumber existingValue,
            bool hasExistingValue,
            JsonSerializer serializer) {
            var s = reader.Value as string;
            return FNumber.Parse(s);
        }
    }
}