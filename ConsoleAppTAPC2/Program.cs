using System;

namespace ConsoleAppTAPC2
{
    public abstract class Account {
        public  decimal Balance { get; private set; }

        public void Deposit(decimal ammount)
        {
            this.Balance += ammount;
        }

        public decimal withdraw(decimal ammount)
        {
            var fee = Calculatewithdrawfee(ammount);
            ammount = ammount + fee;
            if (Balance <= ammount)
                throw new InvalidOperationException("Fonduri insuficiente");
            else
                this.Balance -= ammount;
            return ammount;

        }

        protected abstract decimal Calculatewithdrawfee(decimal ammount);
    }

    public class DebitAccount : Account
    {
        protected override decimal Calculatewithdrawfee(decimal ammount)
        {
            return 0m;
        }
    }

    public class SavingsAccount : Account {
        protected override decimal Calculatewithdrawfee(decimal ammount)
        {
            return ammount* 0.5m/ 100m;
        }
    }

    class Program
    {
        private const int Account = 500;
        static void Main(string[] args)
        {
            var acc = new SavingsAccount();
            acc.Deposit(200);
            Console.WriteLine($"Disponibil {acc.Balance} Ron");
            try
            {
                acc.withdraw(100);
            }
            catch (InvalidOperationException ex) {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine($"Disponibil {acc.Balance} Ron");
        }
    }
}
