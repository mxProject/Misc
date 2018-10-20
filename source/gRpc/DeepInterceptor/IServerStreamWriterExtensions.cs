using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;

namespace mxProject.Helpers.GrpcHelper.Servers
{

    /// <summary>
    /// 
    /// </summary>
    internal static class IServerStreamWriterExtensions
    {

        #region Wrap

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="streamWriter"></param>
        /// <param name="onWrite"></param>
        /// <returns></returns>
        internal static IServerStreamWriter<T> Wrap<T>(this IServerStreamWriter<T> streamWriter, Func<IServerStreamWriter<T>, T, Task> onWrite)
        {
            return new Core.ServerStreamWriterWrapper<T>(streamWriter, onWrite);
        }

        #endregion

    }

}
