
using System.Text.Json;

namespace CurrencyConverter
{
    public class ExchangeRates
    {
        public static APIResponse FetchExchangeRates()
        {
            string url = "https://v6.exchangerate-api.com/v6/aed50817a65470dfacf0eb9d/latest/USD";
            HttpClient client = new HttpClient();
            var response = client.GetStringAsync(url).Result;
            var rates = JsonSerializer.Deserialize<APIResponse>(response);
            if (rates != null)
            {
                return rates;
            }
            else
            {
                throw new Exception("Failed to fetch exchange rates.");
            }
        }
    }
}
