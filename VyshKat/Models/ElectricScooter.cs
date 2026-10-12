using VyshKat.Interfaces;
namespace VyshKat.Models;

public class ElectricScooter : Transport, IEnergyConsumer
{
    public double DailyEnergy { get; }
    public ElectricScooter(int inventoryNumber, string name, int easeOfUse, double dailyEnergy, bool isWorking) : base(inventoryNumber, name, easeOfUse, isWorking)
    {
        if (dailyEnergy < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(dailyEnergy));
        }
        DailyEnergy = dailyEnergy;
    }

}
