using ControlM;
using CtlmDBDriver_Access;
using System;
using System.Collections.Generic;
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
using System.Diagnostics;
using ControlM_Manager_GUI.Model;

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
            //Binding Events to Raise Validation Events
            //Init with no items being valid
            FormContentValidated += ValidateForm;

        }

        private void ValidateForm(object sender, EventArgs e)
        {
            var formValidEventArgs = e as FormContentValidateEventArgs;
            Debug.Print($"The form validation status is {(int)formValidEventArgs.Item}");
            if (formValidEventArgs.Item == ItemValidStatus.All)
            {
                btnConfirm.IsEnabled = true;
            }
        }

        public ItemValidStatus ValidItems { get; set; } = ItemValidStatus.None;

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

        /// <summary>
        /// Fires when the content is validated complete.
        /// That is, no error on textboxes, OSInfo filled.
        /// </summary>
        private event EventHandler FormContentValidated;
        protected virtual void OnFormContentValidated(ItemValidStatus ValidItem)
        {
            FormContentValidated?.Invoke(this, new FormContentValidateEventArgs(ValidItem));
        }

        private void OnConfirmClicked(object sender, RoutedEventArgs e)
        {
        }

        private void OnClearClicked(object sender, RoutedEventArgs e)
        {

        }

        private void OnTextboxValidated(object sender, EventArgs e)
        {
            MachinViewerTxbRule SenderRule = sender as MachinViewerTxbRule;
            switch (SenderRule.Tag)
            {
                case "Hostname":
                    ValidItems |= ItemValidStatus.Hostname;
                    OnFormContentValidated(ValidItems);
                    break;
                case "Domain":
                    ValidItems |= ItemValidStatus.Domain;
                    OnFormContentValidated(ValidItems);
                    break;
                default:
                    break;
            }
        }

        private void OnTextboxValidationLost(object sender, EventArgs e)
        {
            MachinViewerTxbRule SenderRule = sender as MachinViewerTxbRule;
            switch (SenderRule.Tag)
            {
                //Only set ValidItem digit to 0 when both are 1.
                case "Hostname":
                    ValidItems = ~ItemValidStatus.Hostname & ValidItems; 
                    OnFormContentValidated(ValidItems);
                    break;
                case "Domain":
                    ValidItems = ~ItemValidStatus.Domain & ValidItems;
                    OnFormContentValidated(ValidItems);
                    break;
                default:
                    break;
            }
        }
    }

    class FormContentValidateEventArgs : EventArgs
    {
        public FormContentValidateEventArgs(ItemValidStatus ValidItem)
        {
            Item = ValidItem;
        }
        public ItemValidStatus Item { get; set; }
    }

    [Flags]
    public enum ItemValidStatus
    {
        None = 0, Hostname = 1, Domain = 2, Ipv4 = 4, Ipv6 = 8, OSInfo = 16,
        All = Hostname | Domain | Ipv4 | Ipv6 | OSInfo
    }
}
