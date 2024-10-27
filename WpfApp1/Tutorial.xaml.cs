using System.Collections.ObjectModel;
using System.Windows.Controls;
using WpfApp1.Model;

namespace WpfApp1
{
    public partial class Tutorial : Page
    {
        public ObservableCollection<CardModel> Cards { get; set; }

        public Tutorial()
        {
            InitializeComponent();

            Cards = new ObservableCollection<CardModel>
            {
                new CardModel { Title = "Tutorial", Description = "Simplify your decisions through our Smart Menu Assistant who will help you.", ImageSource = "/icon/book.png" },
                // Tambahkan kartu lainnya sesuai kebutuhan
            };

            DataContext = this;
        }
    }
}