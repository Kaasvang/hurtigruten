namespace ConsoleApplication1.Models
{
    public class Money
    {
        public double Balance { get; private set; } = 0;
        public void Withdraw(double amount) => Balance -= amount;
        public void WithdrawAll() => Withdraw(Balance);
        public void Insert(double amount) => Balance += amount;
    }
}