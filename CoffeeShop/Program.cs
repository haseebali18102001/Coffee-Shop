using System;
using System.Collections.Generic;

namespace CoffeeShopApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize services and start the coffee shop application.
            var menuService = new MenuService();
            var orderService = new OrderService();
            var coffeeShop = new CoffeeShop(menuService, orderService);
            coffeeShop.Start();
        }
    }

    // Main class to manage coffee shop operations.
    class CoffeeShop
    {
        private readonly MenuService _menuService;  // Service to manage menu operations.
        private readonly OrderService _orderService; // Service to manage order operations.

        // Constructor to inject dependencies.
        public CoffeeShop(MenuService menuService, OrderService orderService)
        {
            _menuService = menuService;
            _orderService = orderService;
        }

        // Entry point for the application logic.
        public void Start()
        {
            Console.WriteLine("Welcome to the Coffee Shop!");
            while (true)
            {
                // Display menu options for user interaction.
                Console.WriteLine("\n1. Show Menu");
                Console.WriteLine("2. Place Order");
                Console.WriteLine("3. View Orders");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                // Handle user input.
                switch (choice)
                {
                    case "1":
                        _menuService.DisplayMenu();
                        break;
                    case "2":
                        PlaceOrder();
                        break;
                    case "3":
                        _orderService.DisplayOrders();
                        break;
                    case "4":
                        Console.WriteLine("Thank you for visiting!");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        // Handles placing an order by interacting with menu and order services.
        private void PlaceOrder()
        {
            _menuService.DisplayMenu();
            Console.Write("Enter the number of the item you'd like to order: ");
            if (int.TryParse(Console.ReadLine(), out int itemIndex))
            {
                _orderService.PlaceOrder(itemIndex, _menuService.GetMenu());
            }
            else
            {
                Console.WriteLine("Invalid selection.");
            }
        }
    }

    // Service to manage the menu.
    class MenuService
    {
        private readonly List<MenuItem> _menu;

        // Constructor to initialize the menu.
        public MenuService()
        {
            _menu = new List<MenuItem>
            {
                new MenuItem("Espresso", 3.00),
                new MenuItem("Cappuccino", 4.50),
                new MenuItem("Latte", 4.00),
                new MenuItem("Mocha", 4.75),
                new MenuItem("Tea", 2.50)
            };
        }

        // Returns the menu items.
        public List<MenuItem> GetMenu() => _menu;

        // Displays the menu items to the console.
        public void DisplayMenu()
        {
            Console.WriteLine("\nMenu:");
            for (int i = 0; i < _menu.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_menu[i].Name} - ${_menu[i].Price}");
            }
        }
    }

    // Service to manage orders.
    class OrderService
    {
        private readonly List<Order> _orders = new(); // List to store orders.

        // Adds a new order to the list.
        public void PlaceOrder(int itemIndex, List<MenuItem> menu)
        {
            if (itemIndex > 0 && itemIndex <= menu.Count)
            {
                var selectedItem = menu[itemIndex - 1];
                _orders.Add(new Order(selectedItem));
                Console.WriteLine($"You ordered: {selectedItem.Name}");
            }
            else
            {
                Console.WriteLine("Invalid selection.");
            }
        }

        // Displays the list of placed orders.
        public void DisplayOrders()
        {
            if (_orders.Count == 0)
            {
                Console.WriteLine("\nNo orders placed yet.");
                return;
            }

            Console.WriteLine("\nOrders:");
            foreach (var order in _orders)
            {
                Console.WriteLine($"- {order.Item.Name} (${order.Item.Price})");
            }
        }
    }
    // Represents a menu item.
    class MenuItem
    {
        public string Name { get; }  // Name of the item.
        public double Price { get; } // Price of the item.
        public MenuItem(string name, double price)
        {
            Name = name;
            Price = price;
        }
    }
    // Represents an order placed by the user.
    class Order
    {
        public MenuItem Item { get; } // Ordered menu item.

        public Order(MenuItem item)
        {
            Item = item;
        }
    }
}
