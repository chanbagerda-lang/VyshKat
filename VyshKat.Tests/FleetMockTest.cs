
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using VyshKat.Interfaces;
using VyshKat.Models;
using VyshKat.Services;
using Xunit;

namespace VyshKat.Tests;

public class FleetMockTests
{
    [Fact]
    public void AddTransport_MockApproves_TransportAdded()
    {
        IServiceCenter serviceCenter = Substitute.For<IServiceCenter>();
        serviceCenter.Inspect(Arg.Any<Transport>()).Returns(true);

        ServiceCollection services = new ServiceCollection();
        services.AddSingleton<IServiceCenter>(serviceCenter);
        services.AddTransient<Fleet>();

        using var provider = services.BuildServiceProvider();
        Fleet fleet = provider.GetRequiredService<Fleet>();

        Scooter scooter = new Scooter(101, "Scooter1", 8, false);

        bool accepted = fleet.AddTransport(scooter);

        Assert.True(accepted);
        Assert.Equal(1, fleet.CountTransport());
        serviceCenter.Received(1).Inspect(scooter);
    }

    [Fact]
    public void AddTransport_MockRejects_TransportNotAdded()
    {
        IServiceCenter serviceCenter = Substitute.For<IServiceCenter>();
        serviceCenter.Inspect(Arg.Any<Transport>()).Returns(false);

        ServiceCollection services = new ServiceCollection();
        services.AddSingleton<IServiceCenter>(serviceCenter);
        services.AddTransient<Fleet>();

        using var provider = services.BuildServiceProvider();
        Fleet fleet = provider.GetRequiredService<Fleet>();

        Scooter scooter = new Scooter(102, "Scooter2", 8, true);

        bool accepted = fleet.AddTransport(scooter);

        Assert.False(accepted);
        Assert.Equal(0, fleet.CountTransport());
        serviceCenter.Received(1).Inspect(scooter);
    }
}
