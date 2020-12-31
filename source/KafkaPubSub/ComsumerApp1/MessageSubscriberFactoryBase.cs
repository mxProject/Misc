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
    /// サブスクライバーの生成処理の基本実装。
    /// </summary>
    public abstract class MessageSubscriberFactoryBase
    {

        /// <summary>
        /// インスタンスを生成します。
        /// </summary>
        /// <param name="subscriberSetting">サブスクライバーの動作設定</param>
        /// <param name="logger">ロガー</param>
        protected MessageSubscriberFactoryBase(MessageSubscriberSetting subscriberSetting, ILogger logger)
        {
            SubscriberSetting = subscriberSetting;
            Logger = logger;
        }

        /// <summary>
        /// サブスクライバーの動作設定を取得します。
        /// </summary>
        private MessageSubscriberSetting SubscriberSetting { get; }

        /// <summary>
        /// ロガーを取得します。
        /// </summary>
        private ILogger Logger { get; }

        /// <summary>
        /// サブスクライバーを生成します。
        /// </summary>
        /// <typeparam name="TKey">キーの型</typeparam>
        /// <typeparam name="TMessage">メッセージの型</typeparam>
        /// <returns>サブスクライバー</returns>
        public MessageSubscriber<TKey, TMessage> CreateSubscriber<TKey, TMessage>(string topic)
        {
            return new MessageSubscriber<TKey, TMessage>(
                GetDeserializer<TKey>()
                , GetDeserializer<TMessage>()
                , SubscriberSetting
                , topic
                , Logger
                );
        }

        /// <summary>
        /// シリアライザを取得します。
        /// </summary>
        /// <typeparam name="T">シリアライズ対象オブジェクトの型</typeparam>
        /// <returns>シリアライザ</returns>
        protected abstract IDeserializer<T> GetDeserializer<T>();

    }

}
