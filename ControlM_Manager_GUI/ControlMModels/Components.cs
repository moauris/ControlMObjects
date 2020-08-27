using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlM_Manager_GUI.ControlMModels
{
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

    public class ControlMVersion
    {
        public static ControlMVersion V7000()
        {
            return new ControlMVersion(7, 0, 0, "FixPack5");
        }
        public static ControlMVersion V9018()
        {
            return new ControlMVersion(9, 0, 18, "-");
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


    }
}
