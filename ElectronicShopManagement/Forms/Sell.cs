using kh.gov.nbc.bakong_khqr.model;
using kh.gov.nbc.bakong_khqr;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using ElectronicShopManagement;


namespace ElectronicShopManagement.Forms
{
    public partial class Sell : Form
    {
        public static List<ProductForSellModel> products = new List<ProductForSellModel>();
        

        List<ProductForSellModel> products = new List<ProductForSellModel>();
        ProductForSellModel productsModel = new ProductForSellModel();

        public Sell()
        {
            InitializeComponent();

            
            var categories = DataStorage.GetAllProducts()
                               .Select(p => p.Category)
                               .Distinct()
                               .ToList();

            comboboxsell.DataSource = categories;

            comboboxsell.SelectedIndexChanged -= comboboxsell_SelectedIndexChanged;
            comboboxsell.SelectedIndexChanged += comboboxsell_SelectedIndexChanged;
        }

        // Add To Cart
        private void button1_Click(object sender, EventArgs e)
        {
            if (combocashier.SelectedItem == null)
            {
                MessageBox.Show("Please select Cashier name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (comboboxsell.SelectedItem == null || comboboxsellproname.SelectedItem == null)
                return;

            if (!decimal.TryParse(txtsellprice.Text, out var price)) return;
            if (!int.TryParse(txtsellqty.Text, out var qty)) return;

            ProductForSellModel productsModel = new ProductForSellModel()
            {
                Categories = comboboxsell.SelectedItem.ToString(),
                ID = comboboxsellproname.SelectedValue.ToString(),
                Cashier=combocashier.SelectedItem.ToString(),
                Name = comboboxsellproname.Text,
                Prices = Convert.ToDecimal(txtsellprice.Text),
                SellQty = Convert.ToInt32(txtsellqty.Text),
                date = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"),

                Prices = price,
                SellQty = qty,
            };
            
            products.Add(productsModel);

            productsModel.Amount = productsModel.Prices * productsModel.SellQty;
            productsModel.totalAmount = products.Sum(p => p.Prices * p.SellQty);
            txtamount.Text = productsModel.totalAmount.ToString("0.00");

            txtsellqty.Text = "1";

           

            tblshowproductsell.DataSource = null;
            tblshowproductsell.DataSource = products;
            tblshowproductsell.Columns["totalAmount"].Visible = false;
            tblshowproductsell.Columns["ID"].Visible = false;
            tblshowproductsell.Columns["date"].Visible = false;
            tblshowproductsell.Columns["Cashier"].Visible = false;

        }

        
        private async Task CheckPaymentAsync(string md5)
        {
            var client = new HttpClient();
            var url = "https://api-bakong.nbc.gov.kh/v1/check_transaction_by_md5";
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJkYXRhIjp7ImlkIjoiZTI4NzY2YjAxMDA4NGI2ZiJ9LCJpYXQiOjE3NTg3ODg2MjUsImV4cCI6MTc2NjU2NDYyNX0.rfqbEiwVn-VzpxTecK7lHea20c-Bv48T2kkuC0N0Mjc";
            var startTime = DateTime.Now;
            var timeout = TimeSpan.FromMinutes(1);


            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            while (DateTime.Now - startTime < timeout)
            for (int i = 0; i < 10; i++)
            {
                var json = $"{{ \"md5\": \"{md5}\" }}";
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();
                //Console.WriteLine($"[{DateTime.Now}] Response: {result}");

                var response4 = await client.PostAsync(url2, content);
                var result = await response4.Content.ReadAsStringAsync();

                if (result.Contains("Success"))
                {
                    var dialogResult = MessageBox.Show(
                        "Payment Successful!",
                        "Bakong KHQR",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                        
                    );
                    
                    if (dialogResult == DialogResult.OK)
                     {
                        Invoice invoice = new Invoice();
                        invoice.Show();

                        foreach (Form f in Application.OpenForms)
                        {
                        if (f is BakongKhqr)
                        {
                            f.Close();
                            break;
                        }
                    }
                        //products.Clear();
                    {
                      
                        DataStorage.AddFromCart(products);

                        
                        foreach (var item in products)
                        {
                            DataStorage.DecreaseStock(item.ID, item.SellQty);
                        }

                        
                        products.Clear();
                        tblshowproductsell.DataSource = null;
                        tblshowproductsell.DataSource = products; // rebind
                        tblshowproductsell.Columns["totalAmount"].Visible = false;
                        tblshowproductsell.Columns["ID"].Visible = false;
                        tblshowproductsell.Columns["date"].Visible = false;
                        tblshowproductsell.Columns["Cashier"].Visible = false;

                        tblshowproductsell.DataSource = products;
                        if (tblshowproductsell.Columns.Contains("totalAmount"))
                            tblshowproductsell.Columns["totalAmount"].Visible = false;
                        if (tblshowproductsell.Columns.Contains("ID"))
                            tblshowproductsell.Columns["ID"].Visible = false;

                        txtamount.Text = "0.00";
                    }
                    break;
                }

                await Task.Delay(5000); // wait 5 seconds
            }
           
        }
                await Task.Delay(5000);
            }
        }

        
        private async void btnpayment_Click(object sender, EventArgs e)
        {
            decimal totalAmount = products.Sum(p => p.Prices * p.SellQty);
            if (totalAmount <= 0) return;



            DateTime now = DateTime.UtcNow;
            long millisecondsSinceEpoch = (long)(now - new DateTime(1970, 1, 1)).TotalMilliseconds;
            long expirationTimestamp = millisecondsSinceEpoch + 3600000;

            var response = BakongKHQR.GenerateIndividual(
                new IndividualInfo
                {
                    BakongAccountID = "khoeurn_rotheany1@aclb",
                    MerchantName = "Rotheany Khoeurn",
                    Currency = KHQRCurrency.KHR,
                    Amount = Convert.ToInt32(totalAmount),
                    MerchantCity = "PHNOM PENH",
                    BillNumber = "#12345",
                    MobileNumber = "855969884476",
                    StoreLabel = "Electronic Shop",
                    TerminalLabel = "Cashier_1",
                    ExpirationTimestamp = expirationTimestamp
                }
            );

            string qrText = response.Data.QR;
            string md5 = response.Data.MD5;

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(10);

            var bakong = new BakongKhqr(qrCodeImage, totalAmount);
            bakong.Show();

            await CheckPaymentAsync(md5);
        }

        
        private void comboboxsellproname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboboxsellproname.SelectedItem is ProductsModel selectedProduct)
            {
                txtsellprice.Text = selectedProduct.Price.ToString("0.00");
            }
        }

       
        private void comboboxsell_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboboxsell.SelectedItem == null) return;

            string selectedName = comboboxsell.SelectedItem.ToString();

            var prods = DataStorage.GetAllProducts()
                                   .Where(p => p.Category == selectedName)
                                   .ToList();

            comboboxsellproname.DisplayMember = "ProductName";
            comboboxsellproname.ValueMember = "ProductID";
            comboboxsellproname.DataSource = prods;

            comboboxsellproname.SelectedIndexChanged -= comboboxsellproname_SelectedIndexChanged;
            comboboxsellproname.SelectedIndexChanged += comboboxsellproname_SelectedIndexChanged;
        }

        // Cancel Product
        private void button2_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Are you sure to cancel this item?", "Confirmation",
                                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            if (tblshowproductsell.CurrentRow != null)
            {
                var selectedProduct = tblshowproductsell.CurrentRow.DataBoundItem as ProductForSellModel;
                if (selectedProduct != null)
                {
                    products.Remove(selectedProduct);

                    tblshowproductsell.DataSource = null;
                    tblshowproductsell.DataSource = products;
                    tblshowproductsell.Columns["totalAmount"].Visible = false;
                    tblshowproductsell.Columns["ID"].Visible = false;
                    tblshowproductsell.Columns["date"].Visible = false;
                    tblshowproductsell.Columns["Cashier"].Visible = false;

                    if (tblshowproductsell.Columns.Contains("totalAmount"))
                        tblshowproductsell.Columns["totalAmount"].Visible = false;
                    if (tblshowproductsell.Columns.Contains("ID"))
                        tblshowproductsell.Columns["ID"].Visible = false;
                }
            }

            txtamount.Text = products.Count == 0
                ? "0.00"
                : products.Sum(p => p.Amount).ToString("0.00");
        }

        private void Sell_Load(object sender, EventArgs e)
        {
        }

        private void Sell_Load(object sender, EventArgs e)
        {

        }

        private void txtamount_Click(object sender, EventArgs e)
        {

        }
    }
}
