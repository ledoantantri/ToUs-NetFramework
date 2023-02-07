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

namespace ToUs.View.StartView
{
    /// <summary>
    /// Interaction logic for StartView.xaml
    /// </summary>
    public partial class StartView : Window
    {
        public StartView()
        {
            InitializeComponent();
            PnlControlBar.Window = this;
        }

        public bool OfficialStartViewIsViewVisible
        {
            get { return (bool)this.GetValue(OfficialStartViewIsViewVisibleProperty); }
            set { this.SetValue(OfficialStartViewIsViewVisibleProperty, value); }
        }

        public static readonly DependencyProperty OfficialStartViewIsViewVisibleProperty = DependencyProperty.Register(
            "OfficialStartViewIsViewVisible", typeof(bool), typeof(StartView), new PropertyMetadata());
    }
}
