using ConsoleApplication1.Models;

namespace ConsoleApplication1.Inventory
{
    public interface IInventoryProvider
    {
        Soda[] Get();
    }
}