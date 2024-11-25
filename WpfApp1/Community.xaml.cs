using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using WpfApp1.Model;
using static WpfApp1.Tutorial;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Community.xaml
    /// </summary>
    public partial class Community : Page, INotifyPropertyChanged
    {
        public Community()
        {
            InitializeComponent();
            sidebar.NavigateToPage += Sidebar_NavigateToPage;
        }
        private void Sidebar_NavigateToPage(object sender, string pageName)
        {
            Frame frame = new Frame();
            frame.Navigate(sidebar.Navigate(pageName));
            this.Content = frame;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private ObservableCollection<TutorialItem> _tutorialItems;
        public ObservableCollection<TutorialItem> TutorialItems
        {
            get => _tutorialItems;
            set
            {
                _tutorialItems = value;
                OnPropertyChanged(nameof(TutorialItems));
            }
        }

        private void WritePostTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void PostButton_Click(object sender, RoutedEventArgs e)
        {
            string postContent = WritePostTextBox.Text;

            if (string.IsNullOrWhiteSpace(postContent) || postContent == "Write something...")
            {
                MessageBox.Show("Please write something before posting.");
                return;
            }

            int userId = user.Instance.Id; // Replace with the actual user ID if available

            // Add the post to the database
            bool isSuccess = Post.Instance.AddPost(postContent, userId);
            if (isSuccess)
            {
                MessageBox.Show("Your post has been added successfully!");

                // Clear the TextBox after successful posting
                WritePostTextBox.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("An error occurred while adding your post.");
            }
        }

        private void WriteTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (WritePostTextBox.Text == "Write something...")
            {
                WritePostTextBox.Text = "";
                WritePostTextBox.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void WriteTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(WritePostTextBox.Text))
            {
                WritePostTextBox.Text = "Write something...";
                WritePostTextBox.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }
    }
}
