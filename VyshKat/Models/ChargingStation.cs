using VyshKat.Interfaces;

namespace VyshKat.Models;

public class ChargingStation : IInventory, IEnergyConsumer
{
    public int InventoryNumber { get; }
    public string Name { get; }

    public double DailyEnergy { get; }

    public ChargingStation(int inventoryNumber, string name, double dailyEnergy)
    {
        InventoryNumber = inventoryNumber;
        Name = name;
        if (dailyEnergy < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(dailyEnergy));
        }
        DailyEnergy = dailyEnergy;
    }
}