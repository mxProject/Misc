using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace ClassLibrary1
{

    public class SampleSerializer<T> : ISerializer<T>, IAsyncSerializer<T>, IDeserializer<T>, IAsyncDeserializer<T>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        byte[] ISerializer<T>.Serialize(T data, SerializationContext context)
        {
            return System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        Task<byte[]> IAsyncSerializer<T>.SerializeAsync(T data, SerializationContext context)
        {
            return Task.FromResult(System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(data));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="isNull"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        T IDeserializer<T>.Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(data)!;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="isNull"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        Task<T> IAsyncDeserializer<T>.DeserializeAsync(ReadOnlyMemory<byte> data, bool isNull, SerializationContext context)
        {
            return Task.FromResult(System.Text.Json.JsonSerializer.Deserialize<T>(data.ToArray())!);
        }

    }

}
