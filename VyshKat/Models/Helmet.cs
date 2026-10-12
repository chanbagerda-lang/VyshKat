using VyshKat.Interfaces;

namespace VyshKat.Models;

public class Helmet : IInventory
{
    public int InventoryNumber { get; }
    public string Name { get; }
    public Helmet(int inventoryNumber, string name)
    {
        InventoryNumber = inventoryNumber;
        Name = name;
    }

}
