using System;
using System.Collections.Generic;
using System.Text;

namespace ComsumerApp1
{

    /// <summary>
    /// サブスクライバーの動作設定。
    /// </summary>
    public class MessageSubscriberSetting
    {

        /// <summary>
        /// Kafka のブートストラップサーバーを取得または設定します。
        /// </summary>
        public string? BootstrapServers { get; set; }

        /// <summary>
        /// コンシューマーグループIDを取得または設定します。
        /// </summary>
        public string? ConsumerGroupID { get; set; }

        /// <summary>
        /// 受信インターバルを取得または設定します。
        /// </summary>
        public TimeSpan ConsumeInterval { get; set; } = TimeSpan.FromMilliseconds(100);

    }

}
