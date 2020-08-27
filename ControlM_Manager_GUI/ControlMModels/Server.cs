using System;
using System.Collections.Generic;
using System.Text;

namespace ControlM_Manager_GUI.ControlMModels
{
    public sealed class Server : Components
    {
        public Server(ControlMVersion version, ClientNode node) : base(version, node)
        { }

        public int msmaPort { get; set; } = 7005;



        #region Properties Definitions
            
        #endregion
    }
}
