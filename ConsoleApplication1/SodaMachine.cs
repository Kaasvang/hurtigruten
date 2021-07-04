using System;
using System.Linq;
using ConsoleApplication1.Inventory;
using ConsoleApplication1.Models;

namespace ConsoleApplication1
{
    public class SodaMachine
    {
        private static IInventoryProvider _inventory;
        private static Money _money;
        
        private static bool _running = true;

        public SodaMachine(IInventoryProvider inventory, Money money)
        {
            _inventory = inventory;
            _money = money;
        }

        public SodaMachine() : this(new InventoryProvider(), new Money())
        {
            
        }

        /// <summary>
        /// This is the starter method for the machine
        /// </summary>
        public void Start()
        {
            while (_running)
            {
                DisplayMenu();

                var input = Console.ReadLine();
                var command = ParseInput(input);

                if (command.Name.Equals("insert")) 
                {
                    Insert(int.Parse(command.Argument));
                }
                
                if (command.Name.Equals("order"))
                {
                    var product = GetProduct(command.Argument);
                    if (product == null) continue;
                    Order(product);
                }
                
                if (command.Name.Equals("sms order"))
                {
                    var product = GetProduct(command.Argument);
                    if (product == null) continue;
                    SmsOrder(product);
                }

                if (command.Name.Equals("recall"))
                {
                    Recall();
                }
            }
        }

        private Command ParseInput(string input)
        {
            var splitInput = input.Split(' ');
            if (splitInput.Length == 3)
            {
                var name = $"{splitInput[0]} {splitInput[1]}";
                var argument = splitInput[2];
                return new Command() {Name = name, Argument = argument};
            }
            if (splitInput.Length == 2)
            {
                var name = splitInput[0];
                var argument = splitInput[1];
                return new Command() {Name = name, Argument = argument};
            }
            else
            {
                var name = splitInput[0];
                return new Command() {Name = name};
            }
        }

        private void Recall()
        {
            //Give money back
            Console.WriteLine($"Returning {_money.Balance} to customer");
            _money.WithdrawAll();
        }

        public void SmsOrder(Soda product)
        {
            if (product.Nr > 0)
            {
                Console.WriteLine($"Giving {product.Name} out");
                product.Nr--;
            }
        }

        public void Order(Soda product)
        {
            // split string on space
            if (_money.Balance >= product.Price && product.Nr > 0)
            {
                Console.WriteLine($"Giving {product.Name} out");
                _money.Withdraw(product.Price);
                Console.WriteLine("Giving " + _money.Balance + " out in change");
                _money.WithdrawAll();
                product.Nr--;
            }
            else if (product.Nr == 0)
            {
                Console.WriteLine($"No {product.Name} left");
            }
            else
            {
                Console.WriteLine("Need " + (product.Price - _money.Balance) + " more");
            }
        }

        private void Insert(int amount)
        {
            //Add to credit
            _money.Insert(amount);
            Console.WriteLine($"Adding {amount} to credit");
        }

        // TODO: (coke, sprite, fanta) should not be hardcoded
        private void DisplayMenu()
        {
            // TODO: show products command 
            Console.WriteLine("\n\nAvailable commands:");
            Console.WriteLine($"insert (money) - Money put into money slot");
            Console.WriteLine("order (coke, sprite, fanta) - Order from machines buttons");
            Console.WriteLine("sms order (coke, sprite, fanta) - Order sent by sms");
            Console.WriteLine("recall - gives money back");
            Console.WriteLine("-------");
            Console.WriteLine("Inserted money: " + _money);
            Console.WriteLine("-------\n\n");
        }

        public Soda GetProduct(string productName)
        {
            return (
                from prod in _inventory.Get()
                where prod.Name == productName
                select prod)
                .FirstOrDefault();
        }
    }
}