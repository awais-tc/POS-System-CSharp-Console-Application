using System;
using System.Collections.Generic;

namespace POS_System
{
    public class Sale
    {
        private static int _lastSaleId = 0;

        public int SaleId { get; private set; }
        public List<(Product Product, int Quantity)> Items { get; }
        public double TotalAmount { get; private set; }

        public Sale()
        {
            SaleId = ++_lastSaleId;
            Items = new List<(Product, int)>();
        }

        public void AddItem(Product product, int quantity)
        {
            if (product.StockQuantity >= quantity)
            {
                product.StockQuantity -= quantity;
                Items.Add((product, quantity));
                TotalAmount += product.Price * quantity;
            }
            else
            {
                Console.WriteLine($"Insufficient stock for {product.Name}.");
            }
        }

        public override string ToString()
        {
            return $"Sale ID: {SaleId}, Total Amount: {TotalAmount:C}";
        }
    }
}