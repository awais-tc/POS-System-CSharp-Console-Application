using System;
using System.Collections.Generic;
using System.IO;

namespace POS_System
{
    public class CashierUser : User
    {
        public CashierUser(string name) : base(name, UserRole.Cashier) { }

        public void ProcessSale(InventoryManager inventoryManager, string salesFilePath)
        {
            int productId = Program.GetValidIntegerInput("Enter product ID: ");
            int quantity = Program.GetValidIntegerInput("Enter quantity: ");

            var product = inventoryManager.GetProductById(productId);

            if (product == null || product.StockQuantity < quantity)
            {
                Console.WriteLine("Insufficient stock or product not found.");
                return;
            }

            product.StockQuantity -= quantity;

            string saleEntry = $"{DateTime.Now}: Sold {quantity} of {product.Name} at {product.Price * quantity}";
            File.AppendAllText(salesFilePath, saleEntry + Environment.NewLine);

            Console.WriteLine("Sale processed successfully!");
        }
    }
}