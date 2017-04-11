using System;
using System.IO;
using Newtonsoft.Json;

namespace Stubsharp.Utility.Serializers
{
    public class JsonNetSerializer : ISerializer
    {
        public JsonNetSerializer()
        {
            _settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore
            };

            _serializer = JsonSerializer.Create(_settings);
        }

        /// <summary>
        /// The format of the serializer
        /// </summary>
        public string Format { get; } = "json";

        /// <summary>
        /// Serialize an object to a string
        /// </summary>
        public string Serialize(object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }

            using (TextWriter tw = new StringWriter())
            {
                _serializer.Serialize(tw, obj);

                return tw.ToString();
            }
        }

        /// <summary>
        /// Deserialize a string into an object.
        /// </summary>
        public T Deserialize<T>(string data)
        {
            return (T)Deserialize(data, typeof(T));
        }

        /// <summary>
        /// Deserialize a stream into an object
        /// </summary>
        public T Deserialize<T>(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                using (var jReader = new JsonTextReader(reader))
                {
                    return _serializer.Deserialize<T>(jReader);
                }
            }
        }

        /// <summary>
        /// Deserialize a string into an object.
        /// </summary>
        public object Deserialize(string data, Type type)
        {
            if ( string.IsNullOrEmpty(data) )
            {
                return null;
            }

            if ( type == typeof(string) )
            {
                return data;
            }

            return JsonConvert.DeserializeObject(data, type, _settings);
        }

        private readonly JsonSerializer _serializer;
        private readonly JsonSerializerSettings _settings;
    }
}
