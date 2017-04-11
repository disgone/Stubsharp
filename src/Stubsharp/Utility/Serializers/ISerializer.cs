using System;
using System.IO;

namespace Stubsharp.Utility.Serializers
{
    public interface ISerializer
    {
        /// <summary>
        /// The format of the serializer
        /// </summary>
        string Format { get; }

        /// <summary>
        /// Serialize an object to a string
        /// </summary>
        string Serialize(object obj);

        /// <summary>
        /// Deserialize a string into an object.
        /// </summary>
        T Deserialize<T>(string data);

        /// <summary>
        /// Deserialize a string into an object.
        /// </summary>
        object Deserialize(string data, Type type);

        /// <summary>
        /// Deserialize a stream into an object
        /// </summary>
        T Deserialize<T>(Stream stream);
    }
}
