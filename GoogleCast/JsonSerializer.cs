using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace GoogleCast
{
    /// <summary>
    /// Json serializer
    /// </summary>
    public static class JsonSerializer
    {
        /// <summary>
        /// Serializes an object to a JSON UTF-8 string
        /// </summary>
        /// <param name="obj">object to serialize</param>
        /// <returns>a string corresponding to the serialized object</returns>
        public static string SerializeToUTF8String(object obj)
        {
            return Encoding.UTF8.GetString(Serialize(obj));
        }

        /// <summary>
        /// Serializes an object to JSON
        /// </summary>
        /// <param name="obj">object to serialize</param>
        /// <returns>a JSON byte array</returns>
        public static byte[] Serialize(object obj)
        {
            using var ms = new MemoryStream();
            new DataContractJsonSerializer(obj.GetType()).WriteObject(ms, obj);
            return ms.ToArray();
        }

        /// <summary>
        /// Deserializes an JSON UTF-8 string to an object
        /// </summary>
        /// <typeparam name="T">object type</typeparam>
        /// <param name="utf8String">JSON UTF-8 string</param>
        /// <returns>the corresponding object</returns>
        public static T? Deserialize<T>(string utf8String) where T : class
        {
            return (T?)Deserialize(typeof(T), utf8String);
        }

        /// <summary>
        /// Deserializes an JSON byte array to an object
        /// </summary>
        /// <typeparam name="T">object type</typeparam>
        /// <param name="str">JSON byte array</param>
        /// <returns>the corresponding object</returns>
        public static T? Deserialize<T>(byte[] str) where T : class
        {
            return (T?)Deserialize(typeof(T), str);
        }

        /// <summary>
        /// Deserializes an JSON UTF-8 string to an object
        /// </summary>
        /// <param name="type">object type</param>
        /// <param name="utf8String">JSON UTF-8 string</param>
        /// <returns>the corresponding object</returns>
        public static object? Deserialize(Type type, string utf8String)
        {
            return Deserialize(type, string.IsNullOrWhiteSpace(utf8String) ? null : Encoding.UTF8.GetBytes(utf8String));
        }

        /// <summary>
        /// Deserializes an JSON byte array to an object
        /// </summary>
        /// <param name="type">object type</param>
        /// <param name="str">JSON byte array</param>
        /// <returns>the corresponding object</returns>
        public static object? Deserialize(Type type, byte[]? str)
        {
            if (str != null)
            {
                using var ms = new MemoryStream(str);
                return new DataContractJsonSerializer(type).ReadObject(ms);
            }

            return null;
        }
    }
}
