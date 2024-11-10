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
using WpfApp1.Model;
using static WpfApp1.Model.user;
using System.IO;

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
            UsernameTextBox.Focus();
        }

        private void UsernameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Password.ToString();
            if (!string.IsNullOrEmpty(username) && username.Length > 0)
            {
                if (!string.IsNullOrEmpty(password) && password.Length > 0)
                {
                    int result = user.Instance.Login(username, password);
                    if (result == 1)
                    {
                        MessageBox.Show("Login Berhasil");
                        // close this window
                        //open home window
                        HomeWindow main = new HomeWindow();
                        main.Show();
                        Window.GetWindow(this).Close();
                    }
                    else if (result == 0)
                    {
                        MessageBox.Show("Password salah");
                    }
                    else if (result == -1)
                    {
                        MessageBox.Show("Username tidak ditemukan");
                    }
                    
                }
                else
                {
                    MessageBox.Show("Please enter your password");
                }
            }
            else
            {
                MessageBox.Show("Please enter your Username");
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

        private void Remember_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void UsernameTextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if(e.KeyboardDevice.IsKeyDown(Key.Enter))
            {
                PasswordTextBox.Focus();
            }
        }

        private void UsernameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PasswordTextBox.Focus();
            }
        }

        private void PasswordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Login_Click(sender, e);
            }
        }
    }
}
