using VyshKat.Interfaces;
using VyshKat.Models;

namespace VyshKat.Services;

public class Fleet
{
    private readonly List<Transport> _transports = new List<Transport> { };
    private readonly List<IInventory> _items = new List<IInventory> { };
    private readonly IServiceCenter _serviceCenter;

    private bool IsInventoryNumberTaken(int number)
    {
        foreach (Transport transport in _transports)
        {
            if (transport.InventoryNumber == number)
            {
                return true;
            }
        }
        foreach (IInventory inventory in _items)
        {
            if (inventory.InventoryNumber == number)
            {
                return true;
            }
        }
        return false;
    }


    public Fleet(IServiceCenter serviceCenter)
    {
        _serviceCenter = serviceCenter;
    }
    public bool AddTransport(Transport transport)
    {
        if (IsInventoryNumberTaken(transport.InventoryNumber))
        {
            throw new ArgumentException("Инвентарный номер уже существует");
        }
        if (_serviceCenter.Inspect(transport))
        {
            _transports.Add(transport);
            return true;
        }
        return false;
    }
    public void AddItem(IInventory inventory)
    {
        if (IsInventoryNumberTaken(inventory.InventoryNumber))
        {
            throw new ArgumentException("Инвентарный номер уже существует");
        }
        if (inventory is Transport)
        {
            throw new ArgumentException();
        }
        _items.Add(inventory);
    }
    public int CountTransport()
    {
        return _transports.Count;
    }
    public double TotalEnergy()
    {
        double sum = 0;
        foreach (IInventory inventory in _items)
        {
            if (inventory is IEnergyConsumer consumer)
            {
                sum += consumer.DailyEnergy;
            }
        }
        foreach (Transport transport in _transports)
        {
            if (transport is IEnergyConsumer consumer)
            {
                sum += consumer.DailyEnergy;
            }
        }
        return sum;
    }
    public List<Transport> GetBeginnerTransport()
    {
        List<Transport> beginnerTransport = new List<Transport> { };
        foreach (Transport transport in _transports)
        {
            if (transport.IsSuitableForBeginner())
            {
                beginnerTransport.Add(transport);
            }
        }
        return beginnerTransport;
    }
    public List<IInventory> GetAllInventory()
    {
        List<IInventory> allItems = new List<IInventory> { };
        foreach (Transport transport in _transports)
        {
            allItems.Add(transport);
        }
        foreach (IInventory inventory in _items)
        {
            allItems.Add(inventory);
        }
        return allItems;
    }
}


