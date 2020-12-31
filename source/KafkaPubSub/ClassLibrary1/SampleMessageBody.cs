using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{

    /// <summary>
    /// 
    /// </summary>
    public class SampleMessageBody
    {

        public SampleMessageBody(DateTimeOffset time, string message)
        {
            Time = time;
            Message = message;
        }

        public DateTimeOffset Time { get; }

        public string Message { get; }

        public override string ToString()
        {
            return Message;
        }
    }

}
