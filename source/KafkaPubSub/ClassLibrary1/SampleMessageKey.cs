using System;
using System.Collections.Generic;

namespace ClassLibrary1
{

    /// <summary>
    /// 
    /// </summary>
    public readonly struct SampleMessageKey : IEquatable<SampleMessageKey>
    {
        public SampleMessageKey(string key)
        {
            Key = key;
        }

        public string Key { get; }

        public override bool Equals(object obj)
        {
            return obj is SampleMessageKey key && Equals(key);
        }

        public bool Equals(SampleMessageKey other)
        {
            return Key == other.Key;
        }

        public override int GetHashCode()
        {
            return 990326508 + EqualityComparer<string>.Default.GetHashCode(Key);
        }

        public override string ToString()
        {
            return Key;
        }
    }

}
