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
        private static user _instance;
        public static user Instance => _instance ??= new user();

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        protected user()
        {

        }
        public void SetUser(int id, string username, string email, string name, string password)
        {
            Id = id;
            Username = username;
            Email = email;
            Name = name;
            Password = password;
        }
        static string connectionString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;
        public int Login(string username, string inputPassword)
        {
            string query = "SELECT * FROM \"user\" WHERE \"username\" = @username";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    NpgsqlCommand command = new NpgsqlCommand(query, connection);
                    command.Parameters.AddWithValue("@username", username);
                    connection.Open();
                    NpgsqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read(); // Baca baris pertama (harusnya hanya satu baris)
                        string realPassword = System.Text.Encoding.Unicode.GetString((byte[])reader["password"]);


                        // Sesuaikan dengan metode keamanan yang sesuai (contoh sederhana)
                        if (inputPassword == realPassword)
                        {
                            user.Instance.SetUser(Convert.ToInt32(reader["user_id"]),reader["username"].ToString(), reader["email"].ToString(), reader["name"].ToString(), reader["password"].ToString());
                            return 1; // Login berhasil
                        }
                        else
                        {
                            return 0; // Password salah
                        }
                    }
                    else
                    {
                        return -1; // Username tidak ditemukan
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show(ex.Message);
                    return -2;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public bool Register(string username, string email, string name, string password)
        {
            string query = "SELECT \"user_insert\"(@_name, @_email, @_password, @_username);";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    NpgsqlCommand command = new NpgsqlCommand(query, connection);
                    command.Parameters.AddWithValue("_username", username);
                    command.Parameters.AddWithValue("_email", email);
                    command.Parameters.AddWithValue("_name", name);
                    byte[] passwordBytes = System.Text.Encoding.Unicode.GetBytes(password);
                    command.Parameters.AddWithValue("_password", passwordBytes);
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    if (result > 0 || result == -1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show("Registrasi Gagal: " + ex.Message);
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        // get username by userID
        public string GetUsername(int userId)
        {
            string query = "SELECT username FROM \"user\" WHERE user_id = @User_Id";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand command = new NpgsqlCommand(query, connection);
                    command.Parameters.AddWithValue("@User_Id", userId);
                    NpgsqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        return reader["username"].ToString();
                    }
                    else
                    {
                        return "User not found";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show(ex.Message);
                    return "Error";
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
    //Buat turunan class user bernama admin
    public class admin : user
    {
        private static admin _instance;
        public static admin Instance => _instance ??= new admin();
        private admin()
        {

        }
        // Create Method to get admin name given admin ID
        static string connectionString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

        public string GetAdminName(int adminId)
        {
            string query = "SELECT name FROM \"ADMIN\" WHERE admin_id = @Admin_Id";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand command = new NpgsqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Admin_Id", adminId);
                    NpgsqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        return reader["name"].ToString();
                    }
                    else
                    {
                        return "Admin not found";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show(ex.Message);
                    return "Error";
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
