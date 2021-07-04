using System;
using ConsoleApplication1.Models;
using ConsoleApplication1.Tests.Inventory;
using NUnit.Framework;

namespace ConsoleApplication1.Tests
{
    [TestFixture]
    public class SodaMachineTest
    {
        public class Insert
        {
            [TestCase(4, 4)]
            public void Insert_ShouldIncreaseBalanceWithInsertedValue(double value, double expected)
            {
                // setup
                var money = new Money();
                
                // action
                money.Insert(value);
                
                // assert
                Assert.AreEqual(expected, money.Balance);
            }
            
            [Test]
            public void Withdraw_ShouldReduceWithGivenValue()
            {
                // setup
                var insert = 100;
                var amount = 10;
                
                var money = new Money();
                money.Insert(insert);
                
                // action
                money.Withdraw(amount);
                
                // assert
                var expected = insert - amount; 
                Assert.AreEqual(expected, money.Balance);
            }
        }
        public class Order
        {
            [Test]
            public void Order_ShouldReduceProductInventory_WhenProductIsBought()
            {
                // setup
                var initialBalance = 1;
                var initialPrice = 1;
                var initialNr = 1;
                
                var inventory = new InventoryProviderTest(
                    new Soda[]
                    {
                        new Soda()
                            {Name = "coke", Nr = initialNr, Price = initialPrice}
                    });

                var money = new Money();
                money.Insert(initialBalance);
                var sut = new SodaMachine(inventory, money);

                // action
                var product = sut.GetProduct("coke");
                sut.Order(product);

                // assert
                var expected = --initialNr;
                var result = product.Nr;

                Assert.AreEqual(expected, result);
            }

            [Test]
            public void Order_ShouldReduceMoneyBalance_WhenProductIsBought()
            {
                // setup
                var initialBalance = 1;
                var initialPrice = 1;
                var initialNr = 1;
                
                var inventory = new InventoryProviderTest(
                    new Soda[]
                    {
                        new Soda()
                            {Name = "coke", Nr = initialNr, Price = initialPrice}
                    });

                var money = new Money();
                money.Insert(initialBalance);
                var sut = new SodaMachine(inventory, money);
                
                // action
                var product = sut.GetProduct("coke");
                sut.Order(product);

                // assert
                var expected = --initialBalance;
                var result = money.Balance;

                Assert.AreEqual(expected, result);
            }
            
            [Test]
            public void Order_ShouldNotReduceInventory_WhenInsufficientFunds()
            {
                // setup
                var initialBalance = 0;
                var initialPrice = 1;
                var initialNr = 1;
                
                var inventory = new InventoryProviderTest(
                    new Soda[]
                    {
                        new Soda()
                            {Name = "coke", Nr = initialNr, Price = initialPrice}
                    });

                var money = new Money();
                money.Insert(initialBalance);
                var sut = new SodaMachine(inventory, money);
                
                // action
                var product = sut.GetProduct("coke");
                sut.Order(product);

                // assert
                var expected = initialNr;
                var result = product.Nr;

                Assert.AreEqual(expected, result);
            }
            
            [Test]
            public void Order_ShouldNotReduceMoney_WhenInventoryIsEmpty()
            {
                // setup
                var initialBalance = 1;
                var initialPrice = 1;
                var initialNr = 0;
                
                var inventory = new InventoryProviderTest(
                    new Soda[]
                    {
                        new Soda()
                            {Name = "coke", Nr = initialNr, Price = initialPrice}
                    });

                var money = new Money();
                money.Insert(initialBalance);
                var sut = new SodaMachine(inventory, money);
                
                // action
                var product = sut.GetProduct("coke");
                sut.Order(product);

                // assert
                var expected = initialBalance;
                var result = money.Balance;

                Assert.AreEqual(expected, result);
            }
        }
        
        public class SmsOrder
        {
            [Test]
            public void SmsOrder_ShouldReduceProductInventory_WhenProductIsBought()
            {
                // setup
                var initialBalance = 1;
                var initialPrice = 1;
                var initialNr = 1;
                
                var inventory = new InventoryProviderTest(
                    new Soda[]
                    {
                        new Soda()
                            {Name = "coke", Nr = initialNr, Price = initialPrice}
                    });

                var money = new Money();
                money.Insert(initialBalance);
                var sut = new SodaMachine(inventory, money);

                // action
                var product = sut.GetProduct("coke");
                sut.SmsOrder(product);

                // assert
                var expected = --initialNr;
                var result = product.Nr;

                Assert.AreEqual(expected, result);
            }

            [Test]
            public void Order_ShouldNotReduceMoneyBalance_WhenProductIsBought()
            {
                // setup
                var initialBalance = 1;
                var initialPrice = 1;
                var initialNr = 1;
                
                var inventory = new InventoryProviderTest(
                    new Soda[]
                    {
                        new Soda()
                            {Name = "coke", Nr = initialNr, Price = initialPrice}
                    });

                var money = new Money();
                money.Insert(initialBalance);
                var sut = new SodaMachine(inventory, money);
                
                // action
                var product = sut.GetProduct("coke");
                sut.SmsOrder(product);

                // assert
                var expected = initialBalance;
                var result = money.Balance;

                Assert.AreEqual(expected, result);
            }
            
            [Test]
            public void Order_ShouldReduceInventory_WhenInsufficientFunds()
            {
                // setup
                var initialBalance = 0;
                var initialPrice = 1;
                var initialNr = 1;
                
                var inventory = new InventoryProviderTest(
                    new Soda[]
                    {
                        new Soda()
                            {Name = "coke", Nr = initialNr, Price = initialPrice}
                    });

                var money = new Money();
                money.Insert(initialBalance);
                var sut = new SodaMachine(inventory, money);
                
                // action
                var product = sut.GetProduct("coke");
                sut.SmsOrder(product);

                // assert
                var expected = --initialNr;
                var result = product.Nr;

                Assert.AreEqual(expected, result);
            }
        }
    }
}