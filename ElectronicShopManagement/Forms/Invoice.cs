using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElectronicShopManagement.Forms
{
    public partial class Invoice : Form
    {
        public Invoice()
        {
            InitializeComponent();
        }

        private void Invoice_Load(object sender, EventArgs e)
        {
            //List<ProductForSellModel> employees;
            // your data

            var data = Sell.products;
            var invoiceData = data.Select(p => new ProductForSellModel
            {
                Name = p.Name,
                Prices = p.Prices,
                SellQty = p.SellQty,
                totalAmount = p.totalAmount,
                Cashier=p.Cashier,
                date=p.date,
                Amount=p.Amount,
                Categories=p.Categories
            }).ToList();
            ReportDataSource rds = new ReportDataSource("DataSet", invoiceData);

            //reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
            //reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
            data.Clear();

        }
    }
}
