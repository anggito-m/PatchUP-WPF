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
using Npgsql;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Page
    {
        private string connectionString = Environment.GetEnvironmentVariable("connectionString");

        public Profile()
        {
            InitializeComponent();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            string fullName = FullNameTextBox.Text;
            string userName = UserNameTextBox.Text;
            string phone = PhoneTextBox.Text;
            string email = EmailTextBox.Text;
            string bio = BioTextBox.Text;

            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Full Name, User Name, dan Email tidak boleh kosong!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            UpdateProfile(userName, email, fullName, phone, bio);
        }

        private void UpdateProfile(string username, string email, string name, string phone, string bio)
        {
            string query = @"
                UPDATE ""user""
                SET name = @name,
                    email = @email,
                    phone = @phone,
                    bio = @bio
                WHERE username = @username;
            ";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    NpgsqlCommand command = new NpgsqlCommand(query, connection);
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@phone", phone ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@bio", bio ?? (object)DBNull.Value);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Profile berhasil diperbarui!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Profile tidak ditemukan atau tidak ada perubahan.", "Update Gagal", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan saat memperbarui profile: " + ex.Message, "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            FullNameTextBox.Text = string.Empty;
            UserNameTextBox.Text = string.Empty;
            PhoneTextBox.Text = string.Empty;
            EmailTextBox.Text = string.Empty;
            BioTextBox.Text = string.Empty;
        }
    }
}
