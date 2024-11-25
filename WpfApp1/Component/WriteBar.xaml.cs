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

namespace WpfApp1.Component
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class WriteBar : UserControl
    {
        public WriteBar()
        {
            InitializeComponent();
        }

        private void WriteTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (WriteTextBox.Text == "Write something...")
            {
                WriteTextBox.Text = "";
                WriteTextBox.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void WriteTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(WriteTextBox.Text))
            {
                WriteTextBox.Text = "Write something...";
                WriteTextBox.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }

        private void WriteTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
