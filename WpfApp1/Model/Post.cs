using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Npgsql;
using static WpfApp1.Community;

namespace WpfApp1.Model
{
    public class Post
    {
        private static Post _instance;
        public static Post Instance => _instance ??= new Post();

        private Post() { }

        string connectionString = Environment.GetEnvironmentVariable("connectionString");

        public bool AddPost(string content, int userId)
        {
            string query = "INSERT INTO post (post_content, post_timestamp, user_id) VALUES (@content, @timestamp, @userId)";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand command = new NpgsqlCommand(query, connection);
                    command.Parameters.AddWithValue("@content", content);
                    command.Parameters.AddWithValue("@timestamp", DateTime.UtcNow);
                    command.Parameters.AddWithValue("@userId", userId);
                    int result = command.ExecuteNonQuery();

                    // Return true if the query was successful
                    return result > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to add post: " + ex.Message);
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
