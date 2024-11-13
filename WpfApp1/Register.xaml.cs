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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Page
    {
        
        public Register()
        {
            InitializeComponent();
        }
        private void UsernameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (UsernameTextBox.Text == string.Empty || PasswordTextBox.Password == string.Empty || ConfirmPasswordTextBox.Password == string.Empty || NameTextBox.Text==string.Empty || EmailTextBox.Text==string.Empty )
            {
                MessageBox.Show("Please fill all the fields");
            }
            else if (PasswordTextBox.Password != ConfirmPasswordTextBox.Password)
            {
                MessageBox.Show("Password and Confirm Password must be same");
            }
            else if (agreeBox.IsChecked == false)
            {
                MessageBox.Show("Please agree to the terms and conditions");
            }
            else
            {
                user.Instance.Register(UsernameTextBox.Text, EmailTextBox.Text, NameTextBox.Text, PasswordTextBox.Password);
            }
        }

        private void Login_Page(object sender, RoutedEventArgs e)
        {
            Frame frame = new Frame();
            frame.Navigate(new Login());
            this.Content = frame;
        }

        private void Terms_Click(object sender, RoutedEventArgs e)
        {
            TermsPopup.IsOpen = true;
        }

        private void CloseTermsPopup_Click(object sender, RoutedEventArgs e)
        {
            TermsPopup.IsOpen = false;
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Login());
        }

        private bool isPasswordVisible = false;
        private bool isConfirmPasswordVisible = false;

        private void TogglePasswordVisibility_Click(object sender, RoutedEventArgs e)
        {
            if (isPasswordVisible)
            {
                PasswordTextBoxVisible.Visibility = Visibility.Collapsed;
                PasswordTextBox.Visibility = Visibility.Visible;
                PasswordTextBox.Password = PasswordTextBoxVisible.Text;
                isPasswordVisible = false;
            }
            else
            {
                PasswordTextBoxVisible.Visibility = Visibility.Visible;
                PasswordTextBox.Visibility = Visibility.Collapsed;
                PasswordTextBoxVisible.Text = PasswordTextBox.Password;
                isPasswordVisible = true;
            }
        }

        private void ToggleConfirmPasswordVisibility_Click(object sender, RoutedEventArgs e)
        {
            if (isConfirmPasswordVisible)
            {
                ConfirmPasswordTextBoxVisible.Visibility = Visibility.Collapsed;
                ConfirmPasswordTextBox.Visibility = Visibility.Visible;
                ConfirmPasswordTextBox.Password = ConfirmPasswordTextBoxVisible.Text;
                isConfirmPasswordVisible = false;
            }
            else
            {
                ConfirmPasswordTextBoxVisible.Visibility = Visibility.Visible;
                ConfirmPasswordTextBox.Visibility = Visibility.Collapsed;
                ConfirmPasswordTextBoxVisible.Text = ConfirmPasswordTextBox.Password;
                isConfirmPasswordVisible = true;
            }
        }

    }
}
