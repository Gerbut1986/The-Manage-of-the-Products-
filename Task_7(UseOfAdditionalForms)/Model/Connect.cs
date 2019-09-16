namespace Task_7_UseOfAdditionalForms_.Model
{
    using System.Configuration;
    using System.Data.SqlClient;

    static class Connect
    {
        static SqlConnection conn;
        static SqlCommand cmd;

        public static string ConnectionString 
            => ConfigurationManager.ConnectionStrings["my_db1"].ConnectionString;

        // Add method:
        public static string AddProduct(ProductXML prod)
        {
            string msg = string.Empty;
            string query = "INSERT INTO Products(Title, Country, Price) VALUES(@Title, @Country, @Price)";
            using (conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Title", prod.Title);
                    cmd.Parameters.AddWithValue("@Country", prod.Country);
                    cmd.Parameters.AddWithValue("@Price", prod.Price);

                    int result = cmd.ExecuteNonQuery();

                    if (result == 1)
                        msg = "Product was added successfully";
                    else
                        msg = "Product was NOT added. Please try it again";
                }
            }

            return msg;
        }

        // Edit method:
        public static string UpdateProduct(ProductXML prod)
        {
            string msg = string.Empty;
            string query = $"UPDATE Products SET Title = @Title, Country = @Country, Price = @Price WHERE Id = {prod.Id}";
            using (conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Title", prod.Title);
                    cmd.Parameters.AddWithValue("@Country", prod.Country);
                    cmd.Parameters.AddWithValue("@Price", prod.Price);

                    int result = cmd.ExecuteNonQuery();

                    if (result == 1)
                        msg = "Product was Updated successfully";
                    else
                        msg = "Product was NOT Update.. Something went wrong...";
                }
            }

            return msg;
        }

        // Delete method:
        public static string DeleteProduct(ProductXML prod)
        {
            string msg = string.Empty;
            string query = $"DELETE Products WHERE Id = {prod.Id}";
            using (conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Title", prod.Title);
                    cmd.Parameters.AddWithValue("@Country", prod.Country);
                    cmd.Parameters.AddWithValue("@Price", prod.Price);

                    int result = cmd.ExecuteNonQuery();

                    if (result == 1)
                        msg = "Product was Delete successfully";
                    else
                        msg = "Product was NOT Delete.. Something went wrong...";
                }
            }

            return msg;
        }
    }
}
