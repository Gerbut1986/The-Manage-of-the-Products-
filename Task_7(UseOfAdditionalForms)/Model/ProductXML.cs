namespace Task_7_UseOfAdditionalForms_.Model
{
    using System.Xml;

    public class ProductXML
    {
        int id;
        string title, country, price;

        public ProductXML() { }
        public ProductXML(string title, string country, string price)
        {
            this.title = title;
            this.country = country;
            this.price = price;
        }

        public int Id { get { return id; } set { id = value; } }
        public string Title { get { return title; } set { title = value; } }
        public string Country { get { return country; } set { country = value; } }
        public string Price { get { return price; } set { price = value; } }

        public void Save_To_XML(XmlTextWriter writer)
        {
            writer.WriteAttributeString("Id", Id.ToString());
            writer.WriteAttributeString("Name", Title.ToString());
            writer.WriteAttributeString("Country", Country.ToString());
            writer.WriteAttributeString("Price", Price.ToString());
        }

        public override string ToString() => $"{Id} - {Title} - {Country} - {Price}";
 
    }
}
