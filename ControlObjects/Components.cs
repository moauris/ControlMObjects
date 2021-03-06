﻿using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace ControlM
{
    /// <summary>
    /// A Control Object refers to one of the three types of BMC Control-M for Windows and Unix products:
    /// 1. Control-M Enterprise Manager
    /// 2. Control-M Server
    /// 3. Control-M Agent
    /// </summary>

    [Serializable]
    public abstract class Components
    {
        public Components(ControlMVersion version, ClientNode node)
        {
            Version = version;
            Node = node;
        }
        public ControlMVersion Version { get; set; }
        public ClientNode Node { get; set; }
    }

    public abstract class ClientNode
    {

        private ClientMachine machine;

        public ClientMachine Machine
        {
            get { return machine; }
            set { machine = value; }
        }

        public abstract bool IsCluster();
        public string ToHostEntry(string Comment)
        {
            StringBuilder sbHostEntry = new StringBuilder();
            sbHostEntry.Append(string.Format("{0,-46}{1,-30}# {2}", Machine.IPv4, Machine.FQDN, Comment));
            if (machine.IPv6 != null)
            {
                sbHostEntry.AppendLine();
                sbHostEntry.Append(string.Format("{0,-46}{1,-30}# {2}", Machine.IPv6, Machine.FQDN, Comment));
            }
                
            return sbHostEntry.ToString();
        }
    }

    public class ClientNode_Standalone : ClientNode
    {
        public ClientNode_Standalone(ClientMachine client)
        {
            if (client == null)
            {
                throw new Exception("Client Object cannot be null.");
            }
            Machine = client;
        }
        public override bool IsCluster() => false;
        /// <summary>
        /// Generate a host entry line
        /// </summary>
        /// <returns>a host entry line</returns>
        
    }
    public class ClientNode_Cluster : ClientNode
    {
        public ClientNode_Cluster(ClientMachine vip, ClientMachine pri, ClientMachine sec)
        {
            if ((vip == null) || (pri == null) || (sec == null))
            {
                throw new Exception("Client Objects cannot be null.");
            }
            Machine = vip;
            NodeVIP = vip;
            NodePrimary = pri;
            NodeSecondary = sec;
        }
        private ClientMachine nodeVIP;

        public ClientMachine NodeVIP
        {
            get { return nodeVIP; }
            set 
            {
                nodeVIP = value;
            }
        }
        private ClientMachine nodePrimary;

        public ClientMachine NodePrimary
        {
            get { return nodePrimary; }
            set { nodePrimary = value; }
        }

        private ClientMachine nodeSecondary;

        public ClientMachine NodeSecondary
        {
            get { return nodeSecondary; }
            set { nodeSecondary = value; }
        }

        public override bool IsCluster() => true;

    }
    [Serializable]
    public class ClientMachine
    {
        
        public ClientMachine(string hostname, IPAddress ipv4, OSInfo oSInfo)
        {
            Hostname = hostname;
            IPv4 = ipv4;
            OSInformation = oSInfo;
        }
        public ClientMachine(string hostname, string ipv4, OSInfo oSInfo) : this(hostname, IPAddress.Parse(ipv4), oSInfo) { }
        
        public ClientMachine(string hostname, string domain, IPAddress ipv4, IPAddress ipv6, OSInfo oSInfo) : this(hostname, ipv4, oSInfo)
        {
            Domain = domain;
            IPv6 = ipv6;
        }

        public ClientMachine(string hostname, string domain, string ipv4, string ipv6, OSInfo oSInfo) :
            this(hostname, domain, IPAddress.Parse(ipv4), IPAddress.Parse(ipv6), oSInfo) { }

        private OSInfo oSInfo;

        public OSInfo OSInformation
        {
            get { return oSInfo; }
            set { oSInfo = value; }
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


        private string hostname;

        public string Hostname
        {
            get { return hostname; }
            set
            {
                Regex UnqualifyMatch = new Regex("[^A-Za-z0-9]");
                if (UnqualifyMatch.IsMatch(value))
                {
                    throw new Exception("hostname contains invalid character i.e. $%^#@_");
                }
                hostname = value;
            }
        }

        private string domain;
        public string Domain
        {
            get { return domain; }
            set
            {
                Regex QualifiedMatch = new Regex(@"^[\.]([A-Za-z0-9]+[\.])+[A-Za-z0-9]+$");
                if (!QualifiedMatch.IsMatch(value))
                {
                    throw new Exception("Domain format error. Either it contains illegal characters or '.' is misused.");
                }
                domain = value;
            }
        }

        public string FQDN
        {
            get { return hostname + Domain; }
        }

        #region Methods
        public static ClientMachine CreateSample()
        {
            return new ClientMachine("MO78A121"
                , ".cn.ibm.com"
                , "9.200.73.26"
                , "2001:0:2851:b9f0:340c:3a0f:95d9:ff83"
                , new OSInfo("Windows","10 Enterprise","64-bit"));
        }
        public ClientNode_Standalone ConvertToNode()
        {
            return new ClientNode_Standalone(this);
        }
        public ClientNode_Cluster ConvertToCluster(ClientMachine primary, ClientMachine secondary)
        {
            return new ClientNode_Cluster(this, primary, secondary);
        }
        #endregion

    }
    public class ControlMVersion
    {
        public static ControlMVersion V7000()
        {
            return new ControlMVersion(7,0,0,"FixPack5");
        }
        public static ControlMVersion V9018()
        {
            return new ControlMVersion(9,0,18,"-");
        }

        public ControlMVersion(int major, int minor, int build, string patch)
        {
            
            Major = major;
            Minor = minor;
            Build = build;
            Patch = patch;
                    
        }
        private int major;

        public int Major
        {
            get { return major; }
            set
            {
                if (value > 100)
                {
                    throw new Exception("Major version number must be only two digit");
                }
                major = value;
            }
        }
        private int minor;

        public int Minor
        {
            get { return minor; }
            set
            {
                if (value > 10)
                {
                    throw new Exception("Minor version number must be only one digit");
                }
                minor = value;
            }
        }
        private int build;

        public int Build
        {
            get { return build; }
            set
            {
                if (value > 100)
                {
                    throw new Exception("Build number must be only two digit");
                }
                build = value;
            }
        }

        private string patch;
        public string Patch
        {
            get
            {
                return " " + patch;
            }
            set
            {
                patch = value.TrimStart();
            }
        }


        public override string ToString()
        {
            return String.Format("{0}.{1}.{2} {3}", Major, Minor, Build.ToString().PadLeft(2,'0'), Patch);
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
