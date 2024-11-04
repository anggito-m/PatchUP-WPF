using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{

    public abstract class Tutorial
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

        // Konstruktor untuk menginisialisasi field yang wajib
        protected Tutorial(int tutorialId, int productId, int adminId, string title, DateTime timestamp)
        {
            TutorialId = tutorialId;
            ProductId = productId;
            AdminId = adminId;
            Title = title;
            Timestamp = timestamp;
        }

        // Metode abstrak yang akan diimplementasikan oleh kelas turunan
        public abstract void DisplayContent();
    }

    public class VideoTutorial : Tutorial
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

    public class ArticleTutorial : Tutorial
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
