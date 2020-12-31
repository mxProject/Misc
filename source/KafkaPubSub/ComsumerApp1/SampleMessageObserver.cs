using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace ComsumerApp1
{

    /// <summary>
    /// メッセージの受信処理。
    /// </summary>
    class SampleMessageObserver : IObserver<ClassLibrary1.SampleMessageBody>
    {
        public void OnCompleted()
        {
            Console.WriteLine($"[{nameof(SampleMessageObserver)}.{nameof(OnCompleted)}]");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine($"[{nameof(SampleMessageObserver)}.{nameof(OnError)}] {error.Message}");
        }

        public void OnNext(SampleMessageBody value)
        {
            Console.WriteLine($"[{nameof(SampleMessageObserver)}.{nameof(OnNext)}] {value}");
        }
    }
}
