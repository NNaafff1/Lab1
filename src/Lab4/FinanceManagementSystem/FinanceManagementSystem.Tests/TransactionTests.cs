using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FinanceManagementSystem.Tests
{
    [TestClass]
    public class TransactionTests
    {
        [TestMethod]
        public void CreateTransaction_ShouldInitializeCorrectly()
        {
            var transaction = new Transaction("T001", 500m, DateTime.Now, "12345", "67890");

            Assert.AreEqual("T001", transaction.TransactionId);
            Assert.AreEqual(500m, transaction.Amount);
            Assert.AreEqual("12345", transaction.FromAccount);
            Assert.AreEqual("67890", transaction.ToAccount);
        }

        [TestMethod]
        public void TransactionHistory_ShouldContainCorrectTransactions()
        {
            var transactions = new System.Collections.Generic.List<Transaction>
            {
                new Transaction("T001", 500m, DateTime.Now, "12345", "67890"),
                new Transaction("T002", 200m, DateTime.Now, "67890", "12345")
            };

            Assert.AreEqual(2, transactions.Count);
            Assert.AreEqual("T001", transactions[0].TransactionId);
            Assert.AreEqual(200m, transactions[1].Amount);
        }
    }
}
