using System;
using System.Threading.Tasks;

namespace FinanceManagementSystem
{
    public static class CurrencyConverter
    {
        public static async Task<decimal> GetExchangeRate(string fromCurrency, string toCurrency)
        {
            if (fromCurrency == "USD" && toCurrency == "RUB") return 75.0m;
            if (fromCurrency == "USD" && toCurrency == "CNY") return 6.5m;
            if (fromCurrency == "RUB" && toCurrency == "USD") return 0.013m;
            if (fromCurrency == "CNY" && toCurrency == "USD") return 0.15m;

            return 0m; 
        }
    }
}
