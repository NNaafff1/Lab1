using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace FinanceManagementSystem.Tests
{
    [TestClass]
    public class ReportGeneratorTests
    {

        [TestMethod]
        public void GenerateTransactionReport_ShouldDisplayCorrectTransactions()
        {
            var transactions = new List<Transaction>
            {
                new Transaction("T001", 500m, DateTime.Now, "12345", "67890"),
                new Transaction("T002", 200m, DateTime.Now, "67890", "12345")
            };

            using (var sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);
                ReportGenerator.GenerateTransactionReport(transactions);
                var result = sw.ToString();
                Assert.IsTrue(result.Contains("T001"));
                Assert.IsTrue(result.Contains("500"));
                Assert.IsTrue(result.Contains("T002"));
            }
        }
    }
}
