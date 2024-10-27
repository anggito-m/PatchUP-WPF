﻿using System;
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

        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        private user()
        {

        }
        public void SetUser(string username, string email, string name, string password)
        {
            Username = username;
            Email = email;
            Name = name;
            Password = password;
        }
        //string connectionString = Environment.GetEnvironmentVariable("connectionString");
        string connectionString = "Server = junpro2024.postgres.database.azure.com; Username=adminsholeh;Database=patchup;Port=5432;Password=Usecasediagram123;SSLMode=Prefer";
        
        public void Login(string username, string inputPassword)
        {
            string query = "SELECT * FROM \"user\" WHERE \"username\" = @username";
            try { 
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
                            user.Instance.SetUser(reader["username"].ToString(), reader["email"].ToString(), reader["name"].ToString(), reader["password"].ToString()); 
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
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Register(string username, string email, string name, string password)
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
                    byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
                    command.Parameters.AddWithValue("_password", passwordBytes);
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    if (result ==1)
                    { 
                        MessageBox.Show(" Registrasi Berhasil, Silahkan menuju halaman login");
                    }
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Registrasi Gagal: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }

}
