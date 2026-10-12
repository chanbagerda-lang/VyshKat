using VyshKat.Models;
using Xunit;

namespace VyshKat.Tests;

public class TransportTest
{
    [Fact]
    public void IsSuitableForBeginnersHighEase()
    {
        Scooter scooter = new Scooter(100, "Scooter1", 8, true);

        bool result = scooter.IsSuitableForBeginner();

        Assert.True(result);
    }

    [Fact]
    public void IsSuitableForBeginnerLowEase()
    {
        Scooter scooter = new Scooter(102, "Scooter2", 4, true);


        bool result = scooter.IsSuitableForBeginner();


        Assert.False(result);
    }

    [Theory]
    [InlineData(1, false)]
    [InlineData(5, false)]
    [InlineData(6, true)]
    [InlineData(8, true)]
    [InlineData(10, true)]
    public void IsSuitableForBeginner_DifferentEase(
    int easeOfUse, bool expected)
    {

        Scooter scooter = new Scooter(103, "Scooter3", easeOfUse, true);


        bool result = scooter.IsSuitableForBeginner();


        Assert.Equal(expected, result);
    }

    [Fact]
    public void Constructor_ValidValues_StoresProperties()
    {
        Scooter scooter = new Scooter(101, "Scooter1", 8, true);

        Assert.Equal(101, scooter.InventoryNumber);
        Assert.Equal("Scooter1", scooter.Name);
        Assert.Equal(8, scooter.EaseOfUse);
        Assert.True(scooter.IsWorking);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(11)]
    public void Constructor_InvalidEase_ThrowsException(int easeOfUse)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new Scooter(101, "Scooter1", easeOfUse, true));
    }

    [Fact]
    public void ElectricScooter_ValidEnergy_StoresValue()
    {
        ElectricScooter scooter = new ElectricScooter(105, "ElectricScooter1", 8, 2.5, true);

        Assert.Equal(2.5, scooter.DailyEnergy);
    }
    [Theory]
    [InlineData(-1)]
    [InlineData(-2.5)]
    public void ElectricScooter_NegativeEnergy_ThrowsException(double dailyEnergy)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new ElectricScooter(105, "ElectricScooter1", 8, dailyEnergy, true));
    }
}
