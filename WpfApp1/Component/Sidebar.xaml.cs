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
    /// Interaction logic for Sidebar.xaml
    /// </summary>
    public partial class Sidebar : UserControl
    {
        public Sidebar()
        {
            InitializeComponent();
            ProfileName.Text = Model.user.Instance.Name;
        }

        private void HomeClick(object sender, RoutedEventArgs e)
        {

        }

        private void SavedPostClick(object sender, RoutedEventArgs e)
        {

        }

        private void TutorialClick(object sender, RoutedEventArgs e)
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

        }
    }
}
