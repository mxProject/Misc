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
    /// <typeparam name="T"></typeparam>
    internal sealed class ClientStreamWriterWrapper<T> : IClientStreamWriter<T>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="streamWriter"></param>
        /// <param name="onComplete"></param>
        /// <param name="onWrite"></param>
        internal ClientStreamWriterWrapper(IClientStreamWriter<T> streamWriter, Func<IClientStreamWriter<T>, Task> onComplete, Func<IClientStreamWriter<T>, T, Task> onWrite)
        {
            m_StreamWriter = streamWriter;
            m_OnComplete = onComplete;
            m_OnWrite = onWrite;
        }

        private readonly IClientStreamWriter<T> m_StreamWriter;
        private readonly Func<IClientStreamWriter<T>, Task> m_OnComplete;
        private readonly Func<IClientStreamWriter<T>, T, Task> m_OnWrite;

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
        /// <returns></returns>
        public Task CompleteAsync()
        {
            return m_OnComplete(m_StreamWriter);
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
