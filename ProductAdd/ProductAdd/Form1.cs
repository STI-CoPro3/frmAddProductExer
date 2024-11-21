using System.Text.RegularExpressions;

namespace ProductAdd
{

    public partial class Inventory : Form
    {
        private string _ProductName, _Category, _MfgDate, _ExpDate, _Description;
        private int _Quantity;
        private double _SellPrice;
        private BindingSource showProductList = new BindingSource();

        public string Product_Name(string name)
        {
            if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
                throw new StringFormatException("Product name must contain only letters.");
            return name;
        }

        public int Quantity(string qty)
        {
            if (!Regex.IsMatch(qty, @"^[0-9]+$"))
                throw new NumberFormatException("Quantity must be a valid positive integer.");
            return Convert.ToInt32(qty);
        }

        public double SellingPrice(string price)
        {
            if (!Regex.IsMatch(price, @"^(\d*\.)?\d+$"))
                throw new CurrencyFormatException("Selling price must be a valid decimal number.");
            return Convert.ToDouble(price);
        }


        public Inventory()
        {
            InitializeComponent();
            showProductList = new BindingSource();
            Load += Inventory_Load;
        }

        private void Inventory_Load(object sender, EventArgs e)
        {
            string[] ListOfProductCategory =
            {
            "Beverages", "Bread/Bakery", "Canned/Jarred Goods",
            "Dairy", "Frozen Goods", "Meat", "Personal Care", "Other"
        };

            foreach (string category in ListOfProductCategory)
            {
                comboBox1.Items.Add(category);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                _ProductName = textBox1.Text;
                _Category = comboBox1.Text; 
                _MfgDate = dateTimePicker1.Value.ToString("yyyy-MM-dd"); 
                _ExpDate = dateTimePicker2.Value.ToString("yyyy-MM-dd"); 
                _Description = richTextBox1.Text; 
                _Quantity = Quantity(textBox2.Text);
                _SellPrice = SellingPrice(textBox3.Text); 

                showProductList.Add(new ProductClass(_ProductName, _Category, _MfgDate, _ExpDate, _SellPrice, _Quantity, _Description));
                
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.DataSource = showProductList;
            }
            catch (StringFormatException ex)
            {
                MessageBox.Show(ex.Message, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (NumberFormatException ex)
            {
                MessageBox.Show(ex.Message, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (CurrencyFormatException ex)
            {
                MessageBox.Show(ex.Message, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    public class NumberFormatException : Exception
    {
        public NumberFormatException(string message) : base(message) { }
    }

    public class StringFormatException : Exception
    {
        public StringFormatException(string message) : base(message) { }
    }

    public class CurrencyFormatException : Exception
    {
        public CurrencyFormatException(string message) : base(message) { }
    }
    

}
