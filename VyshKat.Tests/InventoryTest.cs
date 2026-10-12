using VyshKat.Models;
using Xunit;

namespace VyshKat.Tests;

public class InventoryTests
{
    [Fact]
    public void Helmet_Constructor_StoresProperties()
    {
        Helmet helmet = new Helmet(101, "Helmet1");

        Assert.Equal(101, helmet.InventoryNumber);
        Assert.Equal("Helmet1", helmet.Name);
    }

    [Fact]
    public void ChargingStation_Constructor_StoresProperties()
    {
        ChargingStation station = new ChargingStation(102, "Station1", 3.5);

        Assert.Equal(102, station.InventoryNumber);
        Assert.Equal("Station1", station.Name);
        Assert.Equal(3.5, station.DailyEnergy);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-3.5)]
    public void ChargingStation_NegativeEnergy_ThrowsException(double dailyEnergy)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new ChargingStation(102, "Station1", dailyEnergy));
    }
}