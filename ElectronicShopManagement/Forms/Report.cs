using System;
using System.Linq;
using System.Windows.Forms;

namespace ElectronicShopManagement.Forms
{
    public partial class Report : Form
    {
        public Report()
        {
            InitializeComponent();

            
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd/MM/yyyy";

            
            dateTimePicker1.Value = DateTime.Today;   
            dateTimePicker2.Value = DateTime.Today;   

            
            dateTimePicker1.ValueChanged += FilterChanged;
            dateTimePicker2.ValueChanged += FilterChanged;

            
            DataStorage.HistoryChanged += LoadReport;

            LoadReport();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            DataStorage.HistoryChanged -= LoadReport;
            base.OnFormClosed(e);
        }

        private void FilterChanged(object sender, EventArgs e) => LoadReport();

        private void LoadReport()
        {
            
            DateTime fromDate = dateTimePicker1.Value.Date;
            DateTime toDate = dateTimePicker2.Value.Date;

           
            if (fromDate > toDate) (fromDate, toDate) = (toDate, fromDate);

            DateTime from = fromDate;
            DateTime to = toDate.AddDays(1).AddTicks(-1);

            var history = DataStorage.GetAllHistory();

            var filtered = history
                .Where(h => h.SoldAt >= from && h.SoldAt <= to)
                .OrderByDescending(h => h.SoldAt)
                .ToList();

            var rows = filtered
                .Select(h => new
                {
                    h.SoldAt,
                    h.ProductID,
                    ProductName = h.ProductName,
                    h.Category,
                    Price = h.Price.ToString("0.00"),
                    h.Quantity,
                    Amount = (h.Price * h.Quantity).ToString("0.00")
                })
                .ToList();

           
            tblshowreport.DataSource = null;
            tblshowreport.DataSource = rows;

            
            tblshowreport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tblshowreport.ReadOnly = true;
            tblshowreport.RowHeadersVisible = false;
            tblshowreport.AllowUserToAddRows = false;
            tblshowreport.AllowUserToDeleteRows = false;
            tblshowreport.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

           
            decimal total = filtered.Sum(h => h.Price * h.Quantity);
            this.Text = $"Report  •  Total: {total:0.00}";
        }

       
        private void Report_Load(object sender, EventArgs e) { }
        private void tblshowreport_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}
