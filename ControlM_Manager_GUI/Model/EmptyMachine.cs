using ControlM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlM_Manager_GUI.Model
{
    public class EmptyMachine
    {
        public EmptyMachine()
        {

        }
        public string Hostname { get; set; }
        public string Domain { get; set; }
        public string IPv4 { get; set; }
        public string IPv6 { get; set; }
        //public string OSInfo { get; set; }

        public bool CreateReal(out ClientMachine realMachine)
        {
            realMachine = null;
            try
            {
                realMachine = new ClientMachine(Hostname, Domain, IPv4, IPv6, new OSInfo("Windows", "Server 2016", "64 Bit"));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
