using System;
using System.Collections.Generic;
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
    /// Interaction logic for NodeViewer.xaml
    /// </summary>
    public partial class NodeViewer : Window
    {
        public NodeViewer()
        {
            InitializeComponent();
        }

        private void ButtonPrintClicked(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(this, "Printing Node Viewer");
            }
        }
    }
}
