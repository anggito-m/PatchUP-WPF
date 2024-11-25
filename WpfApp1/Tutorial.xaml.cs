using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
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
using static WpfApp1.Playlist;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Tutorial.xaml
    /// </summary>
    public partial class Tutorial : Page, INotifyPropertyChanged
    {
                public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private ObservableCollection<ProductItem> _productItems;
        public ObservableCollection<ProductItem> ProductItems
        {
            get => _productItems;
            set
            {
                _productItems = value;
                OnPropertyChanged(nameof(ProductItems));
            }
        }
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
        private ObservableCollection<TutorialItem> _tutorialArticle;
        public ObservableCollection<TutorialItem> TutorialArticle
        {
            get => _tutorialArticle;
            set
            {
                _tutorialArticle = value;
                OnPropertyChanged(nameof(TutorialArticle));
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
        //public TutorialItem TutorialArticle { get; set; }

        public ObservableCollection<CategoryItem> CategoryItems { get; set; } = new ObservableCollection<CategoryItem>();
        public Tutorial()
        {
            InitializeComponent();
            sidebar.NavigateToPage += Sidebar_NavigateToPage;
            CategoryItems = product.getAllCategory();
            ProductItems = new ObservableCollection<ProductItem>();
            TutorialItems = new ObservableCollection<TutorialItem>();
            TutorialArticle = new ObservableCollection<TutorialItem>();
            CommentItems = new ObservableCollection<CommentItem>();
            DataContext = this;
        }
        private void Sidebar_NavigateToPage(object sender, string pageName)
        {
            Frame frame = new Frame();
            frame.Navigate(sidebar.Navigate(pageName));
            this.Content = frame;
        }
        public class CategoryItem
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Icon { get; set; }

            public CategoryItem(int id, string title, string description)
            {
                Id = id;
                Title = title;
                Description = description;
                Icon = "../icon/book.png";
            }
            public CategoryItem(int id, string title, string description, string icon)
            {
                Id = id;
                Title = title;
                Description = description;
                Icon = icon;
            }
        }

        public class ProductItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Icon { get; set; }

            public ProductItem(int id, string name, string description)
            {
                Id = id;
                Name = name;
                Description = description;
                Icon = "../icon/book.png";
            }
            public ProductItem(int id, string name, string description, string icon)
            {
                Id = id;
                Name = name;
                Description = description;
                Icon = icon;
            }
        }
        public class TutorialItem
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


            //public TutorialItem() {
            //    Id = 0;
            //    Title = null;
            //    VideoUrl = null;
            //    Description = null;
            //    Timestamp = DateTime.Now;
            //    DaysSincePost = CountDays(Timestamp);
            //    ProductId = 0;
            //    AdminId = 0;
            //    AdminName = null;
            //    Article = null;
            //    Icon = "../icon/profile.png";
            //    Bitmap = null;
            //}
            public TutorialItem(int id, string title, string videoUrl, string description, DateTime timestamp, int productid, int adminid, string article)
            {
                Id = id;
                Title = title;
                VideoUrl = videoUrl;
                Description = description;
                Timestamp = timestamp;
                DaysSincePost = CountDays(timestamp);
                ProductId = productid;
                AdminId = adminid;
                AdminName = admin.Instance.GetUsername(AdminId);
                Article = article;
                Icon = "../icon/profile.png";
                Bitmap = tutorial.LoadThumbnail(videoUrl);
                CommentsCount = tutorial.GetCommentCountAsync(id).ToString()+" Comments" ;
            }
            public TutorialItem(PlaylistItem tutorialItem)
            {
                Id = tutorialItem.Id;
                Title = tutorialItem.Title;
                VideoUrl = tutorialItem.VideoUrl;
                Description = tutorialItem.Description;
                Timestamp = tutorialItem.Timestamp;
                DaysSincePost = CountDays(tutorialItem.Timestamp);
                ProductId = tutorialItem.ProductId;
                AdminId = tutorialItem.AdminId;
                AdminName = admin.Instance.GetUsername(AdminId);
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
                    return timeSincePost.Hours.ToString()+ " hours ago";
                }
                else if (timeSincePost.Days == 0 && timeSincePost.Hours == 0 && timeSincePost.Minutes != 0 )
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
        // Comment Items
        public class CommentItem
        {
            public int Id { get; set; }
            public string Content { get; set; }
            public DateTime Timestamp { get; set; }
            public int UserId { get; set; }
            public int TutorialId { get; set; }
            public string Username { get; set; }

            public CommentItem(int id, string content, DateTime timestamp, int userid, int tutorialid)
            {
                Id = id;
                Content = content;
                Timestamp = timestamp;
                UserId = userid;
                TutorialId = tutorialid;
                Username = null;
            }
        }
        private async void TutorialCard_GotMouseCapture(object sender, MouseButtonEventArgs e)
        {
            if (sender is TutorialCard tutorialCard && tutorialCard.DataContext is CategoryItem selectedCategory)
            {
                int selectedCategoryId = selectedCategory.Id;
                // Hide the category grid and show the tutorial grid
                ProductItems = await product.getAllProductAsync(selectedCategoryId);
                //MessageBox.Show($"Loaded {ProductItems.Count} products");
                Category_grid.Visibility = Visibility.Collapsed;
                Product_grid.Visibility = Visibility.Visible;
                DataContext = this;
            }
        }

        private async void TutorialCard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TutorialCard tutorialCard && tutorialCard.DataContext is ProductItem selectedProduct)
            {
                int selectedProductId = selectedProduct.Id;
                // Hide the category grid and show the tutorial grid
                TutorialItems = await tutorial.GetTutorialsAsync(selectedProductId);
                //MessageBox.Show($"Loaded {TutorialItems.Count} products");
                //MessageBox.Show($"Loaded {TutorialItems} products");
                Product_grid.Visibility = Visibility.Collapsed;
                Tutorial_grid.Visibility = Visibility.Visible;
                DataContext = this;
            }
           
        }
        
        int globalproductid;
        private async void PostCard_LeftMouseButton(object sender, MouseButtonEventArgs e)
        {
            if (sender is PostCard postCard && postCard.DataContext is TutorialItem selectedProduct)
            {
                int selectedProductId = selectedProduct.Id;
                globalproductid = selectedProductId;
                // Hide the category grid and show the tutorial grid
                //MessageBox.Show(selectedProductId.ToString());
                //VideoPlayer.Navigate(selectedProduct.VideoUrl);
                chromiumWebBrowser.Address = tutorial.ExtractEmbedUrl(selectedProduct.VideoUrl);
                TutorialArticle = await tutorial.GetTutorialArticleAsync(selectedProductId);
                // get tutorial comments from db
                CommentItems = await tutorial.GetTutorialCommentsAsync(selectedProductId);
                // Add chat buble based on commentitems counts
                foreach (CommentItem comment in CommentItems)
                {
                    AddMessageToChat(comment.Content, comment.UserId);
                }
                Tutorial_grid.Visibility = Visibility.Collapsed;
                Tutorial_article.Visibility = Visibility.Visible;
                //DataContext = this;
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
        private void AddToPlaylistButton_Click(object sender, RoutedEventArgs e)
        {
            int userId = user.Instance.Id; 
            int tutorialId = globalproductid;

            bool success = tutorial.AddTutorialToPlaylist(userId, tutorialId);
            if (!success)
            {
                MessageBox.Show("Penambahan tutorial ke playlist gagal.");
            }
        }
    }
}
