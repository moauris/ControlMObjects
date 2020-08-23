using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlM_Manager_GUI.ControlMModels
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
        #endregion
    }
}
