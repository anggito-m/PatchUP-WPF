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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void UsernameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            String[] User = {"Dani", "Adji", "Anggito" };
            if (UsernameTextBox.Text == string.Empty || PasswordTextBox.Password == string.Empty)
            {
                MessageBox.Show("Please fill all the fields");
            }
            else if (!User.Contains(UsernameTextBox.Text))
            {
                MessageBox.Show("Username is not registered");
            }
            else if(PasswordTextBox.Password != "123")
            {
                MessageBox.Show("Password is incorrect");
            }
            else
            {
                MessageBox.Show("Login Berhasil");
            }
        }

        private void Register_Button(object sender, RoutedEventArgs e)
        {
            Frame frame = new Frame();
            frame.Navigate(new Register());
            this.Content = frame;
        }

        private void Forgot_Password(object sender, RoutedEventArgs e)
        {
         
        }
        private void textEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PasswordTextBox.Focus();
        }
        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
