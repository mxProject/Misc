using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;

namespace mxProject.Helpers.GrpcHelper.Core
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal sealed class AsyncStreamReaderWrapper<T> : IAsyncStreamReader<T>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="streamReader"></param>
        /// <param name="onMoveNext"></param>
        /// <param name="onDispose"></param>
        internal AsyncStreamReaderWrapper(IAsyncStreamReader<T> streamReader, Func<IAsyncStreamReader<T>, CancellationToken, Task<bool>> onMoveNext, Action<IAsyncStreamReader<T>> onDispose)
        {
            m_StreamReader = streamReader;
            m_OnMoveNext = onMoveNext;
            m_OnDispose = onDispose;
        }

        private readonly IAsyncStreamReader<T> m_StreamReader;
        private readonly Func<IAsyncStreamReader<T>, CancellationToken, Task<bool>> m_OnMoveNext;
        private readonly Action<IAsyncStreamReader<T>> m_OnDispose;

        /// <summary>
        /// 
        /// </summary>
        public T Current
        {
            get { return m_StreamReader.Current; }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            m_OnDispose(m_StreamReader);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> MoveNext(CancellationToken cancellationToken)
        {
            return await m_OnMoveNext(m_StreamReader, cancellationToken).ConfigureAwait(false);
        }

    }

}
