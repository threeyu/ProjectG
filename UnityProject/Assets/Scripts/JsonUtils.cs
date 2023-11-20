using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace ppy {
    public static class JsonUtils {
        private static Encoding UTF8NoBOM = new UTF8Encoding(false);
        private static JsonSerializerSettings DefaultJsonSettings = new JsonSerializerSettings {
            NullValueHandling = NullValueHandling.Ignore
        };
        private static JsonSerializerSettings JsonSettingsFormat = new JsonSerializerSettings {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented
        };

        public static T FromJson<T>(string json) {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static T FromJson<T>(byte[] json) {
            var str = UTF8NoBOM.GetString(json);
            return JsonConvert.DeserializeObject<T>(str);
        }

        public static object FromJson(string json, Type type) {
            return JsonConvert.DeserializeObject(json, type);
        }

        public static object FromJson(byte[] json, Type type) {
            var str = UTF8NoBOM.GetString(json);
            return JsonConvert.DeserializeObject(str, type);
        }

        public static object FromJson(string ctx) {
            return JsonConvert.DeserializeObject(ctx);
        }

        public static object FromJson(byte[] ctx) {
            var str = UTF8NoBOM.GetString(ctx);
            return JsonConvert.DeserializeObject(str);
        }

        public static string ToJson(object obj) {
            return JsonConvert.SerializeObject(obj, DefaultJsonSettings);
        }

        public static string ToJsonFormat(object obj) {
            return JsonConvert.SerializeObject(obj, JsonSettingsFormat);
        }
    }
}
