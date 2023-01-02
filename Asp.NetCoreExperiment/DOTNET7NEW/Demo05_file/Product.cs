using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo05_file
{
    public class Product
    {
        public string? Name { get; set; }
        public decimal PurchasePrice { get; set; }
        public void PrintSalesProduct()
        {
            var salesPrice = new SalesPrice
            {
                RetailPrice = PurchasePrice * 1.5m,
                WholesalePrice = PurchasePrice * 1.2m
            };
            Console.WriteLine($"Name:{Name},{salesPrice}");
        }
    }
    file record SalesPrice
    {
        public decimal RetailPrice { get; set; }
        public decimal WholesalePrice { get; set; }
    }   
}
