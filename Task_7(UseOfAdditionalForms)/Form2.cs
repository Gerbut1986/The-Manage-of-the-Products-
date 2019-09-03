using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Task_7_UseOfAdditionalForms_.Model;

namespace Task_7_UseOfAdditionalForms_
{
    public partial class Form2 : Form
    {
        readonly string connectionString;
        string mode;
        ProductXML product;
        public Form2(string mode)
        {
            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\Documents\The-Manage-of-the-Products-\Task_7(UseOfAdditionalForms)\Data\MyDB.mdf;Integrated Security=True";
            InitializeComponent();
            this.mode = mode;
        }

        public Form2(string mode, ProductXML product)
        {
            InitializeComponent();
            this.mode = mode;
            this.product = product;
            if (mode.Equals("EDIT"))
            {
                titleTxt.Text = product.Title;
                countryTxt.Text = product.Country;
                priceTxt.Text = product.Price;
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            if (mode.Equals("ADD"))
            {
                string result = string.Empty;
                try
                {
                    try
                    {
                        if (titleTxt.Text == "" && countryTxt.Text == "" && priceTxt.Text == "")
                            MessageBox.Show("All Fields is Empty!! Try To Fill it!", "All fields Empty", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        else if (titleTxt.Text == "" || countryTxt.Text == "" || priceTxt.Text == "")
                            MessageBox.Show("Some of Field is Empty!! Try To Fill it!", "Some field Empty", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        else
                        {
                            ProductXML product = new ProductXML(titleTxt.Text, countryTxt.Text, priceTxt.Text);
                            result = AddProduct(product);

                            if (result == "Product was added successfully")
                            {                 
                                //List<ProductXML> products = new List<ProductXML>();
                                (this.Owner as productForm).listBox1.Items.Add(product);
                                MessageBox.Show("Add to List & Xml File & Database!", result, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                titleTxt.Text = "";
                                countryTxt.Text = "";
                                priceTxt.Text = "";
                            }
                            else
                                MessageBox.Show(result, "failed...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch { }
                }
                catch (DataException de)
                {
                    MessageBox.Show(de.Message, "System of an Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (mode.Equals("EDIT"))
            {
                int index = (this.Owner as productForm).listBox1.Items.IndexOf(this.product);
                (this.Owner as productForm).listBox1.Items.RemoveAt(index);
                (this.Owner as productForm).listBox1.Items.Insert(index, new ProductXML(titleTxt.Text, countryTxt.Text, priceTxt.Text));
                (this.Owner as productForm).listBox1.DisplayMember = "";
                this.Close();
            }
            else MessageBox.Show("Something went wrong...", "..failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        string AddProduct(ProductXML prod)
        {
            string msg = string.Empty;
            string query = "INSERT INTO Products(Title, Country, Price) VALUES(@Title, @Country, @Price)";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Title", prod.Title);
            cmd.Parameters.AddWithValue("@Country", prod.Country);
            cmd.Parameters.AddWithValue("@Price", prod.Price);

            int result = cmd.ExecuteNonQuery();

            if (result == 1)
                msg = "Product was added successfully";
            else
                msg = "Product was NOT added. Please try it again";

            return msg;
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void titleTxt_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(titleTxt.Text))
            {
                errorProvider1.SetError(titleTxt, "You left the text field empty!");
            }
        }

        private void countryTxt_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(countryTxt.Text))
            {
                errorProvider1.SetError(countryTxt, "You left the text field empty!");
            }
        }

        private void priceTxt_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(priceTxt.Text))
            {
                errorProvider1.SetError(priceTxt, "You left the text field empty!");
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            (this.Owner as productForm).listBox1.Items.Clear();
            (this.Owner as productForm).listBox1.Items.AddRange(ReadFromDatabase.Read_Products_From_DB().ToArray());
        }
    }
}
