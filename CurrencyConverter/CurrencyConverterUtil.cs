using Spectre.Console;
using static CurrencyConverter.ExchangeRates;

namespace CurrencyConverter
{

    /// <summary>
    /// Utility class for currency conversion.
    /// </summary>
    public class CurrencyConverterUtil
    {
        /// <summary>
        /// Gets the currency symbol for a given currency name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
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
