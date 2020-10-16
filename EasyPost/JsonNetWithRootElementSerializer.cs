using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Extensions;
using RestSharp.Serialization;
using RestSharp.Serializers.NewtonsoftJson;

namespace EasyPost
{
    public class JsonNetWithRootElementSerializer : JsonNetSerializer, IWithRootElement, IDeserializer
    {
        private readonly JsonSerializer serializer;
        public string RootElement { get; set; }

        /// <summary>
        /// Create the new serializer that uses Json.Net with default settings
        /// </summary>
        public JsonNetWithRootElementSerializer() => serializer = JsonSerializer.Create(DefaultSettings);

        /// <summary>
        /// Create the new serializer that uses Json.Net with custom settings
        /// </summary>
        /// <param name="settings">Json.Net serializer settings</param>
        public JsonNetWithRootElementSerializer(JsonSerializerSettings settings) : base(settings) =>
            serializer = JsonSerializer.Create(settings);

        public new T Deserialize<T>(IRestResponse response)
        {
            var json = base.Deserialize<JToken>(response);

            if (json is JObject jsonObj)
            {
                if (RootElement.HasValue() && jsonObj.ContainsKey(RootElement))
                    return jsonObj[RootElement].ToObject<T>(serializer);

                return json.ToObject<T>(serializer);
            }

            return json.ToObject<T>(serializer);
        }
    }
}