﻿using System.Windows;
using System.Windows.Controls;

namespace ToUs.Utilities
{
    class Btn : RadioButton
    {
        static Btn()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Btn), new FrameworkPropertyMetadata(typeof(Btn)));
        }
    }
}
