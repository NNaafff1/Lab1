using System;

namespace FinanceManagementSystem
{
    public class Transaction
    {
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string FromAccount { get; set; }
        public string ToAccount { get; set; }

        public Transaction(string transactionId, decimal amount, DateTime date, string fromAccount, string toAccount)
        {
            TransactionId = transactionId;
            Amount = amount;
            Date = date;
            FromAccount = fromAccount;
            ToAccount = toAccount;
        }
    }
}
