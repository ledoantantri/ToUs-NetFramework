using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ToUs.Resources.CustomControl
{
    /// <summary>
    /// Interaction logic for NotificationExitApp.xaml
    /// </summary>
    public partial class NotificationExitApp : UserControl
    {
        public new bool IsVisible
        {
            get { return (bool)GetValue(IsVisibleProperty); }
            set { SetValue(IsVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsVisible. This enables animation,
        // styling, binding, etc...
        public new static readonly DependencyProperty IsVisibleProperty =
            DependencyProperty.Register("IsVisible", typeof(bool), typeof(NotificationExitApp), new PropertyMetadata(false));

        //Command property

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(NotificationExitApp));

        public NotificationExitApp()
        {
            InitializeComponent();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}