namespace Task_7_UseOfAdditionalForms_
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Windows.Forms;
    using Task_7_UseOfAdditionalForms_.Model;

    public static class ReadFromDatabase
    {
        public static List<ProductXML> Read_Products_From_DB()
        {
            List<ProductXML> products = new List<ProductXML>();
            string query = "SELECT Id, Title, Country, Price from dbo.Products";
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader dReader = null;

            try
            {
                using (conn = new SqlConnection(Connect.ConnectionString))
                {
                    using (cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        dReader = cmd.ExecuteReader();

                        while (dReader.Read())
                        {
                            ProductXML p = new ProductXML();
                            p.Id = Convert.ToInt32(dReader["id"]);
                            p.Title = dReader["title"].ToString();
                            p.Country = dReader["country"].ToString();
                            p.Price = Convert.ToInt32(dReader["price"]);

                            products.Add(p);
                        }
                    }
                }
            }
            catch (DataException ex)
            {
                MessageBox.Show(ex.Message, "failed...Something went wrong..", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dReader.Close();
                conn.Close();
            }

            return products;
        }
    }
}
