using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Confluent.Kafka;
using ClassLibrary1;

namespace ComsumerApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                await RunAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.ReadKey();
        }

        static async Task RunAsync()
        {
            int process = System.Diagnostics.Process.GetCurrentProcess().Id;
            Console.WriteLine($"サブスクライバーをプロセス {process} で起動しました。");

            // コンソールからパラメーターを受け取る
            Console.WriteLine("bootstrap servers を入力してください（省略時 127.0.0.1）：");
            var bootstrapServers = Console.ReadLine();
            if (string.IsNullOrEmpty(bootstrapServers)) { bootstrapServers = "127.0.0.1"; }

            Console.WriteLine($"コンシューマーグループIDを入力してください（省略時 {Constants.DefaultComsumerGroupID}）：");
            var groupID = Console.ReadLine();
            if (string.IsNullOrEmpty(groupID)) { groupID = Constants.DefaultComsumerGroupID; }

            Console.WriteLine($"トピックを入力してください（省略時 {Constants.DefaultTopic}）：");
            var topic = Console.ReadLine();
            if (string.IsNullOrEmpty(topic)) { topic = Constants.DefaultTopic; }

            // キャンセルトークンを生成する
            using var cancelTokenSource = new CancellationTokenSource();

            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cancelTokenSource.Cancel();
            };

            // 動作設定を生成する
            var subscriberSetting = new MessageSubscriberSetting()
            {
                BootstrapServers = bootstrapServers,
                ConsumerGroupID = groupID
            };

            // observable パターンでメッセージを監視する
            var factory = new SampleMessageSubscriberFactory(subscriberSetting, new SampleLogger());

            Console.WriteLine($"メッセージの受信処理を開始します。終了するには Ctrl+C を押してください。");

            var subscriber = factory.CreateSubscriber<SampleMessageKey, SampleMessageBody>(topic);

            using var releaser = subscriber.Subscribe(new SampleMessageObserver());

            await subscriber.SubscribeAsync(cancelTokenSource.Token).ConfigureAwait(false);

            Console.WriteLine("メッセージの受信処理を終了しました。");

        }
    }
}
