//using ControlM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace ControlM_Manager_GUI.ControlMModels
{
    public class ClientNode
    {
        private ObservableCollection<HostEntry> hostEntries;

        public ObservableCollection<HostEntry> HostEntries
        {
            get { return hostEntries; }
            set 
            {
                if (HostEntries.Count > 3)
                {
                    throw new Exception("Node Cluster cannot hold more than 3 Nodes.");
                }
                hostEntries = value; 
            }
        }

        private OSInfo osInformation;

        public OSInfo OSInformation
        {
            get { return osInformation; }
            set { osInformation = value; }
        }


        public static ClientNode CreateSample()
        {
            return new ClientNode
            {
                HostEntries = { new HostEntry("uiirrii21", ".chenmo.com.cn", "192.100.38.4", "6fd0:38d7:eb64:c34b:9466:20d8:1fc4:e9f6") },
                OSInformation = new OSInfo("Microsoft Windows", "10", "64 bit")
            };
        }

    }

    public class HostEntry
    {
        public HostEntry(string hostname, IPAddress ipv4)
        {
            HostName = hostname;
            IPv4 = ipv4;
        }

        public HostEntry(string hostname, string ipv4)
            : this(hostname, IPAddress.Parse(ipv4)) { }



        public HostEntry(string hostname, string domain, IPAddress ipv4, IPAddress ipv6) 
            : this(hostname, ipv4)
        {
            Domain = domain;
            IPv6 = ipv6;
        }
        public HostEntry(string hostname, string domain, string ipv4, string ipv6)
            : this(hostname, domain, IPAddress.Parse(ipv4), IPAddress.Parse(ipv6)) { }



        private string hostname;

        public string HostName
        {
            get { return hostname; }
            set { hostname = value; }
        }

        private string domain;

        public string Domain
        {
            get { return domain; }
            set { domain = value; }
        }


        private IPAddress ipv4;

        public IPAddress IPv4
        {
            get { return ipv4; }
            set
            {
                if (value.GetAddressBytes().Length != 4)
                {
                    throw new Exception("Not a valid IPv4 Address!");
                }

                ipv4 = value;
            }
        }

        private IPAddress ipv6;

        public IPAddress IPv6
        {
            get { return ipv6; }
            set
            {
                if (value.GetAddressBytes().Length != 16)
                {
                    throw new Exception("Not a valid IPv6 Address!");
                }

                ipv6 = value;
            }
        }

        

        public string ToHostEntry(string Comment)
        {
            StringBuilder sbHostEntry = new StringBuilder();
            string FQDN = this.HostName + this.Domain;
            sbHostEntry.Append(string.Format("{0,-46}{1,-30}# {2}", IPv4, FQDN, Comment));
            if (IPv6 != null)
            {
                sbHostEntry.AppendLine();
                sbHostEntry.Append(string.Format("{0,-46}{1,-30}# {2}", IPv6, FQDN, Comment));
            }

            return sbHostEntry.ToString();
        }

    }

    public class OSInfo
    {
        public OSInfo(string osname, string osverion, string osarchitecture)
        {
            OSName = osname;
            OSVersion = osverion;
            OSArchitecture = osarchitecture;
        }
        public string OSName { get; set; }
        public string OSVersion { get; set; }
        public string OSArchitecture { get; set; }
        public override string ToString()
        {
            return string.Format("{0} {1} {2}", OSName, OSVersion, OSArchitecture);
        }

    }
}
