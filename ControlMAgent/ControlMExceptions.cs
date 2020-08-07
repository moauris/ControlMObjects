using System;
using System.Collections.Generic;
using System.Text;

namespace ControlM
{

    [Serializable]
    public class MachineInitException : Exception
    {
        public MachineInitException() { }
        public MachineInitException(string message, MachineParm erredParm) : base(message)
        {
            ErredParm = erredParm;
        }
        public MachineInitException(string message, Exception inner, MachineParm erredParm) : base(message, inner) 
        { 
            ErredParm = erredParm; 
        }
        protected MachineInitException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        private MachineParm erredParm = MachineParm.None;
        public MachineParm ErredParm
        {
            get { return erredParm; }
            set 
            { 
                erredParm |= value; // erredParm = erredParm | value;
            }
        }
    }

    [Flags]
    public enum MachineParm
    {
        None = 0,
        Hostname = 1,
        Domain = 2,
        Ipv4 = 4,
        Ipv6 = 8,
        OSInfo = 16
    }
}
