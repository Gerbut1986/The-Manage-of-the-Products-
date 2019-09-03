namespace Task_7_UseOfAdditionalForms_
{
    using System;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;
    using Task_7_UseOfAdditionalForms_.Model;

    public partial class productForm : Form
    {
        Form2 f2;
        XmlTextWriter writer;

        public productForm()
        {
            InitializeComponent();
            writer = new XmlTextWriter(@"C:\Users\Admin\Documents\The-Manage-of-the-Products-\Task_7(UseOfAdditionalForms)\Data\myProducts.xml", Encoding.Unicode);
            listBox1.Items.AddRange(ReadFromDatabase.Read_Products_From_DB().ToArray());
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            f2 = new Form2("ADD");
            f2.Text = "Add Product";
            f2.Owner = this;//
            f2.ShowDialog();
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                ProductXML selectedProd = (ProductXML)listBox1.SelectedItem; // listBox1.SelectedItem as ProductXML
                f2 = new Form2("EDIT", selectedProd);
                f2.Owner = this;
                f2.Text = "Editing The Product";
                f2.ShowDialog();
            }
        }

        private void productForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument();
                writer.WriteStartElement("Full_List_Products");
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    writer.WriteStartElement("Name_Of_a_Product");
                    ((ProductXML)(listBox1.Items[i])).Save_To_XML(writer);
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
            catch (XmlException xml)
            {
                MessageBox.Show(xml.Message, "failed...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                writer.Close();
            }
        }

        private void delBtn_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            else MessageBox.Show("You didn't select any element", "This has failed...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count == 0)
                MessageBox.Show("ListBox is Empty!! First you need to add any Products", "EMPTY", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else listBox1.Items.Clear();
        }
    }
}

