using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Grpc.Core;

namespace mxProject.Helpers.GrpcHelper.Clients
{

    /// <summary>
    /// Extension methods of <see cref="IAsyncStreamReader{T}"/>.
    /// </summary>
    internal static class IAsyncStreamReaderExtensions
    {

        #region Wrap

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="streamReader"></param>
        /// <param name="onMoveNext"></param>
        /// <param name="onDispose"></param>
        /// <returns></returns>
        internal static IAsyncStreamReader<T> Wrap<T>(this IAsyncStreamReader<T> streamReader, Func<IAsyncStreamReader<T>, CancellationToken, Task<bool>> onMoveNext, Action<IAsyncStreamReader<T>> onDispose)
        {
            return new Core.AsyncStreamReaderWrapper<T>(streamReader, onMoveNext, onDispose);
        }

        #endregion

    }

}
