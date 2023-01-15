﻿//using Microsoft.Office.Interop.Excel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ToUs.Resources.CustomControl
{
    /// <summary>
    /// Interaction logic for SearchBar.xaml
    /// </summary>
    public partial class SearchBar : UserControl
    {
        //Text input:


        public string TextInput
        {
            get { return (string)GetValue(TextInputProperty); }
            set { SetValue(TextInputProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextInput.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextInputProperty =
            DependencyProperty.Register("TextInput", typeof(string), typeof(SearchBar));



        //Width searchbar
        public double MyWidth
        {
            get { return (double)GetValue(MyWidthProperty); }
            set { SetValue(MyWidthProperty, value); }
        }

        public static readonly DependencyProperty MyWidthProperty =
            DependencyProperty.Register("MyWidth", typeof(double), typeof(SearchBar));

        //Text placeholder


        public string TextBehind
        {
            get { return (string)GetValue(TextBehindProperty); }
            set { SetValue(TextBehindProperty, value); }
        }

        public static readonly DependencyProperty TextBehindProperty =
            DependencyProperty.Register("TextBehind", typeof(string), typeof(SearchBar), new PropertyMetadata(null));

        //Padding



        public Thickness MyPadding
        {
            get { return (Thickness)GetValue(MyPaddingProperty); }
            set { SetValue(MyPaddingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyPadding.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyPaddingProperty =
            DependencyProperty.Register("MyPadding", typeof(Thickness), typeof(SearchBar));






        public SearchBar()
        {
            InitializeComponent();
        }

        private void TextBlockFilter_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBoxFilter.Focus();
        }

        private void TextBoxFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBoxFilter.Text) && TextBoxFilter.Text.Length > 0)
            {
                TextBlockFilter.Visibility = Visibility.Collapsed;
            }
            else
            {
                TextBlockFilter.Visibility = Visibility.Visible;
            }
        }
    }
}
