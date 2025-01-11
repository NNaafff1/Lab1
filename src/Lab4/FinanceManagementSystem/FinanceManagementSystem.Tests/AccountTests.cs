using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FinanceManagementSystem.Tests
{
    [TestClass]
    public class AccountTests
    {
        [TestMethod]
        public void Deposit_ValidAmount_ShouldIncreaseBalance()
        {
            var account = new CurrentAccount("12345", 1000m, "USD");
            account.Deposit(500m);
            Assert.AreEqual(1500m, account.Balance);
        }

        [TestMethod]
        public void Withdraw_ValidAmount_ShouldDecreaseBalance()
        {
            var account = new CurrentAccount("12345", 1000m, "USD");
            account.Withdraw(300m);
            Assert.AreEqual(700m, account.Balance);
        }

        [TestMethod]
        public void Withdraw_InsufficientBalance_ShouldNotChangeBalance()
        {
            var account = new CurrentAccount("12345", 1000m, "USD");
            bool result = account.Withdraw(1500m);
            Assert.IsFalse(result); 
            Assert.AreEqual(1000m, account.Balance);
        }

        [TestMethod]
        public void Transfer_ValidAmount_ShouldTransferFunds()
        {
            var account1 = new CurrentAccount("12345", 1000m, "USD");
            var account2 = new CurrentAccount("67890", 500m, "USD");

            account1.TransferTo(account2, 300m);

            Assert.AreEqual(700m, account1.Balance);
            Assert.AreEqual(800m, account2.Balance);
        }

        [TestMethod]
        public void Transfer_InsufficientBalance_ShouldNotTransferFunds()
        {
            var account1 = new CurrentAccount("12345", 1000m, "USD");
            var account2 = new CurrentAccount("67890", 500m, "USD");

            account1.TransferTo(account2, 1500m);

            Assert.AreEqual(1000m, account1.Balance);
            Assert.AreEqual(500m, account2.Balance);
        }

        [TestMethod]
        public void ApplyInterest_OnDepositAccount_ShouldIncreaseBalance()
        {
            var depositAccount = new DepositAccount("67890", 1000m, "USD");
            depositAccount.ApplyInterest();
            Assert.IsTrue(depositAccount.Balance > 1000m); 
        }
    }
}
