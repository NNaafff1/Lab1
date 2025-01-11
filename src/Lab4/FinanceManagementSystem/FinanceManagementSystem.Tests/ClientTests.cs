using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace FinanceManagementSystem.Tests
{
    [TestClass]
    public class ClientTests
    {
        [TestMethod]
        public void CreateClient_ShouldInitializeCorrectly()
        {
            var client = new Client("John Doe", "johndoe@email.com");
            Assert.AreEqual("John Doe", client.Name);
            Assert.AreEqual("johndoe@email.com", client.Email);
            Assert.AreEqual(0, client.Accounts.Count);
        }

        [TestMethod]
        public void AddAccount_ShouldAddAccountToClient()
        {
            var client = new Client("John Doe", "johndoe@email.com");
            var account = new CurrentAccount("12345", 1000m, "USD");
            client.AddAccount(account);
            Assert.AreEqual(1, client.Accounts.Count);
            Assert.AreEqual("12345", client.Accounts[0].AccountNumber);
        }
    }
}
