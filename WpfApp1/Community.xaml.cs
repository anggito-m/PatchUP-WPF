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
    }
}
