using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Confluent.Kafka;
using ClassLibrary1;

namespace ProducerApp1
{

    /// <summary>
    /// パブリッシャーを生成します。
    /// </summary>
    internal class SampleMessagePublisherFactory : MessagePublisherFactoryBase
    {

        /// <summary>
        /// インスタンスを生成します。
        /// </summary>
        /// <param name="publisherSetting">パブリッシャーの動作設定</param>
        /// <param name="logger">ロガー</param>
        internal SampleMessagePublisherFactory(MessagePublisherSetting publisherSetting, ILogger logger)
            : base(publisherSetting, logger)
        {
        }

        /// <summary>
        /// シリアライザを取得します。
        /// </summary>
        /// <typeparam name="T">シリアライズ対象オブジェクトの型</typeparam>
        /// <returns>シリアライザ</returns>
        protected override ISerializer<T> GetSerializer<T>()
        {
            return SampleSerializerFactory.Create<T>();
        }

    }

}
