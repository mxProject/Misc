using System;
using System.Collections.Generic;
using System.Text;

namespace mxProject.ProcessWorkflow
{
    /// <summary>
    /// 
    /// </summary>
    public readonly struct ProcessCommandID : IEquatable<ProcessCommandID>
    {

        public ProcessCommandID(string id)
        {
            ID = id;
        }

        public string ID { get; }

        public override bool Equals(object obj)
        {
            return obj is ProcessCommandID iD && Equals(iD);
        }

        public bool Equals(ProcessCommandID other)
        {
            return ID == other.ID;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID);
        }

        public override string ToString()
        {
            return ID;
        }

    }

}
