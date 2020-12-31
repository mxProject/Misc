using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Confluent.Kafka;

namespace ComsumerApp1
{

    /// <summary>
    /// <see cref="SyslogLevel"/> に対する拡張メソッド。
    /// </summary>
    public static class SyslogLevelExtensions
    {

        /// <summary>
        /// <see cref="LogLevel"/> に変換します。
        /// </summary>
        /// <param name="syslogLevel"></param>
        /// <returns></returns>
        public static LogLevel ToLogLevel(this SyslogLevel syslogLevel)
        {
            return syslogLevel switch
            {
                SyslogLevel.Emergency => LogLevel.Critical,
                SyslogLevel.Alert => LogLevel.Critical,
                SyslogLevel.Critical => LogLevel.Critical,
                SyslogLevel.Error => LogLevel.Error,
                SyslogLevel.Warning => LogLevel.Warning,
                SyslogLevel.Notice => LogLevel.Information,
                SyslogLevel.Info => LogLevel.Information,
                SyslogLevel.Debug => LogLevel.Debug,
                _ => LogLevel.None,
            };
        }

    }

}
