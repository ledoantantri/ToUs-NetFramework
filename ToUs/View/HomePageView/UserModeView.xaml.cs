using System;
using System.Windows.Controls;

namespace ToUs.View.HomePageView
{
    /// <summary>
    /// Interaction logic for UserModeView.xaml
    /// </summary>
    public partial class UserModeView : UserControl
    {
        public UserModeView()
        {
            InitializeComponent();
            TextDateTime.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");

        }
    }
}