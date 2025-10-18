using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ElectronicShopManagement.Forms
{
    public partial class Products_Stock : Form
    {
        public static List<ProductsModel> SharedProducts = new List<ProductsModel>();

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
        private void Products_Stock_Load(object sender, EventArgs e)
        {
            // Load products first time
            if (SharedProducts.Count == 0)
            {
                SharedProducts = ProductData.GetProducts().ToList();
            }

            // Make table columns
            tblproductstock.Columns.Clear();
            tblproductstock.Columns.Add("ProductID", "Product ID");
            tblproductstock.Columns.Add("ProductName", "Product Name");
            tblproductstock.Columns.Add("Category", "Category");
            tblproductstock.Columns.Add("Price", "Price");
            tblproductstock.Columns.Add("StockQuantity", "Quantity");
            tblproductstock.Columns["Price"].DefaultCellStyle.Format = "C2";

            // IMPORTANT: Allow row selection
            tblproductstock.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            tblproductstock.ReadOnly = true;
            tblproductstock.AllowUserToAddRows = false;

            // Show products in table
            ShowProductsInTable();

            // Put categories in dropdowns
            PutCategoriesInDropdowns();
        }

        private void ShowProductsInTable()
        {
            // Clear table first
            tblproductstock.Rows.Clear();

            // Add each product to table
            foreach (var product in SharedProducts)
            {
                tblproductstock.Rows.Add(
                    product.ProductID,
                    product.ProductName,
                    product.Category,
                    product.Price,
                    product.StockQuantity
                );
            }
        }

        private void PutCategoriesInDropdowns()
        {
            // Get all different categories
            var allCategories = SharedProducts.Select(p => p.Category).Distinct().ToList();

            // Fill search dropdown
            comboBox1.Items.Clear();
            comboBox1.Items.Add("All Categories");
            foreach (var cat in allCategories)
            {
                comboBox1.Items.Add(cat);
            }
            comboBox1.SelectedIndex = 0;

            // Fill category dropdown
            comboboxcategory.Items.Clear();
            foreach (var cat in allCategories)
            {
                comboboxcategory.Items.Add(cat);
            }
            if (comboboxcategory.Items.Count > 0)
                comboboxcategory.SelectedIndex = 0;
        }

        // When user types in search box
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DoSearch();
        }

        // When user picks category
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DoSearch();
        }

        private void DoSearch()
        {
            string searchText = textBox1.Text.ToLower();
            string pickedCategory = comboBox1.SelectedItem?.ToString();

            // Start with all products
            var foundProducts = SharedProducts.ToList();

            // If user typed something, search
            if (!string.IsNullOrEmpty(searchText))
            {
                foundProducts = foundProducts.Where(p =>
                    p.ProductName.ToLower().Contains(searchText) ||
                    p.ProductID.ToLower().Contains(searchText)
                ).ToList();
            }

            // If user picked a category, filter
            if (pickedCategory != "All Categories")
            {
                foundProducts = foundProducts.Where(p => p.Category == pickedCategory).ToList();
            }

            // Show found products
            tblproductstock.Rows.Clear();
            foreach (var product in foundProducts)
            {
                tblproductstock.Rows.Add(
                    product.ProductID,
                    product.ProductName,
                    product.Category,
                    product.Price,
                    product.StockQuantity
                );
            }
        }

        // Add button click
        private void button1_Click(object sender, EventArgs e)
        {
            // Check if all boxes are filled
            if (txtproid.Text == "" || txtproname.Text == "" || txtproprice.Text == "" || txtproqty.Text == "")
            {
                MessageBox.Show("Please fill all boxes");
                return;
            }

            // Check if ID already used
            if (SharedProducts.Any(p => p.ProductID == txtproid.Text))
            {
                MessageBox.Show("This ID already exists. Use different ID.");
                return;
            }

            // Make new product
            var newProduct = new ProductsModel
            {
                ProductID = txtproid.Text,
                ProductName = txtproname.Text,
                Category = comboboxcategory.SelectedItem.ToString(),
                Price = decimal.Parse(txtproprice.Text),
                StockQuantity = int.Parse(txtproqty.Text)
            };

            // Add to list
            SharedProducts.Add(newProduct);

            // Clear boxes and refresh
            ClearForm();
            ShowProductsInTable();
            PutCategoriesInDropdowns();

            MessageBox.Show("Product added!");
        }

        // Update button click
        private void button2_Click(object sender, EventArgs e)
        {
            // Check if user picked a product
            if (tblproductstock.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please click on a product in the table first");
                return;
            }

            // Check if all boxes are filled
            if (txtproid.Text == "" || txtproname.Text == "" || txtproprice.Text == "" || txtproqty.Text == "")
            {
                MessageBox.Show("Please fill all boxes");
                return;
            }

            // Get old product ID from the selected row
            var selectedRow = tblproductstock.SelectedRows[0];
            string oldId = selectedRow.Cells["ProductID"].Value.ToString();
            string newId = txtproid.Text;

            // Check if new ID already used
            if (oldId != newId && SharedProducts.Any(p => p.ProductID == newId))
            {
                MessageBox.Show("This ID already exists. Use different ID.");
                return;
            }

            // Find the product to update
            var product = SharedProducts.FirstOrDefault(p => p.ProductID == oldId);
            if (product != null)
            {
                // Update product info
                product.ProductID = newId;
                product.ProductName = txtproname.Text;
                product.Category = comboboxcategory.SelectedItem.ToString();
                product.Price = decimal.Parse(txtproprice.Text);
                product.StockQuantity = int.Parse(txtproqty.Text);
            }

            // Refresh table
            ShowProductsInTable();
            PutCategoriesInDropdowns();

            MessageBox.Show("Product updated!");
        }

        // Delete button click
        private void button3_Click(object sender, EventArgs e)
        {
            // Check if user picked a product
            if (tblproductstock.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please click on a product in the table first");
                return;
            }

            // Get product info from selected row
            var selectedRow = tblproductstock.SelectedRows[0];
            string productId = selectedRow.Cells["ProductID"].Value.ToString();
            string productName = selectedRow.Cells["ProductName"].Value.ToString();

            // Ask user to confirm
            var result = MessageBox.Show($"Delete {productName}?", "Confirm", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                // Remove from list
                SharedProducts.RemoveAll(p => p.ProductID == productId);

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
                // Clear form and refresh
                ClearForm();
                ShowProductsInTable();
                PutCategoriesInDropdowns();

                MessageBox.Show("Product deleted!");
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            var id = textBox2.Text.Trim();
            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Please select a product to delete!");
                return;
            }
        // When user clicks on table row
        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (tblproductstock.SelectedRows.Count > 0)
            {
                var row = tblproductstock.SelectedRows[0];
                txtproid.Text = row.Cells["ProductID"].Value.ToString();
                comboboxcategory.SelectedItem = row.Cells["Category"].Value.ToString();
                txtproname.Text = row.Cells["ProductName"].Value.ToString();
                txtproprice.Text = row.Cells["Price"].Value.ToString();
                txtproqty.Text = row.Cells["StockQuantity"].Value.ToString();
            }
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
        private void ClearForm()
        {
            txtproid.Text = "";
            txtproname.Text = "";
            txtproprice.Text = "";
            txtproqty.Text = "";
            if (comboboxcategory.Items.Count > 0)
                comboboxcategory.SelectedIndex = 0;
            tblproductstock.ClearSelection();
        }

        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) { }

        
        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}