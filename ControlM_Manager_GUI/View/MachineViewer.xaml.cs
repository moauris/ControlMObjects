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
    public partial class MachineViewer : Window, INotifyPropertyChanged
    {
        public MachineViewer()
        {
            InitializeComponent();
            Machine = ClientMachine.CreateSample();
        }
        public MachineViewer(ClientMachine machine) : base()
        {
            Machine = machine;
        }
        private ClientMachine machine;

        public ClientMachine Machine
        {
            get { return machine; }
            set
            {
                machine = value;
                OnPropertyChanged("Machine");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
