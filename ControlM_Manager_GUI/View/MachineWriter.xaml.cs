using ControlM;
using CtlmDBDriver_Access;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ControlM_Manager_GUI.View
{
    /// <summary>
    /// Interaction logic for MachineWriter.xaml
    /// </summary>
    public partial class MachineWriter : Window
    {
        public MachineWriter()
        {
            InitializeComponent();
        }

        /*
        private void OnValidateClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                EnteredMachine = new ClientMachine
                (Hostname, Domain, IPv4, IPv6, new OSInfo("Linux", "7.2", "64-bit"));
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }
            btnUpload.IsEnabled = true;
        }
        */

        private void OnUploadClicked(object sender, RoutedEventArgs e)
        {
            DatabaseWriter.WriteMachineInfo(EnteredMachine);

            btnUpload.IsEnabled = false;
        }

        private void OnCancelClicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        public ClientMachine EnteredMachine { get; set; }

        public string Hostname
        {
            get { return (string)GetValue(HostnameProperty); }
            set { SetValue(HostnameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Hostname.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HostnameProperty =
            DependencyProperty.Register("Hostname", typeof(string), typeof(MachineWriter), new PropertyMetadata(string.Empty));



        public string Domain
        {
            get { return (string)GetValue(DomainProperty); }
            set { SetValue(DomainProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Domain.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DomainProperty =
            DependencyProperty.Register("Domain", typeof(string), typeof(MachineWriter), new PropertyMetadata(string.Empty));



        public string IPv4
        {
            get { return (string)GetValue(IPv4Property); }
            set { SetValue(IPv4Property, value); }
        }

        // Using a DependencyProperty as the backing store for IPv4.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IPv4Property =
            DependencyProperty.Register("IPv4", typeof(string), typeof(MachineWriter), new PropertyMetadata(string.Empty));

        public string IPv6
        {
            get { return (string)GetValue(IPv6Property); }
            set { SetValue(IPv6Property, value); }
        }

        // Using a DependencyProperty as the backing store for IPv4.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IPv6Property =
            DependencyProperty.Register("IPv6", typeof(string), typeof(MachineWriter), new PropertyMetadata(string.Empty));

        private void ValidateTextBox(MachineParm errParm, MachineParm compareParm, TextBox txb)
        {
            Debug.Print($"Checking Parameter Enum: {compareParm} value is {errParm.ToString()}");
            Debug.Print($"Is error? : {(errParm & compareParm) == compareParm}");
            bool TestResult = (errParm & compareParm) == compareParm;
            Debug.Print($"Is error? : {TestResult}");
            if (TestResult)
            {
                Debug.Print("Setting to Invalid style");
                txb.Style = (Style)Resources["TextBoxStyle_Invalid"];
            }
            else
            {
                Debug.Print("Setting to normal style");
                txb.Style = (Style)Resources["MonoFont"];
            }
        }
        private void OnValidateTxb(object sender, RoutedEventArgs e)
        {
            TextBox txb = sender as TextBox;


            MachineParm errParm = ClientMachine.ValidateParms(Hostname, Domain, IPv4, IPv6, new OSInfo("AIX", "7,1", "64-bit"));
            switch (txb.Name)
            {
                case "tbxHostname":
                    ValidateTextBox(errParm, MachineParm.Hostname, txb);
                    break;
                case "tbxDomain":
                    ValidateTextBox(errParm, MachineParm.Domain, txb);
                    break;
                case "tbxIPv4":
                    ValidateTextBox(errParm, MachineParm.Ipv4, txb);
                    break;
                case "tbxIPv6":
                    ValidateTextBox(errParm, MachineParm.Ipv6, txb);
                    break;
                default:
                    break;
            }

            btnUpload.IsEnabled = true;
        }
    }
}
