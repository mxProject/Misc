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
    /// Kafka にメッセージを送信します。
    /// </summary>
    /// <typeparam name="TKey">キーの型</typeparam>
    /// <typeparam name="TMessage">メッセージの型</typeparam>
    public class MessagePublisher<TKey, TMessage> : IMessagePublisher<TMessage>
    {

        /// <summary>
        /// インスタンスを生成します。
        /// </summary>
        /// <param name="keySerializer">キーに対するシリアライザ</param>
        /// <param name="messageSerializer">メッセージに対するシリアライザ</param>
        /// <param name="setting">プロデューサーの動作設定</param>
        /// <param name="keyGenerator">キーの生成処理</param>
        /// <param name="topic">トピック</param>
        /// <param name="logger">ロガー</param>
        public MessagePublisher(ISerializer<TKey> keySerializer, ISerializer<TMessage> messageSerializer, MessagePublisherSetting setting, string topic, Func<TKey> keyGenerator, ILogger logger)
        {
            KeySerializer = keySerializer;
            MessageSerializer = messageSerializer;
            Topic = topic;
            KeyGenerator = keyGenerator;
            Logger = logger;
            Producer = BuildProducer(GetProducerConfig(setting));
        }

        /// <summary>
        /// 使用しているリソースを解放します。
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 使用しているリソースを解放します。
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            TerminateProducer();
        }

        /// <summary>
        /// キーに対するシリアライザを取得します。
        /// </summary>
        private ISerializer<TKey> KeySerializer { get; }

        /// <summary>
        /// メッセージに対するシリアライザを取得します。
        /// </summary>
        private ISerializer<TMessage> MessageSerializer { get; }

        /// <summary>
        /// ロガーを取得します。
        /// </summary>
        private ILogger Logger { get; }

        /// <summary>
        /// トピックを取得します。
        /// </summary>
        private string Topic { get; }

        /// <summary>
        /// キーの生成処理を取得します。
        /// </summary>
        private Func<TKey> KeyGenerator { get; }

        #region プロデューサー

        /// <summary>
        /// プロデューサーを取得します。
        /// </summary>
        private IProducer<TKey, TMessage> Producer { get; }

        /// <summary>
        /// プロデューサーを解放します。
        /// </summary>
        private void TerminateProducer()
        {
            if (Producer == null) { return; }

            Producer.Flush(TimeSpan.FromMilliseconds(10000));
            Producer.Dispose();
        }

        /// <summary>
        /// プロデューサーを生成します。
        /// </summary>
        /// <param name="config">動作設定</param>
        /// <returns>プロデューサー</returns>
        protected virtual IProducer<TKey, TMessage> BuildProducer(IEnumerable<KeyValuePair<string, string>> config)
        {
            var producerBuilder = new ProducerBuilder<TKey, TMessage>(config)
                .SetKeySerializer(KeySerializer)
                .SetValueSerializer(MessageSerializer)
                .SetErrorHandler(OnError)
                ;

            return producerBuilder.Build();
        }

        /// <summary>
        /// プロデューサーの動作設定を取得します。
        /// </summary>
        /// <param name="producerSetting">プロデューサーの動作設定</param>
        /// <returns>動作設定のキーと値の組み合わせ</returns>
        protected virtual IEnumerable<KeyValuePair<string, string>> GetProducerConfig(MessagePublisherSetting producerSetting)
        {
            if (producerSetting.BootstrapServers == null || producerSetting.BootstrapServers == "")
            {
                throw new NullReferenceException("ブートストラップサーバーが設定されていません。");
            }

            return new ProducerConfig()
            {
                BootstrapServers = producerSetting.BootstrapServers,
            };
        }

        /// <summary>
        /// エラーが発生したときの処理を行います。
        /// </summary>
        /// <param name="producer"></param>
        /// <param name="error"></param>
        protected virtual void OnError(IProducer<TKey, TMessage> producer, Error error)
        {
            WriteLog(LogLevel.Error, () => BuildLogMessage(error));
        }

        #endregion

        #region メッセージ発行

        /// <summary>
        /// 指定されたメッセージを発行します。
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <returns></returns>
        public Task PublishAsync(TMessage message, CancellationToken cancellationToken)
        {
            var kafkaMessage = new Message<TKey, TMessage>()
            {
                Key = GenerateNewKey(),
                Value = message,
                Timestamp = new Timestamp(DateTimeOffset.UtcNow)
            };

            return Producer.ProduceAsync(Topic, kafkaMessage, cancellationToken)
                .ContinueWith(t => OnPublished(t.Result));
        }

        /// <summary>
        /// メッセージを発行したときの処理を行います。
        /// </summary>
        /// <param name="result">発行の結果</param>
        protected virtual void OnPublished(DeliveryResult<TKey, TMessage> result)
        {
            WriteLog(LogLevel.Debug, () => BuildLogMessage(result));
        }

        /// <summary>
        /// 新しいキーを生成します。
        /// </summary>
        /// <returns>キー</returns>
        private TKey GenerateNewKey()
        {
            return KeyGenerator();
        }

        #endregion

        #region ロギング

        /// <summary>
        /// 指定されたログを出力します。
        /// </summary>
        /// <param name="level">ログレベル</param>
        /// <param name="messageBuilder">ログメッセージを生成するメソッド</param>
        /// <param name="exception">例外</param>
        private void WriteLog(LogLevel level, Func<string> messageBuilder, Exception? exception = null)
        {
            if (!Logger.IsEnabled(level)) { return; }

            if (exception == null)
            {
                Logger.Log(level, messageBuilder());
            }
            else
            {
                Logger.Log(level, exception, messageBuilder());
            }
        }

        private string BuildLogMessage(DeliveryResult<TKey, TMessage> result)
        {
            return $"メッセージを発行しました。[{result.Topic}:{result.Offset}] {result.Message.Value}";
        }

        private string BuildLogMessage(Error error)
        {
            return error.Reason;
        }

        //private string BuildLogMessage(Exception exception)
        //{
        //    return exception.Message;
        //}

        #endregion

    }

}
