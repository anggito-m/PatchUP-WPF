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
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Playlist : Page
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        private ObservableCollection<PlaylistItem> _playlistItems;
        public ObservableCollection<PlaylistItem> playlistItems
        {
            get => _playlistItems;
            set
            {
                _playlistItems = value;
                OnPropertyChanged(nameof(playlistItems));
            }
        }
        //public ObservableCollection<PlaylistItem> playlistItems { get; set; } = new ObservableCollection<PlaylistItem>();
        public Playlist()
        {
            InitializeComponent();
            sidebar.NavigateToPage += Sidebar_NavigateToPage;
            playlistItems = await tutorial.GetMyTutorialsPlaylist(user.Instance.Id);
            MessageBox.Show(playlistItems.Count.ToString());
        }
        private void Sidebar_NavigateToPage(object sender, string pageName)
        {
            Frame frame = new Frame();
            frame.Navigate(sidebar.Navigate(pageName));
            this.Content = frame;
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
