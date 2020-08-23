using ControlM_Manager_GUI.ControlMModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace ControlM_Manager_GUI.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NodeViewerViewClicked(object sender, RoutedEventArgs e)
        {
            Debug.Print("Button Clicked, display NodeViewer");
            var nodeViewer = new NodeViewer(ClientNode.CreateSample());
            nodeViewer.Show();
        }

        private void NodeViewerWriteClicked(object sender, RoutedEventArgs e)
        {
            var nodeViewer = new NodeViewer();
            nodeViewer.Show();
        }
    }
}
