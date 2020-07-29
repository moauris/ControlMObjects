using System;
using System.Collections.Generic;
using System.Text;

namespace ControlM
{
    public sealed class Agent : Components
    {
        public Agent(ControlMVersion version, ClientNode node, Server connectedMServer, int msmaPort) : base(version, node)
        {
            ConnectedMServer = connectedMServer;
            MSMAPort = msmaPort;
        }

        #region Properties
        private Server connectedMServer;

        public Server ConnectedMServer
        {
            get { return connectedMServer; }
            set { connectedMServer = value; }
        }

        private int msmaPort;

        public int MSMAPort
        {
            get { return msmaPort; }
            set { msmaPort = value; }
        }

        #endregion

        #region Methods
        public override string ToString()
        {
            //Agent name concatenation example version 7: [aur00101(uny30110):27006]
            //Agent name concatenation example version 9: [aur00101.sony.co.jp(uny30110):27006]
            if (Version.Major <= 7) 
                return string.Format("[{0}({1}):{2}]", Node.Machine.Hostname, ConnectedMServer.Node.Machine.Hostname, MSMAPort);
            return string.Format("[{0}({1}):{2}]", Node.Machine.FQDN, ConnectedMServer.Node.Machine.Hostname, MSMAPort);
        }
        #endregion
    }
}
