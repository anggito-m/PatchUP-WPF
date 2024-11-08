using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Tutorial.xaml
    /// </summary>
    public partial class Tutorial : Page
    {
        public ObservableCollection<TutorialItem> TutorialItems { get; set; }
        public Tutorial()
        {
            InitializeComponent();
            sidebar.NavigateToPage += Sidebar_NavigateToPage;
            TutorialItems = new ObservableCollection<TutorialItem>
            {
                new TutorialItem("Type 1", "Description of type 1", "../icon/book.png"),
                new TutorialItem("Type 2", "Description of type 2", "../icon/book.png"),
                new TutorialItem("Type 3", "Description of type 3", "../icon/book.png"),
                new TutorialItem("Type 4", "Description of type 4", "../icon/book.png"),
                new TutorialItem("Type 5", "Description of type 5", "../icon/book.png"),
                new TutorialItem("Type 6", "Description of type 6", "../icon/book.png"),
                new TutorialItem("Type 7", "Description of type 7", "../icon/book.png"),
                new TutorialItem("Type 8", "Description of type 8", "../icon/book.png"),
                new TutorialItem("Type 9", "Description of type 9", "../icon/book.png")
            };

            DataContext = this;
        }
        private void Sidebar_NavigateToPage(object sender, string pageName)
        {
            Frame frame = new Frame();
            frame.Navigate(sidebar.Navigate(pageName));
            this.Content = frame;
        }
        public class TutorialItem
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string Icon { get; set; }

            public TutorialItem(string title, string description, string icon)
            {
                Title = title;
                Description = description;
                Icon = icon;
            }
        }
    }
}
