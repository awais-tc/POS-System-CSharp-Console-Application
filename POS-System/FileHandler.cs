using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_System
{
    public class FileHandler
    {
        public async Task<List<Product>> LoadInventoryAsync(string filePath)
        {
            var products = new List<Product>();

            if (File.Exists(filePath))
            {
                var lines = await File.ReadAllLinesAsync(filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');

                    if (parts.Length >= 5 &&
                        int.TryParse(parts[0], out int id) &&
                        double.TryParse(parts[2], out double price) &&
                        int.TryParse(parts[3], out int stock))
                    {
                        products.Add(new Product
                        {
                            ProductId = id,
                            Name = parts[1],
                            Price = price,
                            StockQuantity = stock,
                            Barcode = new Barcode(parts[4])
                        });
                    }
                }
            }

            return products;
        }

        public async Task SaveInventoryAsync(string filePath, List<Product> products)
        {
            var lines = products.Select(p => $"{p.ProductId},{p.Name},{p.Price},{p.StockQuantity},{p.Barcode.Code}");
            await File.WriteAllLinesAsync(filePath, lines);
        }
    }
}