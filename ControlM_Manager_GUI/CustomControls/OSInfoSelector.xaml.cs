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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ControlM_Manager_GUI.CustomControls
{
    /// <summary>
    /// Interaction logic for OSInfoSelector.xaml
    /// </summary>
    public partial class OSInfoSelector : UserControl
    {
        public OSInfoSelector()
        {
            InitializeComponent();
        }

        public OSNameList osNameList
        {
            get { return (OSNameList)GetValue(osNameListProperty); }
            set { SetValue(osNameListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for osNameList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty osNameListProperty =
            DependencyProperty.Register("osNameList", typeof(OSNameList), typeof(OSInfoSelector), new PropertyMetadata(new OSNameList()));
    }

}
