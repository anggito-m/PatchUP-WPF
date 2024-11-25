using CefSharp.Wpf;
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
using WpfApp1.Component;
using WpfApp1.Model;
using static WpfApp1.Tutorial;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Playlist : Page
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //private ObservableCollection<PlaylistItem> _playlistItems;
        //public ObservableCollection<PlaylistItem> playlistItems
        //{
        //    get => _playlistItems;
        //    set
        //    {
        //        _playlistItems = value;
        //        OnPropertyChanged(nameof(playlistItems));
        //    }
        //}
        private ObservableCollection<PlaylistItem> _tutorialArticle;
        public ObservableCollection<PlaylistItem> TutorialArticles
        {
            get => _tutorialArticle;
            set
            {
                _tutorialArticle = value;
                OnPropertyChanged(nameof(TutorialArticles));
            }
        }
        private ObservableCollection<CommentItem> _commentItems;
        public ObservableCollection<CommentItem> CommentItems
        {
            get => _commentItems;
            set
            {
                _commentItems = value;
                OnPropertyChanged(nameof(CommentItems));
            }
        }
        public ObservableCollection<PlaylistItem> playlistItems { get; set; } = new ObservableCollection<PlaylistItem>();
        public Playlist()
        {
            InitializeComponent();
            sidebar.NavigateToPage += Sidebar_NavigateToPage;
            playlistItems = tutorial.GetMyTutorialsPlaylist(user.Instance.Id);
            TutorialArticles = new ObservableCollection<PlaylistItem>();
            CommentItems = new ObservableCollection<CommentItem>();
            DataContext = this;

        }
        private void Sidebar_NavigateToPage(object sender, string pageName)
        {
            Frame frame = new Frame();
            frame.Navigate(sidebar.Navigate(pageName));
            this.Content = frame;
        }
        int globalproductid;

        private async void PostCard_LeftMouseButton(object sender, MouseButtonEventArgs e)
        {
            if (sender is PostCard postCard && postCard.DataContext is PlaylistItem selectedProduct)
            {
                int selectedProductId = selectedProduct.Id;
                globalproductid = selectedProductId;
                // Hide the category grid and show the tutorial grid
                //MessageBox.Show(selectedProductId.ToString());
                //VideoPlayer.Navigate(selectedProduct.VideoUrl);
                chromiumWebBrowser.Address = tutorial.ExtractEmbedUrl(selectedProduct.VideoUrl);
                TutorialArticles = await tutorial.GetPlaylistArticleAsync(selectedProductId);
                MessageBox.Show(TutorialArticles.Count.ToString());
                // get tutorial comments from db
                CommentItems = await tutorial.GetTutorialCommentsAsync(selectedProductId);
                // Add chat buble based on commentitems counts
                foreach (CommentItem comment in CommentItems)
                {
                    AddMessageToChat(comment.Content, comment.UserId);
                }
                Tutorial_grid.Visibility = Visibility.Hidden;
                Tutorial_article.Visibility = Visibility.Visible;
                DataContext = this;
            }
        }
        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string userMessage = UserInputTextBox.Text;
            if (!string.IsNullOrWhiteSpace(userMessage))
            {
                // Display user's message
                MessagesPanel.Children.Clear();
                UserInputTextBox.Clear();
                await tutorial.AddTutorialCommentAsync(globalproductid, user.Instance.Id, userMessage);

                CommentItems = await tutorial.GetTutorialCommentsAsync(globalproductid);

                // Add chat buble based on commentitems counts
                foreach (CommentItem comment in CommentItems)
                {
                    AddMessageToChat(comment.Content, comment.UserId);
                }

                // Add chat buble based on commentitems counts

                // Scroll to the latest message
            }
        }
        public void AddMessageToChat(string message, int userID)
        {
            // Create a TextBlock for the username
            TextBlock usernameText = new TextBlock
            {
                Text = Model.user.Instance.GetUsername(userID), // Set the username here
                Foreground = Brushes.DarkBlue,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 0, 0, 5) // Add some space below the username
            };

            // Create a TextBlock for the message text
            TextBlock messageText = new TextBlock
            {
                Text = message,
                Foreground = Brushes.Black,
                TextWrapping = TextWrapping.Wrap
            };

            // StackPanel to hold the username and message text
            StackPanel messageStack = new StackPanel();
            messageStack.Children.Add(usernameText); // Add username at the top
            messageStack.Children.Add(messageText); // Add message text below

            // Create the Border for the chat bubble
            Border chatBubble = new Border
            {
                Background = Brushes.LightBlue,
                CornerRadius = new CornerRadius(10),
                Padding = new Thickness(10),
                Margin = new Thickness(5),
                HorizontalAlignment = HorizontalAlignment.Left,
                Child = messageStack // Set StackPanel as the child of the Border
            };

            // Add the chat bubble to the MessagesPanel
            MessagesPanel.Children.Add(chatBubble);

        }
        public class PlaylistItem
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string VideoUrl { get; set; }
            public string Description { get; set; }
            public DateTime Timestamp { get; set; }
            public int ProductId { get; set; }
            public int AdminId { get; set; }
            public string Article { get; set; }
            public string Icon { get; set; }
            public BitmapImage Bitmap { get; set; }
            public string AdminName { get; set; }
            public string DaysSincePost { get; set; }
            public string CommentsCount { get; set; }


            public PlaylistItem(int id, string title, string videoUrl, string description, DateTime timestamp, int productid, int adminid, string article)
            {
                Id = id;
                Title = title;
                VideoUrl = videoUrl;
                Description = description;
                Timestamp = timestamp;
                DaysSincePost = CountDays(timestamp);
                ProductId = productid;
                AdminId = adminid;
                AdminName = admin.Instance.GetAdminName(AdminId);
                Article = article;
                Icon = "../icon/profile.png";
                Bitmap = tutorial.LoadThumbnail(videoUrl);
                CommentsCount = tutorial.GetCommentCountAsync(id).ToString() + " Comments";
            }
            public PlaylistItem(TutorialItem tutorialItem)
            {
                Id = tutorialItem.Id;
                Title = tutorialItem.Title;
                VideoUrl = tutorialItem.VideoUrl;
                Description = tutorialItem.Description;
                Timestamp = tutorialItem.Timestamp;
                DaysSincePost = CountDays(tutorialItem.Timestamp);
                ProductId = tutorialItem.ProductId;
                AdminId = tutorialItem.AdminId;
                AdminName = admin.Instance.GetAdminName(AdminId);
                Article = tutorialItem.Article;
                Icon = "../icon/profile.png";
                Bitmap = tutorial.LoadThumbnail(tutorialItem.VideoUrl);
                CommentsCount = tutorial.GetCommentCountAsync(tutorialItem.Id).ToString() + " Comments";
            }
            private string CountDays(DateTime timestamp)
            {
                TimeSpan timeSincePost = DateTime.Now - timestamp;
                if (timeSincePost.Days == 0 && timeSincePost.Hours != 0)
                {
                    return timeSincePost.Hours.ToString() + " hours ago";
                }
                else if (timeSincePost.Days == 0 && timeSincePost.Hours == 0 && timeSincePost.Minutes != 0)
                {
                    return timeSincePost.Minutes.ToString() + " minutes ago";
                }
                else if (timeSincePost.Days == 0 && timeSincePost.Hours == 0 && timeSincePost.Minutes == 0)
                {
                    return timeSincePost.Seconds.ToString() + " seconds ago";
                }
                else
                {
                    return timeSincePost.Days.ToString() + " days ago";
                }
            }
        }
       
    }
}
