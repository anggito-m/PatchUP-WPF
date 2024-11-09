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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Help.xaml
    /// </summary>
    public partial class Help : Page
    {
        public Help()
        {
            InitializeComponent();
            try
            {
                sidebar.NavigateToPage += Sidebar_NavigateToPage;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during initialization: {ex.Message}", "Initialization Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Sidebar_NavigateToPage(object sender, string pageName)
        {
            Frame frame = new Frame();
            frame.Navigate(sidebar.Navigate(pageName));
            this.Content = frame;
        }
    }
}
