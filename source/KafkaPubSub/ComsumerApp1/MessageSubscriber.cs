using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Confluent.Kafka;

namespace ComsumerApp1
{

    /// <summary>
    /// Kafka からのメッセージを監視します。
    /// </summary>
    /// <typeparam name="TKey">メッセージのキーの型</typeparam>
    /// <typeparam name="TMessage">メッセージの型</typeparam>
    public class MessageSubscriber<TKey, TMessage> : System.Reactive.ObservableBase<TMessage>
    {
        /// <summary>
        /// インスタンスを生成します。
        /// </summary>
        /// <param name="keyDeserializer">キーに対するデシリアライザ</param>
        /// <param name="messageDeserializer">メッセージに対するデシリアライザ</param>
        /// <param name="subscriberSetting">動作設定</param>
        /// <param name="topic">トピック</param>
        /// <param name="logger">ロガー</param>
        public MessageSubscriber(IDeserializer<TKey> keyDeserializer, IDeserializer<TMessage> messageDeserializer, MessageSubscriberSetting subscriberSetting, string topic, ILogger logger) : base()
        {
            KeyDeserializer = keyDeserializer;
            MessageDeserializer = messageDeserializer;
            SubscriberSetting = subscriberSetting;
            Topic = topic;
            Logger = logger ?? Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
        }

        /// <summary>
        /// ロガーを取得します。
        /// </summary>
        private ILogger Logger { get; }

        /// <summary>
        /// 動作設定を取得します。
        /// </summary>
        private MessageSubscriberSetting SubscriberSetting { get; }
        
        /// <summary>
        /// トピックを取得します。
        /// </summary>
        private string Topic { get; }

        #region 受信

        /// <summary>
        /// キーに対するデシリアライザを取得します。
        /// </summary>
        private IDeserializer<TKey> KeyDeserializer { get; }

        /// <summary>
        /// メッセージに対するデシリアライザを取得します。
        /// </summary>
        private IDeserializer<TMessage> MessageDeserializer { get; }

        /// <summary>
        /// メッセージ受信を開始します。
        /// </summary>
        /// <param name="cancellation">キャンセルトークン</param>
        public Task SubscribeAsync(CancellationToken cancellation)
        {
            Task.Yield();

            TimeSpan interval = SubscriberSetting.ConsumeInterval;

            using var consumer = BuildConsumer(GetConsumerConfig(SubscriberSetting));

            consumer.Subscribe(Topic);

            while (true)
            {
                if (cancellation.IsCancellationRequested) { break; }

                try
                {
                    if (m_Observers.Count == 0) { continue; }

                    ConsumeResult<TKey, TMessage> result = consumer.Consume(interval);

                    if (result == null) { continue; }

                    // TODO: 今回の確認では IsPartitionEOF を発生させることができなかった。
                    if (result.IsPartitionEOF) { continue; }

                    WriteLog(LogLevel.Debug, () => BuildLogMessage(result));
                    NotifyMessage(result.Message.Value);

                    consumer.Commit(result);
                }
                catch (Exception ex)
                {
                    OnException(consumer, ex);
                    break;
                }
            }

            NotifyComplated();

            return Task.CompletedTask;
        }

        /// <summary>
        /// コンシューマーを生成します。
        /// </summary>
        /// <param name="config">動作設定</param>
        /// <returns>コンシューマー</returns>
        protected IConsumer<TKey, TMessage> BuildConsumer(IEnumerable<KeyValuePair<string, string>> config)
        {
            var consumerBuilder = new ConsumerBuilder<TKey, TMessage>(config)
                .SetKeyDeserializer(KeyDeserializer)
                .SetValueDeserializer(MessageDeserializer)
                .SetErrorHandler(OnError)
                .SetLogHandler(OnLogging)
                ;

            return consumerBuilder.Build();
        }

