using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceManagementSystem
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new Client("John Doe", "johndoe@email.com");
            var currentAccount = new CurrentAccount("12345", 1000m, "USD");
            var depositAccount = new DepositAccount("67890", 5000m, "USD");

            client.AddAccount(currentAccount);
            client.AddAccount(depositAccount);

            currentAccount.Deposit(500m);
            currentAccount.Withdraw(200m);
            currentAccount.TransferTo(depositAccount, 300m);

            depositAccount.ApplyInterest();

            ReportGenerator.GenerateAccountReport(client);

            var transactions = new List<Transaction>
            {
                new Transaction("T001", 500m, DateTime.Now, "12345", "67890"),
                new Transaction("T002", 300m, DateTime.Now, "67890", "12345")
            };
            ReportGenerator.GenerateTransactionReport(transactions);

            decimal rate = await CurrencyConverter.GetExchangeRate("USD", "RUB");
            Console.WriteLine($"Exchange rate from USD to RUB: {rate}");

            Console.ReadKey();
        }
    }
}
