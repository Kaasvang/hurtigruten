using ConsoleApplication1.Inventory;
using ConsoleApplication1.Models;

namespace ConsoleApplication1.Tests.Inventory
{
    public class InventoryProviderTest : IInventoryProvider
    {
        private readonly Soda[] _product;
        public Soda[] Get() => _product;
        public InventoryProviderTest(Soda[] soda) => _product = soda;
    }
}