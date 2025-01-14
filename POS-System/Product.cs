namespace POS_System
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int StockQuantity { get; set; }
        public Barcode Barcode { get; set; }

        public static void DisplayInventory(List<Product> products)
        {
            Console.WriteLine("\nInventory:");
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.ProductId}, Name: {product.Name}, Price: {product.Price}, Stock: {product.StockQuantity}, Barcode: {product.Barcode.Code}");
            }
        }
    }

    public class Barcode
    {
        public string Code { get; }

        public Barcode(string code)
        {
            Code = code;
        }
    }
}