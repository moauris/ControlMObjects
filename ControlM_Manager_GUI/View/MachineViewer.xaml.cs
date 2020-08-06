using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ControlM;

namespace ControlM_Manager_GUI.View
{
    /// <summary>
    /// Interaction logic for MachineViewer.xaml
    /// </summary>
    public partial class MachineViewer : Window
    {
        public MachineViewer()
        {
            InitializeComponent();
            DisplayedMachine = ClientMachine.CreateSample(); // for debug
            DataContext = DisplayedMachine;
        }
        public MachineViewer(ClientMachine machine) : base()
        {
            DisplayedMachine = machine;
        }

        public ClientMachine DisplayedMachine
        {
            get { return (ClientMachine)GetValue(DisplayedMachineProperty); }
            set { SetValue(DisplayedMachineProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisplayedMachine.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayedMachineProperty =
            DependencyProperty.Register("DisplayedMachine", typeof(ClientMachine), typeof(MachineViewer), new PropertyMetadata(null));

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
