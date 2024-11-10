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
        public ObservableCollection<CategoryItem> CategoryItems { get; set; } = new ObservableCollection<CategoryItem>();
        public Tutorial()
        {
            InitializeComponent();
            sidebar.NavigateToPage += Sidebar_NavigateToPage;
            CategoryItems = product.getAllCategory();
            ProductItems = new ObservableCollection<ProductItem>();
            TutorialItems = new ObservableCollection<TutorialItem>();
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
            public string Article { get; set; }
            public DateTime Timestamp { get; set; }
            public int ProductId { get; set; }
            public int AdminId { get; set; }
            public string TutorialType { get; set; }
            public string Icon { get; set; }    

            public TutorialItem(int id, string title, string videoUrl, string article, DateTime timestamp, int productid, int adminid, string tutorialType)
            {
                Id = id;
                Title = title;
                VideoUrl = videoUrl;
                Article = article;
                Timestamp = timestamp;
                ProductId = productid;
                AdminId = adminid;
                TutorialType = tutorialType;
                Icon = "../icon/profile.png";
            }
        }

        private async void TutorialCard_GotMouseCapture(object sender, MouseButtonEventArgs e)
        {
            if (sender is TutorialCard tutorialCard && tutorialCard.DataContext is CategoryItem selectedCategory)
            {
                int selectedCategoryId = selectedCategory.Id;
                // Hide the category grid and show the tutorial grid
                ProductItems = await product.getAllProductAsync(selectedCategoryId);
                MessageBox.Show($"Loaded {ProductItems.Count} products");
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
                MessageBox.Show($"Loaded {TutorialItems.Count} products");
                MessageBox.Show($"Loaded {TutorialItems} products");

                Product_grid.Visibility = Visibility.Collapsed;
                Tutorial_grid.Visibility = Visibility.Visible;
                DataContext = this;
            }
           
        }
    }
}
