using System;

namespace FinanceManagementSystem
{
    public abstract class Account
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; protected set; }
        public string Currency { get; set; }

        protected Account(string accountNumber, decimal initialBalance, string currency)
        {
            AccountNumber = accountNumber;
            Balance = initialBalance;
            Currency = currency;
        }

        public abstract bool Withdraw(decimal amount);
        public abstract void Deposit(decimal amount);
        public abstract bool TransferTo(Account targetAccount, decimal amount);
    }

    public class CurrentAccount : Account
    {
        public CurrentAccount(string accountNumber, decimal initialBalance, string currency)
            : base(accountNumber, initialBalance, currency) { }

        public override bool Withdraw(decimal amount)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
                return true;
            }
            return false;
        }

        public override void Deposit(decimal amount)
        {
            Balance += amount;
        }

        public override bool TransferTo(Account targetAccount, decimal amount)
        {
            if (Withdraw(amount))
            {
                targetAccount.Deposit(amount);
                return true;
            }
            return false;
        }
    }

    public class DepositAccount : Account
    {
        private const decimal InterestRate = 0.05m; 

        public DepositAccount(string accountNumber, decimal initialBalance, string currency)
            : base(accountNumber, initialBalance, currency) { }

        public override bool Withdraw(decimal amount)
        {
            return false;
        }

        public override void Deposit(decimal amount)
        {
            Balance += amount;
        }

        public override bool TransferTo(Account targetAccount, decimal amount)
        {
            return false; 
        }

        public void ApplyInterest()
        {
            Balance += Balance * InterestRate;
        }
    }
}
