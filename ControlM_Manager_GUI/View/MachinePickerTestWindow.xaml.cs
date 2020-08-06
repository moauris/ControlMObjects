using System;
using System.Collections.Generic;
using System.Linq;
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
using ControlM;
using CtlmDBDriver_Access;

namespace ControlM_Manager_GUI.View
{
    /// <summary>
    /// Interaction logic for MachinePickerTestWindow.xaml
    /// </summary>
    public partial class MachinePickerTestWindow : Window
    {
        public MachinePickerTestWindow()
        {
            InitializeComponent();
        }

        private void OnPickMachineClicked(object sender, RoutedEventArgs e)
        {
            MachinePicker machinePicker = new MachinePicker();
            machinePicker.Show();
        }

        private void OnWriteNewMachine(object sender, RoutedEventArgs e)
        {
            MachineWriter machineWriter = new MachineWriter();
            machineWriter.Show();
        }
    }
}
