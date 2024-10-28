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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
            UsernameBinding.Text = Model.user.Instance.Name;
            ProfileName.Text = Model.user.Instance.Name;
        }

        private void TutorialClick(object sender, RoutedEventArgs e)
        {
            Frame frame = new Frame();
            frame.Navigate(new Tutorial());
            this.Content = frame;

        }

        private void HomeClick(object sender, RoutedEventArgs e)
        {
             
        }

        private void SavedPostClick(object sender, RoutedEventArgs e)
        {

        }

        private void CommunityClick(object sender, RoutedEventArgs e)
        {

        }

        private void ChatbotClick(object sender, RoutedEventArgs e)
        {

        }

        private void PlaylistClick(object sender, RoutedEventArgs e)
        {

        }

        private void HelpClick(object sender, RoutedEventArgs e)
        {
            Frame frame = new Frame();
            frame.Navigate(new Help());
            this.Content = frame;
        }
    }
}
