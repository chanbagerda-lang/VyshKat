using VyshKat.Interfaces;
using VyshKat.Models;

namespace VyshKat.Services;

public class ServiceCenter : IServiceCenter
{
    public bool Inspect(Transport transport)
    {
        return transport.IsWorking;
    }
}


