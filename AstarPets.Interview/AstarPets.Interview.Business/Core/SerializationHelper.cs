using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace AstarPets.Interview.Business.Core
{
    public static class SerializationHelper
    {
        /// <summary>
        /// Serialize the object using the DataContractSerializer
        /// </summary>
        /// <param name="encoding">The encoding to use when serializing.</param>
        /// <param name="entity">The entity to serialize.</param>
        /// <returns></returns>
        public static string DataContractSerialize<T>(Encoding encoding, T entity)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));

            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, entity);
                return encoding.GetString(ms.ToArray());
            }
        }

        /// <summary>
        /// Serialize the object using the DataContractSerializer, defaults to using UTF8 encoding
        /// </summary>
        /// <param name="entity">The entity to serialize.</param>
        /// <returns></returns>
        public static string DataContractSerialize<T>(T entity)
        {
            return DataContractSerialize(Encoding.UTF8, entity);
        }

        /// <summary>
        /// Deserialize the object using the DataContractSerializer
        /// </summary>
        /// <param name="encoding">The encoding to use when deserializing.</param>
        /// <param name="entity">The entity to deserialize.</param>
        /// <returns></returns>
        public static T DataContractDeserialize<T>(Encoding encoding, string entity)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));

            using (MemoryStream ms = new MemoryStream(encoding.GetBytes(entity)))
            {
                return (T)serializer.ReadObject(ms);
            }
        }


        /// <summary>
        /// Deserialize the object using the DataContractSerializer, defaults to using UTF8 encoding
        /// </summary>
        /// <param name="entity">The entity to deserialize.</param>
        /// <returns></returns>
        public static T DataContractDeserialize<T>(string entity)
        {
            return DataContractDeserialize<T>(Encoding.UTF8, entity);
        }
    }
}