        /// <summary>
        /// コンシューマーの動作設定を取得します。
        /// </summary>
        /// <param name="consumerSetting">コンシューマーの動作設定</param>
        /// <returns>動作設定のキーと値の組み合わせ</returns>
        protected IEnumerable<KeyValuePair<string, string>> GetConsumerConfig(MessageSubscriberSetting consumerSetting)
        {
            if (consumerSetting.BootstrapServers == null || consumerSetting.BootstrapServers == "")
            {
                throw new NullReferenceException("ブートストラップサーバーが設定されていません。");
            }

            if (consumerSetting.ConsumerGroupID == null || consumerSetting.ConsumerGroupID == "")
            {
                throw new NullReferenceException("コンシューマーグループIDが設定されていません。");
            }

            return new ConsumerConfig()
            {
                BootstrapServers = consumerSetting.BootstrapServers,
                GroupId = consumerSetting.ConsumerGroupID,
                EnableAutoCommit = false,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
        }

        /// <summary>
        /// ログを出力します。
        /// </summary>
        /// <param name="consumer"></param>
        /// <param name="log"></param>
        private void OnLogging(IConsumer<TKey, TMessage> consumer, LogMessage log)
        {
            LogLevel logLevel = log.Level.ToLogLevel();
            WriteLog(logLevel, () => BuildLogMessage(log));
        }

        /// <summary>
        /// エラーが発生したときの処理を行います。
        /// </summary>
        /// <param name="consumer"></param>
        /// <param name="error"></param>
        private void OnError(IConsumer<TKey, TMessage> consumer, Error error)
        {
            WriteLog(LogLevel.Error, () => BuildLogMessage(error));
            NotifyError(new Exception(error.Reason));
        }

        /// <summary>
        /// 例外が発生したときの処理を行います。
        /// </summary>
        /// <param name="consumer"></param>
        /// <param name="exception"></param>
        private void OnException(IConsumer<TKey, TMessage> consumer, Exception exception)
        {
            WriteLog(LogLevel.Critical, () => BuildLogMessage(exception), exception);
            NotifyError(exception);
        }

        #endregion

        #region 通知

        /// <summary>
        /// 指定されたメッセージを通知します。
        /// </summary>
        /// <param name="message">メッセージ</param>
        private void NotifyMessage(TMessage message)
        {
            if (m_Observers.Count == 0) { return; }

            lock (m_Observers)
            {
                for (int i = 0; i < m_Observers.Count; ++i)
                {
                    m_Observers[i].OnNext(message);
                }
            }
        }

        /// <summary>
        /// 指定されたメッセージを通知します。
        /// </summary>
        /// <param name="exception">例外</param>
        private void NotifyError(Exception exception)
        {
            if (m_Observers.Count == 0) { return; }

            lock (m_Observers)
            {
                for (int i = 0; i < m_Observers.Count; ++i)
                {
                    m_Observers[i].OnError(exception);
                }
            }
        }

        /// <summary>
        /// 完了を通知します。
        /// </summary>
        private void NotifyComplated()
        {
            if (m_Observers.Count == 0) { return; }

            lock (m_Observers)
            {
                for (int i = 0; i < m_Observers.Count; ++i)
                {
                    m_Observers[i].OnCompleted();
                }
            }
        }

        #endregion

        #region オブザーバー

        /// <summary>
        /// 指定されたオブザーバーによる購読を開始します。
        /// </summary>
        /// <param name="observer"></param>
        /// <returns></returns>
        protected override IDisposable SubscribeCore(IObserver<TMessage> observer)
        {
            AddObserver(observer);
            return System.Reactive.Disposables.Disposable.Create(() => RemoveObserver(observer));
        }

        private readonly List<IObserver<TMessage>> m_Observers = new List<IObserver<TMessage>>();

        /// <summary>
        /// 指定されたオブザーバーを追加します。
        /// </summary>
        /// <param name="observer"></param>
        private void AddObserver(IObserver<TMessage> observer)
        {
            lock (m_Observers)
            {
                m_Observers.Add(observer);
            }
        }

        /// <summary>
        /// 指定されたオブザーバーを削除します。
        /// </summary>
        /// <param name="observer"></param>
        private void RemoveObserver(IObserver<TMessage> observer)
        {
            if (m_Observers.Contains(observer))
            {
                lock (m_Observers)
                {
                    m_Observers.Remove(observer);
                }
            }
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

        private string BuildLogMessage(ConsumeResult<TKey, TMessage> result)
        {
            return $"メッセージを受け取りました。[{result.Topic}:{result.Offset}] {result.Message.Value}";
        }

        private string BuildLogMessage(LogMessage log)
        {
            return log.Message;
        }

        private string BuildLogMessage(Error error)
        {
            return error.Reason;
        }

        private string BuildLogMessage(Exception exception)
        {
            return exception.Message;
        }

        #endregion

    }

}
