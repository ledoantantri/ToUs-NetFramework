using System.Windows;

namespace ToUs.View.AuthenticateView
{
    /// <summary>
    /// Interaction logic for AuthenticateView.xaml
    /// </summary>
    public partial class AuthenticateView : Window
    {
        public AuthenticateView()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            PnlControlBar.Window = this;
        }
    }
}
