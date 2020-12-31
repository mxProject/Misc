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
    /// パブリッシャーの生成処理の基本実装。
    /// </summary>
    public abstract class MessagePublisherFactoryBase
    {

        /// <summary>
        /// インスタンスを生成します。
        /// </summary>
        /// <param name="publisherSetting">パブリッシャーの動作設定</param>
        /// <param name="logger">ロガー</param>
        protected MessagePublisherFactoryBase(MessagePublisherSetting publisherSetting, ILogger logger)
        {
            PublisherSetting = publisherSetting;
            Logger = logger;
        }

        /// <summary>
        /// パブリッシャーの動作設定を取得します。
        /// </summary>
        protected MessagePublisherSetting PublisherSetting { get; }

        /// <summary>
        /// ロガーを取得します。
        /// </summary>
        private ILogger Logger { get; }

        /// <summary>
        /// パブリッシャーを生成します。
        /// </summary>
        /// <typeparam name="TKey">キーの型</typeparam>
        /// <typeparam name="TMessage">メッセージの型</typeparam>
        /// <param name="topic">トピック</param>
        /// <param name="keyGenerator">キーの生成処理</param>
        /// <returns>パブリッシャー</returns>
        public MessagePublisher<TKey, TMessage> CreatePublisher<TKey, TMessage>(string topic, Func<TKey> keyGenerator)
        {
            return new MessagePublisher<TKey, TMessage>(
                GetSerializer<TKey>()
                , GetSerializer<TMessage>()
                , PublisherSetting
                , topic
                , keyGenerator
                , Logger
                );
        }

        /// <summary>
        /// シリアライザを取得します。
        /// </summary>
        /// <typeparam name="T">シリアライズ対象オブジェクトの型</typeparam>
        /// <returns>シリアライザ</returns>
        protected abstract ISerializer<T> GetSerializer<T>();

    }

}
