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

        public event EventHandler<string> NavigateToPage;
        public object Navigate(string pageName)
        {
            Frame frame = new Frame();
            object page;
            switch (pageName)
            {
                case "Home":
                    page = new Home();
                    break;
                case "SavedPost":
                    page = (new Tutorial());
                    break;
                case "Tutorial":
                    page = (new Tutorial());
                    break;
                case "Community":
                    page = (new Community());
                    break;
                case "Chatbot":
                    page = (new Chatbot());
                    break;
                case "Playlist":
                    page = (new Home());
                    break;
                case "Help":
                    page = (new Help());
                    break;
                case "LogOut":
                    page = (new Chatbot());
                    break;
                case "Profile":
                    page = (new Profile());
                    break;
                default:
                    MessageBox.Show(pageName);
                    page = new Home();
                    break;
            }
            frame.Navigate(page);
            Content = frame;
            return frame;
        }

        private void HomeClick(object sender, RoutedEventArgs e)
        {
            NavigateToPage?.Invoke(this, "Home");
            /*
            Frame frame = new Frame();
            frame.Navigate(new Home());
            this.Content = frame;
            */
        }

        private void SavedPostClick(object sender, RoutedEventArgs e)
        {
            NavigateToPage?.Invoke(this, "SavedPost");
            //Frame frame = new Frame();
            //frame.Navigate(new Home());
            //this.Content = frame;
        }

        private void TutorialClick(object sender, RoutedEventArgs e)
        {
            NavigateToPage?.Invoke(this, "Tutorial");
            //Frame frame = new Frame();
            //frame.Navigate(new Tutorial());
            //this.Content = frame;
        }

        private void CommunityClick(object sender, RoutedEventArgs e)
        {
            NavigateToPage?.Invoke(this, "Community");
            //Frame frame = new Frame();
            //frame.Navigate(new Community());
            //this.Content = frame;
        }

        private void ChatbotClick(object sender, RoutedEventArgs e)
        {
            NavigateToPage?.Invoke(this, "Chatbot");
            //Frame frame = new Frame();
            //frame.Navigate(new Chatbot());
            //this.Content = frame;
        }

        private void PlaylistClick(object sender, RoutedEventArgs e)
        {
            NavigateToPage?.Invoke(this, "Playlist");
        }

        private void HelpClick(object sender, RoutedEventArgs e)
        {
            NavigateToPage?.Invoke(this, "Help");

        }

        private void ProfileClick(object sender, RoutedEventArgs e)
        {
            NavigateToPage?.Invoke(this, "Profile");
        }
        private void LogoutClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to log out?",
                "Confirm Logout",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            // ERROR
            if (result == MessageBoxResult.Yes)
            {
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.Content = new Register();
            }
        }

    }
}
