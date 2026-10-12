using VyshKat.Interfaces;

namespace VyshKat.Models;

public abstract class Transport : IInventory
{
    public int InventoryNumber { get; }
    public string Name { get; }

    public int EaseOfUse { get; }

    public bool IsWorking { get; }
    protected Transport(int inventoryNumber, string name, int easeOfUse, bool isWorking)
    {
        InventoryNumber = inventoryNumber;
        Name = name;
        if (easeOfUse < 1 || easeOfUse > 10)
        {
            throw new ArgumentOutOfRangeException(nameof(easeOfUse));
        }
        EaseOfUse = easeOfUse;
        IsWorking = isWorking;
    }
    public bool IsSuitableForBeginner()
    {
        return EaseOfUse >= 6;
    }

}
