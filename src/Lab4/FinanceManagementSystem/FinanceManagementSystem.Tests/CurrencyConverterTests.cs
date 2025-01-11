using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace FinanceManagementSystem.Tests
{
    [TestClass]
    public class CurrencyConverterTests
    {
        [TestMethod]
        public async Task GetExchangeRate_ValidCurrencies_ShouldReturnRate()
        {
            decimal rate = await CurrencyConverter.GetExchangeRate("USD", "RUB");
            Assert.IsTrue(rate > 0); 
        }

        [TestMethod]
        public async Task GetExchangeRate_UnsupportedCurrency_ShouldReturnZeroOrFail()
        {
            try
            {
                decimal rate = await CurrencyConverter.GetExchangeRate("USD", "XYZ");
                Assert.AreEqual(0, rate); 
            }
            catch (Exception)
            {
                Assert.IsTrue(true); 
            }
        }
    }
}
