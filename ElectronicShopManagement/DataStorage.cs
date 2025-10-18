using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace ElectronicShopManagement
{
    public static class DataStorage
    {
        
        private static readonly string productFile = "products.json";
        private static readonly string salesFile = "sales.json";

       
        private static List<ProductsModel> _products;
        private static List<SaleHistory> _sales;

     
        public static event Action ProductsChanged;
        public static event Action HistoryChanged;

        private static List<ProductsModel> Products
        {
            get
            {
                if (_products == null)
                    _products = LoadProducts();
                return _products;
            }
        }

        private static List<SaleHistory> Sales
        {
            get
            {
                if (_sales == null)
                    _sales = LoadSales();
                return _sales;
            }
        }

        // ----------------------------- PRODUCT -----------------------------

        public static List<ProductsModel> GetAllProducts() => Products.ToList();

        public static ProductsModel GetById(string id) =>
            Products.FirstOrDefault(p => p.ProductID.Equals(id, StringComparison.OrdinalIgnoreCase));

        public static void SaveProducts(List<ProductsModel> products)
        {
            try
            {
                string json = JsonConvert.SerializeObject(products, Formatting.Indented);
                File.WriteAllText(productFile, json);
                _products = products;
                ProductsChanged?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data: {ex.Message}", "Error");
            }
        }

        public static List<ProductsModel> LoadProducts()
        {
            try
            {
                if (File.Exists(productFile))
                {
                    string json = File.ReadAllText(productFile);
                    var savedProducts = JsonConvert.DeserializeObject<List<ProductsModel>>(json);
                    if (savedProducts != null && savedProducts.Count > 0)
                        return savedProducts;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading saved data: {ex.Message}. Loading default products.", "Warning");
            }
            return ProductData.GetProducts();
        }

        public static void UpdateProduct(ProductsModel product)
        {
            var existing = GetById(product.ProductID);
            if (existing != null)
            {
                existing.ProductName = product.ProductName;
                existing.Category = product.Category;
                existing.Price = product.Price;
                existing.StockQuantity = product.StockQuantity;
                SaveProducts(Products);
            }
        }

        public static void DecreaseStock(string id, int qty)
        {
            var p = GetById(id);
            if (p != null)
            {
                p.StockQuantity = Math.Max(0, p.StockQuantity - qty);
                SaveProducts(Products);
            }
        }

        public static void ResetToDefault()
        {
            try
            {
                if (File.Exists(productFile))
                    File.Delete(productFile);
                _products = ProductData.GetProducts();
                SaveProducts(_products);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error resetting data: {ex.Message}", "Error");
            }
        }

        // ----------------------------- SALES HISTORY -----------------------------

        public class SaleHistory
        {
            public DateTime SoldAt { get; set; }
            public string ProductID { get; set; }
            public string ProductName { get; set; }
            public string Category { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
            public decimal Amount { get; set; }
        }

        public static List<SaleHistory> GetAllHistory() => Sales.ToList();

        public static void AddFromCart(List<ProductForSellModel> cart)
        {
            if (cart == null || cart.Count == 0) return;

            foreach (var item in cart)
            {
                Sales.Add(new SaleHistory
                {
                    SoldAt = DateTime.Now,
                    ProductID = item.ID,
                    ProductName = item.Name,
                    Category = item.Categories,
                    Price = item.Prices,
                    Quantity = item.SellQty,
                    Amount = item.Prices * item.SellQty
                });
            }

            SaveSales();
            HistoryChanged?.Invoke();
        }

        private static List<SaleHistory> LoadSales()
        {
            try
            {
                if (File.Exists(salesFile))
                {
                    string json = File.ReadAllText(salesFile);
                    var sales = JsonConvert.DeserializeObject<List<SaleHistory>>(json);
                    if (sales != null)
                        return sales;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading sales: {ex.Message}", "Warning");
            }
            return new List<SaleHistory>();
        }

        private static void SaveSales()
        {
            try
            {
                string json = JsonConvert.SerializeObject(Sales, Formatting.Indented);
                File.WriteAllText(salesFile, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving sales: {ex.Message}", "Error");
            }
        }
    }
}
