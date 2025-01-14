using System.Collections.Generic;
using System.Linq;

namespace POS_System
{
    public class InventoryManager
    {
        public List<Product> Products { get; set; } = new List<Product>();

        public Product GetProductById(int productId)
        {
            return Products.FirstOrDefault(p => p.ProductId == productId);
        }
    }
}