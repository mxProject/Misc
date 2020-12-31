using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{

    /// <summary>
    /// サンプルロガー
    /// </summary>
    public class SampleLogger : Microsoft.Extensions.Logging.ILogger
    {

        /// <inheritdoc/>
        public IDisposable BeginScope<TState>(TState state)
        {
            Log(LogLevel.Information, 0, state, null!, (state, _) => $"BeginScope: {state}");

            return new LogScope(() =>
            {
                Log(LogLevel.Information, 0, state, null!, (state, _) => $"EndScope: {state}");
            }
            );
        }

        /// <inheritdoc/>
        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        /// <inheritdoc/>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} [{logLevel}] {formatter(state, exception)}");
        }

        /// <summary>
        /// ログスコープの状態を管理するオブジェクト。
        /// </summary>
        private readonly struct LogScope : IDisposable
        {
            internal LogScope(Action onDispose)
            {
                m_OnDispose = onDispose;
            }

            private readonly Action m_OnDispose;

            public void Dispose()
            {
                m_OnDispose();
            }
        }
    }
}
