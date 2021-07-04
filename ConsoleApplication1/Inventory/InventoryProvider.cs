using ConsoleApplication1.Models;

namespace ConsoleApplication1.Inventory
{
    public class InventoryProvider : IInventoryProvider
    {
        public Soda[] Get()
        {
            return new Soda[]
            {
                new Soda {Name = "coke", Nr = 5, Price = 20.0},
                new Soda {Name = "sprite", Nr = 3, Price = 15.0},
                new Soda {Name = "fanta", Nr = 3, Price = 15.0}
            };
        }
    }
}