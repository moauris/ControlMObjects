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



        public string OSName
        {
            get { return (string)GetValue(OSNameProperty); }
            set
            {
                if (value != string.Empty)
                {
                    OnPropertyChanged("OSName");
                    SetValue(OSNameProperty, value);
                }
            }
        }

        // Using a DependencyProperty as the backing store for OSName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OSNameProperty =
            DependencyProperty.Register("OSName", typeof(string), typeof(OSInfoSelector), new PropertyMetadata(string.Empty));



        public string OSVersion
        {
            get { return (string)GetValue(OSVersionProperty); }
            set
            {
                if (value != string.Empty)
                {
                    OnPropertyChanged("OSVersion");
                    SetValue(OSVersionProperty, value);
                }
            }
        }

        // Using a DependencyProperty as the backing store for OSVersion.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OSVersionProperty =
            DependencyProperty.Register("OSVersion", typeof(string), typeof(OSInfoSelector), new PropertyMetadata(string.Empty));



        public string OSArchitecture
        {
            get { return (string)GetValue(OSArchitectureProperty); }
            set 
            {
                if (value != string.Empty)
                {
                    OnPropertyChanged("OSArchitecture");
                    SetValue(OSArchitectureProperty, value);
                }
            }
        }

        // Using a DependencyProperty as the backing store for OSArchitecture.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OSArchitectureProperty =
            DependencyProperty.Register("OSArchitecture", typeof(string), typeof(OSInfoSelector), new PropertyMetadata(string.Empty));

        public event EventHandler<PropertyChangedEventArgs> PropertyChangedEventHandler;
        protected virtual void OnPropertyChanged(string PropertyName)
        {
            PropertyChangedEventHandler.Invoke(this
                , new PropertyChangedEventArgs(PropertyName));
        }
    }

    public class PropertyChangedEventArgs : EventArgs
    {
        public PropertyChangedEventArgs(string propertyName)
        {
            PropertyName = propertyName;
        }
        public string PropertyName { get; set; }
    }
}
