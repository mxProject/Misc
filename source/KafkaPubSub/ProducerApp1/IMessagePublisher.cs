using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using ClassLibrary1;

namespace ProducerApp1
{

    /// <summary>
    /// パブリッシャーに必要な機能を提供します。
    /// </summary>
    /// <typeparam name="TMessage">メッセージの型</typeparam>
    public interface IMessagePublisher<TMessage> : IDisposable
    {

        #region メッセージ発行

        /// <summary>
        /// 指定されたメッセージを発行します。
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <returns></returns>
        public Task PublishAsync(TMessage message, CancellationToken cancellationToken);

        #endregion

    }

}
