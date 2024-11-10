using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Npgsql;
using static WpfApp1.Tutorial;

namespace WpfApp1.Model
{
    public static class product
    {
        public static int Id { get; set; }
        public static string Name { get; set; }
        public static string Description { get; set; }
        public static int Category_Id { get; set; }
        //public static product(int id, string name, string description, int category_id)
        //{
        //    Id = id;
        //    Name = name;
        //    Description = description;
        //    Category_Id = category_id;
        //}
        static string connectionString = Environment.GetEnvironmentVariable("connectionString");

        public static ObservableCollection<CategoryItem> getAllCategory()
        {
            ObservableCollection<CategoryItem> items = new ObservableCollection<CategoryItem>();
            string query = "SELECT * FROM product_category";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand command = new NpgsqlCommand(query, connection);
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        items.Add(new CategoryItem(Convert.ToInt32(reader["category_id"]), reader["category_name"].ToString(), reader["category_description"].ToString()));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return items;
        }
        //public async static ObservableCollection<ProductItem> getAllProduct(int Category_id)
        //{
        //    ObservableCollection<ProductItem> items = new ObservableCollection<ProductItem>();
        //    string query = "SELECT * FROM product WHERE category_id=@Category_id";
        //    using (NpgsqlConnection connection = await new NpgsqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            connection.Open();
        //            NpgsqlCommand command = new NpgsqlCommand(query, connection);
        //            command.Parameters.AddWithValue("@Category_id", Category_id);
        //            NpgsqlDataReader reader = command.ExecuteReader();
        //            while (reader.Read())
        //            {
        //                items.Add(new ProductItem(Convert.ToInt32(reader["product_id"]), reader["product_name"].ToString(), reader["product_description"].ToString()));
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //        }
        //    }
        //    return items;
        //}
        public async static Task<ObservableCollection<ProductItem>> getAllProductAsync(int Category_id)
        {
            ObservableCollection<ProductItem> items = new ObservableCollection<ProductItem>();
            string query = "SELECT * FROM product WHERE category_id = @Category_Id";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync(); // Open connection asynchronously

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Category_Id", Category_id);

                        using (NpgsqlDataReader reader = await command.ExecuteReaderAsync()) // Execute reader asynchronously
                        {
                            while (await reader.ReadAsync()) // Read each row asynchronously
                            {
                                items.Add(new ProductItem(
                                    Convert.ToInt32(reader["product_id"]),
                                    reader["product_name"].ToString(),
                                    reader["product_description"].ToString()
                                ));
                            }
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return items;
        }

    }
}
