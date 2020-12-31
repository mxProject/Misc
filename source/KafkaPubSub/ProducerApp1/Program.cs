using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Confluent.Kafka;
using ClassLibrary1;

namespace ProducerApp1
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

        /// <summary>
        /// 処理を実行します。
        /// </summary>
        /// <returns></returns>
        static async Task RunAsync()
        {
            int process = System.Diagnostics.Process.GetCurrentProcess().Id;
            Console.WriteLine($"パブリッシャーをプロセス {process} で起動しました。");

            // コンソールからパラメーターを受け取る
            Console.WriteLine("bootstrap servers を入力してください（省略時 127.0.0.1）：");
            var bootstrapServers = Console.ReadLine();
            if (string.IsNullOrEmpty(bootstrapServers)) { bootstrapServers = "127.0.0.1"; }

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
            var publisherSetting = new MessagePublisherSetting()
            {
                BootstrapServers = bootstrapServers
            };

            // キーを生成するメソッド
            static SampleMessageKey GenerateKey()
            {
                return new SampleMessageKey(Guid.NewGuid().ToString());
            }

            // パブリッシャーを生成する
            var factory = new SampleMessagePublisherFactory(publisherSetting, new SampleLogger());

            using IMessagePublisher<SampleMessageBody> publisher
                = factory.CreatePublisher<SampleMessageKey, SampleMessageBody>(topic, GenerateKey);

            Console.WriteLine("メッセージの送信処理を開始します。終了するには Ctrl+C を押してください。");

            // 一定間隔でメッセージを発行する
            int sequence = 0;
            TimeSpan interval = TimeSpan.FromSeconds(5);

            while (true)
            {
                if (cancelTokenSource.Token.IsCancellationRequested) { break; }

                ++sequence;

                await publisher.PublishAsync(
                    new SampleMessageBody(DateTimeOffset.UtcNow, $"{sequence}回目のメッセージ（プロセス{process}）")
                    , cancelTokenSource.Token
                    ).ConfigureAwait(false);

                await Task.Delay(interval, cancelTokenSource.Token);
            }

            publisher.Dispose();

            Console.WriteLine("メッセージの送信処理を終了しました。");

        }

    }
}
