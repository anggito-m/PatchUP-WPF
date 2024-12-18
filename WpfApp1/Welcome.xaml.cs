﻿using System;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Welcome : Page
    {
        public Welcome()
        {
            InitializeComponent();          
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame frame = new Frame();
            frame.Navigate(new Login());
            this.Content = frame;
        }

        private void Button_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Frame frame = new Frame();
                frame.Navigate(new Login());
                this.Content = frame;
            }

        }
    }
}
