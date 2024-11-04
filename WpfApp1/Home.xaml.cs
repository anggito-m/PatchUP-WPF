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
            sidebar.NavigateToPage += Sidebar_NavigateToPage;
        }

        private void Sidebar_NavigateToPage(object sender, string pageName)
        {
            Frame frame = new Frame();
            switch (pageName)
            {
                case "Home":
                    frame.Navigate(new Tutorial());
                    this.Content = frame;
                    break;
            }
        }

        private void BtnCommunityClick(object sender, RoutedEventArgs e)
        {
            Frame frame = new Frame();
            frame.Navigate(new Community());
            this.Content = frame;
        }

        private void BtnTutorialClick(object sender, RoutedEventArgs e)
        {
            Frame frame = new Frame();
            frame.Navigate(new Tutorial());
            this.Content = frame;
        }

        private void BtnChatbotClick(object sender, RoutedEventArgs e)
        {
            Frame frame = new Frame();
            frame.Navigate(new Chatbot());
            this.Content = frame;
        }

    }
}
