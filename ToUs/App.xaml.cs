using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ToUs.View.SignInView;

namespace ToUs
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            var signinview =  new SignInView();
            signinview.Show();
            signinview.IsVisibleChanged += (s, ev) =>
            {
                if (signinview.IsVisible == false && signinview.IsLoaded)
                {
                    var mainView = new View.MainView();
                    mainView.Show();
                    signinview.Close();
                }
            };
        }
    }
}
