using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Npgsql;
using static WpfApp1.Tutorial;

namespace WpfApp1.Model
{

    public abstract class tutorial
    {
        // Field privat
        private int tutorialId;
        private int productId;
        private int adminId;
        private string title;
        private string videoUrl;
        private string article;
        private DateTime timestamp;

        // Properti dengan akses publik untuk get dan akses terbatas untuk set
        public int TutorialId
        {
            get { return tutorialId; }
            private set { tutorialId = value; }
        }

        public int ProductId
        {
            get { return productId; }
            private set { productId = value; }
        }

        public int AdminId
        {
            get { return adminId; }
            private set { adminId = value; }
        }

        public string Title
        {
            get { return title; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    title = value;
                }
                else
                {
                    throw new ArgumentException("Title tidak boleh kosong.");
                }
            }
        }

        public string VideoUrl
        {
            get { return videoUrl; }
            set
            {
                if (Uri.IsWellFormedUriString(value, UriKind.Absolute))
                {
                    videoUrl = value;
                }
                else
                {
                    throw new ArgumentException("Format URL tidak valid.");
                }
            }
        }

        public string Article
        {
            get { return article; }
            set { article = value; }
        }

        public DateTime Timestamp
        {
            get { return timestamp; }
            private set { timestamp = value; }
        }

        protected void UpdateTutorialTitle(string newTutorialTitle)
        {
            if (newTutorialTitle == "")
            {
                throw new ArgumentException("Title harus diisi");
            }
            title = newTutorialTitle; // Memperbarui TutorialTitle
        }

        //Konstruktor untuk menginisialisasi field yang wajib
        protected tutorial(int tutorialId, int productId, int adminId, string title, DateTime timestamp)
        {
            TutorialId = tutorialId;
            ProductId = productId;
            AdminId = adminId;
            Title = title;
            Timestamp = timestamp;
        }

