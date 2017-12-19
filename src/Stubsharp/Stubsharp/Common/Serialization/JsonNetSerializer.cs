using System;
using System.IO;
using Newtonsoft.Json;

namespace Stubsharp.Common.Serialization
{
    public class JsonNetSerializer : ISerializer
    {
        private readonly JsonSerializer _serializer;

        public JsonNetSerializer()
        {
            _serializer = JsonSerializer.Create();
        }

        public JsonNetSerializer(JsonSerializerSettings settings)
        {
            _serializer = JsonSerializer.Create(settings);
        }

        /// <summary>
        ///     The format of the serializer
        /// </summary>
        public string Format { get; } = "json";

        /// <summary>
        ///     Serialize an object to a string
        /// </summary>
        public string Serialize(object obj)
        {
            using ( TextWriter tw = new StringWriter() )
            {
                if ( obj == null )
                {
                    obj = string.Empty;
                }
                _serializer.Serialize(tw, obj);
                return tw.ToString();
            }
        }

        /// <summary>
        ///     Deserialize a string into an object.
        /// </summary>
        public T Deserialize<T>(string data)
        {
            if ( string.IsNullOrEmpty(data) )
            {
                return default( T );
            }

            if ( typeof( T ) == typeof( string ) )
            {
                return (T) (object) data;
            }

            var reader = new StringReader(data);

            try
            {
                using ( var jReader = new JsonTextReader(reader) )
                {
                    reader = null;
                    return _serializer.Deserialize<T>(jReader);
                }
            }
            finally
            {
                reader?.Dispose();
            }
        }

        /// <summary>
        ///     Deserialize a string into an object.
        /// </summary>
        public object Deserialize(string data, Type type)
        {
            if ( string.IsNullOrEmpty(data) )
            {
                return null;
            }

            if ( type == typeof( string ) )
            {
                return data;
            }

            var reader = new StringReader(data);

            try
            {
                using ( var jReader = new JsonTextReader(reader) )
                {
                    reader = null;
                    return _serializer.Deserialize(jReader, type);
                }
            }
            finally
            {
                reader?.Dispose();
            }
        }

        /// <summary>
        ///     Deserialize a stream into an object
        /// </summary>
        public T Deserialize<T>(Stream stream)
        {
            var reader = new StreamReader(stream);

            try
            {
                using ( var jReader = new JsonTextReader(reader) )
                {
                    reader = null;
                    return _serializer.Deserialize<T>(jReader);
                }
            }
            finally
            {
                reader?.Dispose();
            }
        }
    }
}
