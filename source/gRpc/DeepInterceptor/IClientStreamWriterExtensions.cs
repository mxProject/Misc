using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Grpc.Core;

namespace mxProject.Helpers.GrpcHelper.Clients
{

    /// <summary>
    /// Extension methods of <see cref="IClientStreamWriter{T}"/>.
    /// </summary>
    internal static class IClientStreamWriterExtensions
    {

        #region Wrap

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="streamWriter"></param>
        /// <param name="onComplete"></param>
        /// <param name="onWrite"></param>
        /// <returns></returns>
        internal static IClientStreamWriter<T> Wrap<T>(this IClientStreamWriter<T> streamWriter, Func<IClientStreamWriter<T>, Task> onComplete, Func<IClientStreamWriter<T>, T, Task> onWrite)
        {
            return new Core.ClientStreamWriterWrapper<T>(streamWriter, onComplete, onWrite);
        }

        #endregion

    }

}
