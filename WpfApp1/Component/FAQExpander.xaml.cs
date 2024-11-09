using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Component
{
    public partial class FAQExpander : UserControl
    {
        public FAQExpander()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        // DependencyProperty untuk HeaderText
        public string HeaderText
        {
            get { return (string)GetValue(HeaderTextProperty); }
            set { SetValue(HeaderTextProperty, value); }
        }

        public static readonly DependencyProperty HeaderTextProperty =
            DependencyProperty.Register("HeaderText", typeof(string), typeof(FAQExpander), new PropertyMetadata(string.Empty));

        // DependencyProperty untuk ContentText
        public string ContentText
        {
            get { return (string)GetValue(ContentTextProperty); }
            set { SetValue(ContentTextProperty, value); }
        }

        public static readonly DependencyProperty ContentTextProperty =
            DependencyProperty.Register("ContentText", typeof(string), typeof(FAQExpander), new PropertyMetadata(string.Empty));
    }
}
