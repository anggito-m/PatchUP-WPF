using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();
            HomeWin.Navigate(new Home());
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the window position to the top-left corner
            this.Left = 0; // Set the horizontal position
            this.Top = 0;  // Set the vertical position
        }
    }
}