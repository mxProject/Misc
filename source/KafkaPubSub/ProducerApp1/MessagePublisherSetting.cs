using System;
using System.Collections.Generic;
using System.Text;

namespace ProducerApp1
{

    /// <summary>
    /// パブリッシャーの動作設定。
    /// </summary>
    public class MessagePublisherSetting
    {

        /// <summary>
        /// Kafka のブートストラップサーバーを取得または設定します。
        /// </summary>
        public string? BootstrapServers { get; set; }

    }

}
