using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace FinanceManagementSystem.Tests
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void Program_ShouldInitializeClientAndAccountCorrectly()
        {
            var client = new Client("John Doe", "johndoe@email.com");
            var account = new CurrentAccount("12345", 1000m, "USD");
            client.AddAccount(account);
            Assert.AreEqual("John Doe", client.Name);
            Assert.AreEqual(1, client.Accounts.Count);
            Assert.AreEqual(1000m, client.Accounts[0].Balance);
        }

        [TestMethod]
        public void Program_ShouldExecuteTransactionCorrectly()
        {
            var account1 = new CurrentAccount("12345", 1000m, "USD");
            var account2 = new CurrentAccount("67890", 500m, "USD");

            account1.TransferTo(account2, 300m);

            Assert.AreEqual(700m, account1.Balance);
            Assert.AreEqual(800m, account2.Balance);
        }
    }
}
