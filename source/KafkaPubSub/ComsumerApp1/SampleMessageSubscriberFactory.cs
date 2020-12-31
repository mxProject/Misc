using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Confluent.Kafka;
using ClassLibrary1;

namespace ComsumerApp1
{

    /// <summary>
    /// サブスクライバーを生成します。
    /// </summary>
    internal class SampleMessageSubscriberFactory : MessageSubscriberFactoryBase
    {

        /// <summary>
        /// インスタンスを生成します。
        /// </summary>
        /// <param name="subscriberSetting">サブスクライバーの動作設定</param>
        /// <param name="logger">ロガー</param>
        internal SampleMessageSubscriberFactory(MessageSubscriberSetting subscriberSetting, ILogger logger)
            : base(subscriberSetting, logger)
        {
        }

        /// <summary>
        /// シリアライザを取得します。
        /// </summary>
        /// <typeparam name="T">シリアライズ対象オブジェクトの型</typeparam>
        /// <returns>シリアライザ</returns>
        protected override IDeserializer<T> GetDeserializer<T>()
        {
            return SampleSerializerFactory.Create<T>();
        }

    }

}
