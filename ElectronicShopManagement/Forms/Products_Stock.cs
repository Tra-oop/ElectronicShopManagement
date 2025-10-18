using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ElectronicShopManagement.Forms
{
    public partial class Products_Stock : Form
    {
        public Products_Stock()
        {
            InitializeComponent();
        }

        private void Products_Stock_Load(object sender, EventArgs e)
        {
            LoadCategories();
            LoadGrid();

            // Events
            button1.Click += BtnAdd_Click;                 // Add
            button2.Click += BtnUpdate_Click;              // Update
            button3.Click += BtnDelete_Click;              // Delete
            textBox1.TextChanged += SearchOrFilterChanged; // Search
            comboBox1.SelectedIndexChanged += SearchOrFilterChanged; // Filter by Category
            dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;
        }

      
        private void LoadCategories()
        {
            var categories = DataStorage.LoadProducts()
                                        .Select(p => p.Category)
                                        .Distinct()
                                        .OrderBy(c => c)
                                        .ToList();

            
            comboBox2.DataSource = categories;

            
            var filterCategories = new List<string> { "All" };
            filterCategories.AddRange(categories);
            comboBox1.DataSource = filterCategories;
        }

        private void LoadGrid()
        {
            var products = DataStorage.LoadProducts();
            ApplySearchAndFilter(products);
        }

        private void ApplySearchAndFilter(List<ProductsModel> all)
        {
            string keyword = textBox1.Text.Trim().ToLower();
            string category = comboBox1.SelectedItem?.ToString();

            var filtered = all
                .Where(p =>
                    (string.IsNullOrEmpty(keyword) ||
                     p.ProductID.ToLower().Contains(keyword) ||
                     p.ProductName.ToLower().Contains(keyword)) &&
                    (string.IsNullOrEmpty(category) || category == "All" || p.Category == category))
                .Select(p => new
                {
                    p.ProductID,
                    p.ProductName,
                    p.Category,
                    p.Price,
                    p.StockQuantity
                })
                .ToList();

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = filtered;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ReadOnly = true;
        }

        private void SearchOrFilterChanged(object sender, EventArgs e) => LoadGrid();

        
        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dataGridView1.Rows[e.RowIndex];
            textBox2.Text = row.Cells["ProductID"].Value?.ToString();
            textBox3.Text = row.Cells["ProductName"].Value?.ToString();
            comboBox2.Text = row.Cells["Category"].Value?.ToString();
            textBox4.Text = row.Cells["Price"].Value?.ToString();
            textBox5.Text = row.Cells["StockQuantity"].Value?.ToString();
        }

      
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(comboBox2.Text))
            {
                MessageBox.Show("Please fill in all product fields!");
                return;
            }

            if (!decimal.TryParse(textBox4.Text, out decimal price) ||
                !int.TryParse(textBox5.Text, out int qty))
            {
                MessageBox.Show("Invalid price or quantity!");
                return;
            }

            var list = DataStorage.LoadProducts();

            
            if (list.Any(p => p.ProductID == textBox2.Text.Trim()))
            {
                MessageBox.Show("ProductID already exists!");
                return;
            }

            list.Add(new ProductsModel
            {
                ProductID = textBox2.Text.Trim(),
                ProductName = textBox3.Text.Trim(),
                Category = comboBox2.Text,
                Price = price,
                StockQuantity = qty
            });

            DataStorage.SaveProducts(list);
            ClearInputs();
            LoadGrid();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            var id = textBox2.Text.Trim();
            if (string.IsNullOrEmpty(id)) { MessageBox.Show("Please select a product."); return; }

            if (!decimal.TryParse(textBox4.Text, out decimal price) ||
                !int.TryParse(textBox5.Text, out int qty))
            {
                MessageBox.Show("Invalid price or quantity!");
                return;
            }

            var list = DataStorage.LoadProducts();
            var model = list.FirstOrDefault(p => p.ProductID == id);
            if (model == null) { MessageBox.Show("Product not found."); return; }

            model.ProductName = textBox3.Text.Trim();
            model.Category = comboBox2.Text;
            model.Price = price;
            model.StockQuantity = qty;

            DataStorage.SaveProducts(list);
            ClearInputs();
            LoadGrid();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            var id = textBox2.Text.Trim();
            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Please select a product to delete!");
                return;
            }

            if (MessageBox.Show($"Are you sure to delete {id}?",
                "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            var list = DataStorage.LoadProducts();
            var model = list.FirstOrDefault(p => p.ProductID == id);
            if (model == null) { MessageBox.Show("Product not found."); return; }

            list.Remove(model);
            DataStorage.SaveProducts(list);
            ClearInputs();
            LoadGrid();
        }

        
        private void ClearInputs()
        {
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }

        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}
