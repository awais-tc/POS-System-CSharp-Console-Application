using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace POS_System
{
    public class AdminUser : User
    {
        public AdminUser(string name) : base(name, UserRole.Admin) { }

        

        public void UpdateProduct(int productId, List<Product> inventory)
        {
            var product = inventory.FirstOrDefault(p => p.ProductId == productId);

            if (product == null)
            {
                Console.WriteLine("Product not found!");
                return;
            }

            Console.WriteLine("Updating product details. Press Enter to skip any field.");

            Console.Write("Enter new name (letters only): ");
            string newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName) && newName.All(char.IsLetter))
            {
                product.Name = newName;
            }
            else if (!string.IsNullOrWhiteSpace(newName))
            {
                Console.WriteLine("Invalid input! Product name must contain letters only.");
            }

            Console.Write("Enter new price: ");
            string newPriceInput = Console.ReadLine();
            if (double.TryParse(newPriceInput, out double newPrice) && newPrice > 0)
            {
                product.Price = newPrice;
            }
            else if (!string.IsNullOrWhiteSpace(newPriceInput))
            {
                Console.WriteLine("Invalid input! Price must be a positive number.");
            }

            Console.Write("Enter new stock quantity: ");
            string newStockInput = Console.ReadLine();
            if (int.TryParse(newStockInput, out int newStock) && newStock > 0)
            {
                product.StockQuantity = newStock;
            }
            else if (!string.IsNullOrWhiteSpace(newStockInput))
            {
                Console.WriteLine("Invalid input! Stock quantity must be a positive number.");
            }

            Console.WriteLine("Product updated successfully!");
        }

        public async Task ViewSalesReportAsync(string filePath)
        {
            if (File.Exists(filePath))
            {
                var report = await File.ReadAllLinesAsync(filePath);
                Console.WriteLine("Sales Report:");
                foreach (var line in report)
                {
                    Console.WriteLine(line);
                }
            }
            else
            {
                Console.WriteLine("No sales report available.");
            }
        }

        

        public void AddProduct(List<Product> inventory)
        {
            string name;
            double price;
            int stock;

            while (true)
            {
                Console.Write("Enter product name (letters only): ");
                name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name) && name.All(char.IsLetter))
                    break;

                Console.WriteLine("Invalid input! Product name must contain letters only and cannot be empty.");
            }

            while (true)
            {
                Console.Write("Enter price: ");
                if (double.TryParse(Console.ReadLine(), out price) && price > 0)
                    break;

                Console.WriteLine("Invalid input! Price must be a positive number.");
            }

            while (true)
            {
                Console.Write("Enter stock quantity: ");
                if (int.TryParse(Console.ReadLine(), out stock) && stock > 0)
                    break;

                Console.WriteLine("Invalid input! Stock quantity must be a positive number.");
            }

            Console.Write("Enter barcode: ");
            string barcode = Console.ReadLine();

            inventory.Add(new Product
            {
                ProductId = inventory.Count + 1,
                Name = name,
                Price = price,
                StockQuantity = stock,
                Barcode = new Barcode(barcode)
            });

            Console.WriteLine("Product added successfully!");
        }

        public void DeleteProduct(int productId, List<Product> inventory)
        {
            var product = inventory.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                inventory.Remove(product);
                Console.WriteLine("Product deleted successfully!");
            }
            else
            {
                Console.WriteLine("Product not found!");
            }
        }
    }
}
