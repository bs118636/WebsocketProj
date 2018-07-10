using System.Collections.Generic;

namespace Test.Cryptocurrencies
{
    public static class CryptoPrices
    {
        public static Dictionary<string, decimal> crDictionary = 
            new Dictionary<string, decimal>()
            {
                { "BTCUSD", 6775.08m },
                { "ETHUSD", 488.69m },
                { "XRPUSD", 0.477799m },
                { "BCHUSD", 2751.57m },
                { "EOSUSD", 8.72m },
                { "ETHBTC", 0.07213066m },
                { "XRPBTC", 0.00007052m },
                { "BCHBTC", 0.11093138m },
                { "EOSBTC", 0.00128678m }
            };
    }
}
