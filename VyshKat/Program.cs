using Microsoft.Extensions.DependencyInjection;
using VyshKat.Interfaces;
using VyshKat.Models;
using VyshKat.Services;

ServiceCollection services = new ServiceCollection { };
services.AddTransient<IServiceCenter, ServiceCenter>();
services.AddTransient<Fleet>();
using var provider = services.BuildServiceProvider();
var fleet = provider.GetRequiredService<Fleet>();

while (true)
{
    Console.WriteLine("\nVyshKat");
    Console.WriteLine("1.Add transport");
    Console.WriteLine("2.Add inventory item");
    Console.WriteLine("3.Show transport count");
    Console.WriteLine("4.Show total energy");
    Console.WriteLine("5.Show beginner-friendly transport");
    Console.WriteLine("6.Show all inventory");
    Console.WriteLine("0.Exit");

    Console.Write("Choose an option: ");
    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            {
                Console.WriteLine("\nChoose transport type:");
                Console.WriteLine("1. Scooter");
                Console.WriteLine("2. ElectricScooter");
                Console.WriteLine("3. Bike");

                Console.Write("Type: ");
                string? type = Console.ReadLine();

                if (type != "1" && type != "2" && type != "3")
                {
                    Console.WriteLine("Invalid transport type");
                    break;
                }

                Console.Write("Inventory number: ");
                bool validNumber = int.TryParse(Console.ReadLine(), out int inventoryNumber);

                Console.Write("Name: ");
                string name = Console.ReadLine() ?? "";

                Console.Write("Ease of use (1-10): ");
                bool validEase = int.TryParse(Console.ReadLine(), out int easeOfUse);

                Console.Write("Is working (true/false): ");
                bool validWorking = bool.TryParse(Console.ReadLine(), out bool isWorking);

                if (!validNumber || !validEase || !validWorking ||
                    easeOfUse < 1 || easeOfUse > 10 || string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Invalid input");
                    break;
                }

                double dailyEnergy = 0;

                if (type == "2")
                {
                    Console.Write("Daily energy (kWh): ");
                    bool validEnergy = double.TryParse(Console.ReadLine(), out dailyEnergy);

                    if (!validEnergy || double.IsNaN(dailyEnergy) ||
                        double.IsInfinity(dailyEnergy) || dailyEnergy < 0)
                    {
                        Console.WriteLine("Invalid energy value");
                        break;
                    }
                }

                Transport transport;

                if (type == "1")
                {
                    transport = new Scooter(inventoryNumber, name, easeOfUse, isWorking);
                }
                else if (type == "2")
                {
                    transport = new ElectricScooter(inventoryNumber, name, easeOfUse, dailyEnergy, isWorking);
                }
                else
                {
                    transport = new Bike(inventoryNumber, name, easeOfUse, isWorking);
                }

                try
                {
                    bool accepted = fleet.AddTransport(transport);
                    Console.WriteLine($"{name} inspection: {(accepted ? "Accepted" : "Rejected")}");
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Inventory number already exists");
                }

                break;
            }
        case "2":
            {
                Console.WriteLine("\nChoose item type:");
                Console.WriteLine("1. Helmet");
                Console.WriteLine("2. ChargingStation");

                Console.Write("Type: ");
                string? type = Console.ReadLine();

                if (type != "1" && type != "2")
                {
                    Console.WriteLine("Invalid item type");
                    break;
                }

                Console.Write("Inventory number: ");
                bool validNumber = int.TryParse(Console.ReadLine(), out int inventoryNumber);

                Console.Write("Name: ");
                string name = Console.ReadLine() ?? "";

                if (!validNumber || string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Invalid input");
                    break;
                }

                double dailyEnergy = 0;

                if (type == "2")
                {
                    Console.Write("Daily energy (kWh): ");
                    bool validEnergy = double.TryParse(Console.ReadLine(), out dailyEnergy);

                    if (!validEnergy || double.IsNaN(dailyEnergy) ||
                        double.IsInfinity(dailyEnergy) || dailyEnergy < 0)
                    {
                        Console.WriteLine("Invalid energy value");
                        break;
                    }
                }

                IInventory item;

                if (type == "1")
                {
                    item = new Helmet(inventoryNumber, name);
                }
                else
                {
                    item = new ChargingStation(inventoryNumber, name, dailyEnergy);
                }

                try
                {
                    fleet.AddItem(item);
                    Console.WriteLine($"{name} added successfully");
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Inventory number already exists");
                }

                break;
            }

        case "3":
            Console.WriteLine($"Transport count: {fleet.CountTransport()}");
            break;

        case "4":
            Console.WriteLine($"Total daily energy: {fleet.TotalEnergy()} kWh");
            break;

        case "5":
            Console.WriteLine("\nBeginner-friendly transport:");
            foreach (Transport transport in fleet.GetBeginnerTransport())
            {
                Console.WriteLine($"{transport.Name} : {transport.EaseOfUse}");
            }
            break;

        case "6":
            Console.WriteLine("\nInventory:");
            foreach (IInventory inventory in fleet.GetAllInventory())
            {
                Console.WriteLine($"{inventory.Name} : {inventory.InventoryNumber}");
            }
            break;

        case "0":
            return;

        default:
            Console.WriteLine("Invalid option");
            break;
    }
}
