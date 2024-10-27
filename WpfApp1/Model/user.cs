using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Npgsql;
using System.Windows;

namespace WpfApp1.Model
{
    public class user
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public user()
        {
            Username = "";
            Password = "";
            Name = "";
            Email = "";
        }
        public user(string username, string email, string name, string password)
        {
            Username = username;
            Email = email;
            Name = name;
            Password = password;
        }
        string connectionString = Environment.GetEnvironmentVariable("connectionString");
        string userID = File.ReadAllText(@"jwt.json");
        
        public void getUser(string username, string inputPassword)
        {
            string query = "SELECT * FROM \"user\" WHERE \"username\" = @username";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {

                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read(); // Baca baris pertama (harusnya hanya satu baris)
                    string realPassword = reader["password"].ToString();

                    // Sesuaikan dengan metode keamanan yang sesuai (contoh sederhana)
                    if (inputPassword == realPassword)
                    {
                        MessageBox.Show("Login Berhasil");
                    }
                    else
                    {
                        MessageBox.Show("Password salah");
                    }
                }
                else
                {
                    MessageBox.Show("Username Tidak Ditemukan"); // Username tidak ditemukan
                }

            }
        }

    }

}
