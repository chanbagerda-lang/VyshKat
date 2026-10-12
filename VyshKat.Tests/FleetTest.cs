
using VyshKat.Interfaces;
using VyshKat.Models;
using VyshKat.Services;
using Xunit;

namespace VyshKat.Tests;

public class FleetTests
{
    [Fact]
    public void AddTransport_WorkingScooter_IncreasesCount()
    {
        Fleet fleet = new Fleet(new ServiceCenter());
        Scooter scooter = new Scooter(101, "Scooter1", 8, true);

        bool accepted = fleet.AddTransport(scooter);

        Assert.True(accepted);
        Assert.Equal(1, fleet.CountTransport());
    }

    [Fact]
    public void AddTransport_BrokenScooter_DoesNotIncreaseCount()
    {
        Fleet fleet = new Fleet(new ServiceCenter());
        Scooter scooter = new Scooter(101, "Scooter1", 8, false);

        bool accepted = fleet.AddTransport(scooter);

        Assert.False(accepted);
        Assert.Equal(0, fleet.CountTransport());
    }

    [Fact]
    public void TotalEnergy_TransportAndItems_ReturnsSum()
    {
        Fleet fleet = new Fleet(new ServiceCenter());

        fleet.AddTransport(new ElectricScooter(101, "ElectricScooter1", 8, 2.5, true));
        fleet.AddItem(new ChargingStation(102, "Station1", 3.0));
        fleet.AddItem(new Helmet(103, "Helmet1"));

        Assert.Equal(5.5, fleet.TotalEnergy());
    }

    [Fact]
    public void GetBeginnerTransport_MixedEase_ReturnsOnlySuitable()
    {
        Fleet fleet = new Fleet(new ServiceCenter());

        fleet.AddTransport(new Scooter(101, "Scooter1", 5, true));
        fleet.AddTransport(new Bike(102, "Bike1", 6, true));
        fleet.AddTransport(new ElectricScooter(103, "ElectricScooter1", 9, 2.5, true));

        List<Transport> result = fleet.GetBeginnerTransport();

        Assert.Equal(2, result.Count);
        Assert.Contains(result, transport => transport.InventoryNumber == 102);
        Assert.Contains(result, transport => transport.InventoryNumber == 103);
        Assert.DoesNotContain(result, transport => transport.InventoryNumber == 101);
    }

    [Fact]
    public void GetAllInventory_TransportAndItems_ReturnsAll()
    {
        Fleet fleet = new Fleet(new ServiceCenter());

        fleet.AddTransport(new Scooter(101, "Scooter1", 8, true));
        fleet.AddItem(new Helmet(102, "Helmet1"));
        fleet.AddItem(new ChargingStation(103, "Station1", 3.0));

        List<IInventory> result = fleet.GetAllInventory();

        Assert.Equal(3, result.Count);
        Assert.Contains(result, item => item.InventoryNumber == 101);
        Assert.Contains(result, item => item.InventoryNumber == 102);
        Assert.Contains(result, item => item.InventoryNumber == 103);
    }

    [Fact]
    public void AddTransport_DuplicateInventoryNumber_ThrowsException()
    {
        Fleet fleet = new Fleet(new ServiceCenter());

        fleet.AddTransport(new Scooter(101, "Scooter1", 8, true));

        Assert.Throws<ArgumentException>(() =>
            fleet.AddTransport(new Bike(101, "Bike1", 7, true)));

        Assert.Equal(1, fleet.CountTransport());
    }
}
