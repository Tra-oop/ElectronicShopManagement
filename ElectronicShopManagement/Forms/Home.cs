using System;
using System.Linq;
using System.Windows.Forms;

namespace ElectronicShopManagement.Forms
{
    public partial class Home : Form
    {
        private const int LOW_STOCK_THRESHOLD = 20;

       
        private Label ReportsCountLabel => label6;
    

        public Home()
        {
            InitializeComponent();

            
            DataStorage.HistoryChanged += RefreshHistoryGrid;
            
            DataStorage.ProductsChanged += RefreshDashboard;
            
            DataStorage.HistoryChanged += UpdateReportsCount;

            
            label4.Cursor = Cursors.Hand;
            label4.Click += (s, e) => ShowLowStockList();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            RefreshHistoryGrid();
            RefreshDashboard();
            UpdateReportsCount(); 
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            DataStorage.HistoryChanged -= RefreshHistoryGrid;
            DataStorage.ProductsChanged -= RefreshDashboard;
            DataStorage.HistoryChanged -= UpdateReportsCount;
            base.OnFormClosed(e);
        }

        private void RefreshDashboard()
        {
            var all = DataStorage.GetAllProducts();

            int totalProducts = all.Count;
            int lowStockCount = all.Count(p => p.StockQuantity <= LOW_STOCK_THRESHOLD);

            label2.Text = totalProducts.ToString();  // Products Stock
            label4.Text = lowStockCount.ToString();  // Low Stocks
        }

        
        private void UpdateReportsCount()
        {
            var count = DataStorage.GetAllHistory().Count;
            if (ReportsCountLabel != null)
                ReportsCountLabel.Text = count.ToString();
        }
        

        private void RefreshHistoryGrid()
        {
            var data = DataStorage.GetAllHistory()
                .Select(s => new
                {
                    s.SoldAt,
                    s.ProductID,
                    s.ProductName,
                    s.Category,
                    Price = s.Price.ToString("0.00"),
                    s.Quantity,
                    Amount = s.Amount.ToString("0.00")
                })
                .OrderByDescending(x => x.SoldAt)
                .ToList();

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = data;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
        }

        private void ShowLowStockList()
        {
            var lowStocks = DataStorage.GetAllProducts()
                .Where(p => p.StockQuantity <= LOW_STOCK_THRESHOLD)
                .OrderBy(p => p.StockQuantity)
                .Select(p => new
                {
                    p.ProductID,
                    p.ProductName,
                    p.Category,
                    p.Price,
                    p.StockQuantity
                })
                .ToList();

            if (lowStocks.Count == 0)
            {
                MessageBox.Show(
                    "No products with stock less than 20 items",
                    "Low Stock",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }

            
            Form popup = new Form
            {
                Text = $"Products with stock ≤ {LOW_STOCK_THRESHOLD}",
                Size = new System.Drawing.Size(700, 400),
                StartPosition = FormStartPosition.CenterParent
            };

            DataGridView grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                DataSource = lowStocks,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            popup.Controls.Add(grid);
            popup.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}
