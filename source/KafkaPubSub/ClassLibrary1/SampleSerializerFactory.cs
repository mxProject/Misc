using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{

    public class SampleSerializerFactory
    {
        public static SampleSerializer<T> Create<T>()
        {
            return new SampleSerializer<T>();
        }
    }

}
