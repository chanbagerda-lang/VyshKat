using VyshKat.Models;

namespace VyshKat.Interfaces;

public interface IServiceCenter
{
    public bool Inspect(Transport transport);
}