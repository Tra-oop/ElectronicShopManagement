using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicShopManagement
{
    public class ProductForSellModel
    {
        public string Categories { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public int SellQty { get; set; }
        public decimal Prices { get; set; }
        public decimal Amount { get; set; }
        public decimal totalAmount { get; set; }
        public string date {  get; set; }
        public string Cashier { get; set; } 

    }
}
