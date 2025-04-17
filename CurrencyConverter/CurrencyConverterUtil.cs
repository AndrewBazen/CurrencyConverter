using Spectre.Console;
using static CurrencyConverter.ExchangeRates;

namespace CurrencyConverter
{
    public class CurrencyConverterUtil
    {

        public static char GetCurrencySymbol(string name)
        {
            Dictionary<string, char> symbols = new Dictionary<string, char>
            {
                { "USD", '$' },
                { "EUR", '€' },
                { "GBP", '£' },
                { "JPY", '¥' },
                { "AUD", '$' },
                { "CAD", '$' },
                { "CHF", '₣' },
                { "CNY", '¥' },
                { "SEK", 'k' },
                { "NZD", '$' }

            };
            return symbols[name];
        }
    }
}
