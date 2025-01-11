using System;
using System.Collections.Generic;

namespace FinanceManagementSystem
{
    public static class ReportGenerator
    {
        public static void GenerateAccountReport(Client client)
        {
            Console.WriteLine($"Account Report for {client.Name} ({client.Email})");
            foreach (var account in client.Accounts)
            {
                Console.WriteLine($"Account {account.AccountNumber}: {account.Balance} {account.Currency}");
            }
        }

        public static void GenerateTransactionReport(List<Transaction> transactions)
        {
            Console.WriteLine("Transaction Report:");
            foreach (var transaction in transactions)
            {
                Console.WriteLine($"Transaction ID: {transaction.TransactionId}, Amount: {transaction.Amount}, From: {transaction.FromAccount}, To: {transaction.ToAccount}, Date: {transaction.Date}");
            }
        }
    }
}
