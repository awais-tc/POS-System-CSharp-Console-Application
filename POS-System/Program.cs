using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace POS_System
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string inventoryFilePath = Path.Combine(desktopPath, "Inventory.txt");
            string salesFilePath = Path.Combine(desktopPath, "SalesReport.txt");

            InventoryManager inventoryManager = new InventoryManager();
            FileHandler fileHandler = new FileHandler();

            // Load Inventory from File
            inventoryManager.Products = await fileHandler.LoadInventoryAsync(inventoryFilePath);

            bool exitApplication = false;
            while (!exitApplication)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the POS System!");

                // Authentication logic...
                UserRole role = GetValidRole();
                User currentUser = role == UserRole.Admin
                    ? new AdminUser("Admin")
                    : new CashierUser("Cashier") as User;

                bool userExit = false;
                while (!userExit)
                {
                    Console.Clear();
                    Console.WriteLine("\nSelect an option:");
                    if (currentUser.Role == UserRole.Admin)
                    {
                        Console.WriteLine("1. Add Product");
                        Console.WriteLine("2. Update Product");
                        Console.WriteLine("3. Delete Product");
                        Console.WriteLine("4. View Sales Report");
                    }
                    else if (currentUser.Role == UserRole.Cashier)
                    {
                        Console.WriteLine("5. Process Sale");
                    }
                    Console.WriteLine("6. Exit to Main Menu");

                    Console.Write("Choice: ");
                    string choice = Console.ReadLine();
                    //perform operation on base of user input
                    switch (choice)
                    {
                        case "1" when currentUser.Role == UserRole.Admin:
                            ((AdminUser)currentUser).AddProduct(inventoryManager.Products);
                            await fileHandler.SaveInventoryAsync(inventoryFilePath, inventoryManager.Products);
                            break;

                        case "2" when currentUser.Role == UserRole.Admin:
                            Product.DisplayInventory(inventoryManager.Products);
                            int updateId = GetValidIntegerInput("Enter product ID to update: ");
                            ((AdminUser)currentUser).UpdateProduct(updateId, inventoryManager.Products);
                            await fileHandler.SaveInventoryAsync(inventoryFilePath, inventoryManager.Products);
                            break;

                        case "3" when currentUser.Role == UserRole.Admin:
                            Product.DisplayInventory(inventoryManager.Products);
                            int deleteId = GetValidIntegerInput("Enter product ID to delete: ");
                            ((AdminUser)currentUser).DeleteProduct(deleteId, inventoryManager.Products);
                            await fileHandler.SaveInventoryAsync(inventoryFilePath, inventoryManager.Products);
                            break;

                        case "4" when currentUser.Role == UserRole.Admin:
                            await ((AdminUser)currentUser).ViewSalesReportAsync(salesFilePath);
                            break;

                        case "5" when currentUser.Role == UserRole.Cashier:
                            Product.DisplayInventory(inventoryManager.Products);
                            ((CashierUser)currentUser).ProcessSale(inventoryManager, salesFilePath);
                            break;

                        case "6":
                            userExit = true;
                            break;

                        default:
                            Console.WriteLine("Invalid option!");
                            break;
                    }

                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        // Get Valid Role with Validation
        static UserRole GetValidRole()
        {
            while (true)
            {
                Console.Write("Enter your role (Admin/Cashier): ");
                string roleInput = Console.ReadLine();

                if (Enum.TryParse(roleInput, true, out UserRole role) &&
                    (role == UserRole.Admin || role == UserRole.Cashier))
                {
                    return role;
                }

                Console.WriteLine("Invalid role. Please enter 'Admin' or 'Cashier'.");
            }
        }

        // Global Integer Input Validator
        public static int GetValidIntegerInput(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (int.TryParse(input, out int result))
                    return result;

                Console.WriteLine("Invalid input! Please enter a valid number.");
            }
        }
    }
}
