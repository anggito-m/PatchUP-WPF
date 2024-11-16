using System;
using System.Collections.Generic;
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

namespace WpfApp1.Component
{
    /// <summary>
    /// Interaction logic for PostCard.xaml
    /// </summary>
    public partial class PostCard : UserControl
    {
        public PostCard()
        {
            InitializeComponent();
        }
        public string AdminName
        {
            get => (string)GetValue(AdminNameProperty);
            set => SetValue(AdminNameProperty, value);
        }
        //days ago

        public static readonly DependencyProperty AdminNameProperty =
            DependencyProperty.Register("AdminName", typeof(string), typeof(PostCard), new PropertyMetadata(string.Empty));
        public string DaySincePost
        {
            get => GetValue(DaySincePostProperty).ToString();
            set => SetValue(DaySincePostProperty, value);
        }
        //days ago

        public static readonly DependencyProperty DaySincePostProperty =
            DependencyProperty.Register("DaySincePost", typeof(string), typeof(PostCard), new PropertyMetadata(string.Empty));
        public string PostTitle
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("PostTitle", typeof(string), typeof(PostCard), new PropertyMetadata(string.Empty));

        public string PostDescription
        {
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("PostDescription", typeof(string), typeof(PostCard), new PropertyMetadata(string.Empty));

        public ImageSource SenderIcon
        {
            get => (ImageSource)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("SenderIcon", typeof(ImageSource), typeof(PostCard), new PropertyMetadata(null));

        public ImageSource PostImage
        {
            get => (ImageSource)GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }

        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("PostImage", typeof(BitmapImage), typeof(PostCard), new PropertyMetadata(null));
    }
}
