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
            string currentUsername = WpfApp1.Model.Session.Username;
            LoadProfile(currentUsername);
            sidebar.NavigateToPage += Sidebar_NavigateToPage;
        }
        private void Sidebar_NavigateToPage(object sender, string pageName)
        {
            Frame frame = new Frame();
            frame.Navigate(sidebar.Navigate(pageName));
            this.Content = frame;
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
                    bio = @bio,
                    avatar = @avatar
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
                    command.Parameters.AddWithValue("@avatar", profileImageData ?? (object)DBNull.Value);

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

        public void LoadProfile(string username)
        {
            string query = @"
                SELECT name, username, email, phone, bio, avatar
                FROM ""user""
                WHERE username = @username;
            ";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    NpgsqlCommand command = new NpgsqlCommand(query, connection);
                    command.Parameters.AddWithValue("@username", username);

                    connection.Open();
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            FullNameTextBox.Text = reader["name"]?.ToString() ?? string.Empty;
                            UserNameTextBox.Text = reader["username"]?.ToString() ?? string.Empty;
                            EmailTextBox.Text = reader["email"]?.ToString() ?? string.Empty;
                            PhoneTextBox.Text = reader["phone"]?.ToString() ?? string.Empty;
                            BioTextBox.Text = reader["bio"]?.ToString() ?? string.Empty;

                            // Mengambil avatar
                            if (reader["avatar"] != DBNull.Value)
                            {
                                byte[] imageData = (byte[])reader["avatar"];
                                profileImageData = imageData;

                                BitmapImage bitmap = new BitmapImage();
                                using (var stream = new System.IO.MemoryStream(imageData))
                                {
                                    bitmap.BeginInit();
                                    bitmap.StreamSource = stream;
                                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                                    bitmap.EndInit();
                                }
                                ProfileImage.Source = bitmap;
                            }
                            else
                            {
                                // Menggunakan gambar default jika avatar kosong
                                ProfileImage.Source = new BitmapImage(new Uri("/icon/profile.png", UriKind.Relative));
                            }
                        }
                        else
                        {
                            MessageBox.Show("Data profil tidak ditemukan.", "Data Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan saat mengambil data profil: " + ex.Message, "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private byte[]? profileImageData;

        private void ProfileImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                try
                {
                    profileImageData = System.IO.File.ReadAllBytes(filePath);

                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(filePath);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();

                    ProfileImage.Source = bitmap;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan saat mengupload gambar: " + ex.Message, "Upload Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

    }
}
