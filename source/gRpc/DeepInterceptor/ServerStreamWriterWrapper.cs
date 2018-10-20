using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;

namespace mxProject.Helpers.GrpcHelper.Core
{

    /// <summary>
    /// 
    /// </summary>
    internal sealed class ServerStreamWriterWrapper<T> : IServerStreamWriter<T>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="streamWriter"></param>
        /// <param name="onWrite"></param>
        internal ServerStreamWriterWrapper(IServerStreamWriter<T> streamWriter, Func<IServerStreamWriter<T>, T, Task> onWrite)
        {
            m_StreamWriter = streamWriter;
            m_OnWrite = onWrite;
        }

        private readonly IServerStreamWriter<T> m_StreamWriter;
        private readonly Func<IServerStreamWriter<T>, T, Task> m_OnWrite;

        /// <summary>
        /// 
        /// </summary>
        public WriteOptions WriteOptions
        {
            get { return m_StreamWriter.WriteOptions; }
            set { m_StreamWriter.WriteOptions = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task WriteAsync(T message)
        {
            return m_OnWrite(m_StreamWriter, message);
        }

    }

}
