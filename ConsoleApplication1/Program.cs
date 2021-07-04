using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            SodaMachine sodaMachine = new SodaMachine();
            sodaMachine.Start();
        }
    }

    public class SodaMachine
    {
        private static int _money;
        
        private static bool _running = true;
        private static readonly Soda[] Inventory = 
        {
            new Soda {Name = "coke", Nr = 5},
            new Soda {Name = "sprite", Nr = 3},
            new Soda {Name = "fanta", Nr = 3}
        };

        /// <summary>
        /// This is the starter method for the machine
        /// </summary>
        public void Start()
        {
            while (_running)
            {
                Console.WriteLine("\n\nAvailable commands:");
                Console.WriteLine($"insert (money) - Money put into money slot");
                Console.WriteLine("order (coke, sprite, fanta) - Order from machines buttons");
                Console.WriteLine("sms order (coke, sprite, fanta) - Order sent by sms");
                Console.WriteLine("recall - gives money back");
                Console.WriteLine("-------");
                Console.WriteLine("Inserted money: " + _money);
                Console.WriteLine("-------\n\n");

                var input = Console.ReadLine();
                
                if (input.StartsWith("insert"))
                {
                    //Add to credit
                    _money += int.Parse(input.Split(' ')[1]);
                    Console.WriteLine("Adding " + int.Parse(input.Split(' ')[1]) + " to credit");
                }
                
                if (input.StartsWith("order"))
                {
                    // split string on space
                    var csoda = input.Split(' ')[1];
                    var product = GetProduct(csoda);
                    if (product == null) continue;
                    if (product.Name == csoda && _money > 19 && product.Nr > 0)
                    {
                        Console.WriteLine($"Giving {product.Name} out");
                        _money -= 20;
                        Console.WriteLine("Giving " + _money + " out in change");
                        _money = 0;
                        product.Nr--;
                    }
                    else if (product.Name == csoda && product.Nr == 0)
                    {
                        Console.WriteLine($"No {product.Name} left");
                    }
                    else if (product.Name == csoda)
                    {
                        Console.WriteLine("Need " + (20 - _money) + " more");
                    }
                }
                
                if (input.StartsWith("sms order"))
                {
                    var csoda = input.Split(' ')[2];
                    var product = GetProduct(csoda);
                    if (product == null) continue;
                    if (product.Nr > 0)
                    {
                        Console.WriteLine($"Giving {product.Name} out");
                        product.Nr--;
                    }
                }

                if (input.Equals("recall"))
                {
                    //Give money back
                    Console.WriteLine("Returning " + _money + " to customer");
                    _money = 0;
                }
            }
        }

        private Soda GetProduct(string productName)
        {
            return (
                from prod in Inventory
                where prod.Name == productName
                select prod)
                .FirstOrDefault();
        }
    }
    
    public class Soda
    {
        public string Name { get; set; }
        public int Nr { get; set; }

    }
}