        //Metode abstrak yang akan diimplementasikan oleh kelas turunan
        public abstract void DisplayContent();
        static string connectionString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;
        public static async Task<ObservableCollection<TutorialItem>> GetTutorialsAsync(int productId)
        {
            ObservableCollection<TutorialItem> items = new ObservableCollection<TutorialItem>();
            string query = "SELECT * FROM tutorial WHERE product_id = @Product_Id;"; 
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    NpgsqlCommand command = new NpgsqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Product_Id", productId);
                    NpgsqlDataReader reader =await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {

                        items.Add(new TutorialItem(Convert.ToInt32(reader["tutorial_id"]), reader["tutorial_title"].ToString(), reader["tutorial_video_url"].ToString(), reader["tutorial_description"].ToString(), Convert.ToDateTime(reader["tutorial_timestamp"]), Convert.ToInt32(reader["product_id"]), Convert.ToInt32(reader["admin_id"]), reader["tutorial_article"].ToString()));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return items;
        }

        public static async Task<ObservableCollection<TutorialItem>> GetTutorialArticleAsync(int tutorialId)
        {
            ObservableCollection<TutorialItem> items = new ObservableCollection<TutorialItem>();
            string query = "SELECT * FROM tutorial WHERE tutorial_id = @tutorial_Id;";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    NpgsqlCommand command = new NpgsqlCommand(query, connection);
                    command.Parameters.AddWithValue("@tutorial_Id", tutorialId);
                    NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {

                        items.Add(new TutorialItem(Convert.ToInt32(reader["tutorial_id"]), reader["tutorial_title"].ToString(), reader["tutorial_video_url"].ToString(), reader["tutorial_description"].ToString(), Convert.ToDateTime(reader["tutorial_timestamp"]), Convert.ToInt32(reader["product_id"]), Convert.ToInt32(reader["admin_id"]), reader["tutorial_article"].ToString()));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return items;
        }
        // Get Tutorial Comments
        public static async Task<ObservableCollection<CommentItem>> GetTutorialCommentsAsync(int tutorialId)
        {
            ObservableCollection<CommentItem> items = new ObservableCollection<CommentItem>();
            string query = "SELECT * FROM tutorial_comment WHERE tutorial_id = @tutorial_Id;";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    NpgsqlCommand command = new NpgsqlCommand(query, connection);
                    command.Parameters.AddWithValue("@tutorial_Id", tutorialId);
                    NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        items.Add(new CommentItem(Convert.ToInt32(reader["comment_id"]), Convert.ToString(reader["comment_content"]), Convert.ToDateTime(reader["comment_timestamp"]), Convert.ToInt32(reader["user_id"]), Convert.ToInt32(reader["tutorial_id"])));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return items;
        }
        // Add Tutorial Comment
        public static async Task AddTutorialCommentAsync(int tutorialId, int userId, string commentContent)
        {
            string query = "INSERT INTO tutorial_comment (comment_content, comment_timestamp, user_id, tutorial_id) VALUES (@comment_content, @comment_timestamp, @user_id, @tutorial_id);";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    NpgsqlCommand command = new NpgsqlCommand(query, connection);
                    command.Parameters.AddWithValue("@comment_content", commentContent);
                    command.Parameters.AddWithValue("@comment_timestamp", DateTime.Now);
                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@tutorial_id", tutorialId);
                    await command.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public static BitmapImage LoadThumbnail(string videoUrl)
    {
        // Ekstrak video ID dari URL
        string videoId = ExtractVideoId(videoUrl);
        string thumbnailUrl = $"https://img.youtube.com/vi/{videoId}/hqdefault.jpg";

        // Buat WebClient untuk mengunduh gambar
        using (WebClient webClient = new WebClient())
        {
            byte[] imageBytes = webClient.DownloadData(thumbnailUrl);
            BitmapImage bitmap = new BitmapImage();
            using (var stream = new MemoryStream(imageBytes))
            {
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.StreamSource = stream;
                bitmap.EndInit();
            }
            return bitmap;
        }
    }
        // get count of tutorial_comment
        public static int GetCommentCountAsync(int tutorialId)
        {
            int count = 0;
            string query = "SELECT COUNT(*) FROM tutorial_comment WHERE tutorial_id = @tutorial_Id;";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand command = new NpgsqlCommand(query, connection);
                    command.Parameters.AddWithValue("@tutorial_Id", tutorialId);
                    count = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return count;
        }

        // Extract Embed video url
        public static string ExtractEmbedUrl(string url)
        {
           var videoID = ExtractVideoId(url);
            var uri = $"https://www.youtube.com/embed/{videoID}";
            return uri;
        }

        private static string ExtractVideoId(string url)
    {
        var uri = new Uri(url);
        var query = System.Web.HttpUtility.ParseQueryString(uri.Query);
        return query["v"];
    }
}

    public class VideoTutorial : tutorial
    {

        public VideoTutorial(int tutorialId, int productId, int adminId, string title, DateTime timestamp, string videoUrl)
            : base(tutorialId, productId, adminId, title, timestamp)
        {
            VideoUrl = videoUrl;
        }

        public override void DisplayContent()
        {
            Console.WriteLine($"Memutar video dari: {VideoUrl}");
        }
    }

    public class ArticleTutorial : tutorial
    {
        public ArticleTutorial(int tutorialId, int productId, int adminId, string title, DateTime timestamp, string article)
            : base(tutorialId, productId, adminId, title, timestamp)
        {
            Article = article;
        }

        public override void DisplayContent()
        {
            Console.WriteLine($"Menampilkan konten artikel:\n{Article}");
        }
    }
    public static class TutorialExtensions
    {
        // Menambahkan metode untuk memperbarui judul tutorial
        public static void UpdateTitle(this Tutorial tutorial, string newTitle)
        {
            // Memanggil metode UpdateTutorialTitle dari kelas Tutorial
            tutorial.GetType().GetMethod("UpdateTutorialTitle", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(tutorial, new object[] { newTitle });
        }
    }

}
