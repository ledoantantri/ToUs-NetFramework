using System.Windows;
using System.Windows.Controls;

namespace ToUs.Resources.CustomControl
{
    /// <summary>
    /// Interaction logic for BoxLoading.xaml
    /// </summary>
    /// 



    public partial class BoxLoading : UserControl
    {

        public new bool IsVisible
        {
            get { return (bool)GetValue(IsVisibleProperty); }
            set { SetValue(IsVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsVisible. This enables animation,
        // styling, binding, etc...
        public new static readonly DependencyProperty IsVisibleProperty =
            DependencyProperty.Register("IsVisible", typeof(bool), typeof(BoxLoading), new PropertyMetadata(true));

        public BoxLoading()
        {
            InitializeComponent();
        }
    }
}
