using System.Text.Json;

namespace CurrencyConverter
{
    // updates the JSON APIResponse and ConversionRates classes by setting propertiies from an new JSON
    internal class APIUpdate
    {
        public static void Update(APIResponse apiResponse)
        {

            string url = "https://v6.exchangerate-api.com/v6/aed50817a65470dfacf0eb9d/latest/USD";

            HttpClient client = new HttpClient();
            var newApiResponse = JsonSerializer.Deserialize<APIResponse>(client.GetStringAsync(url).Result);
            if (newApiResponse != null)
            {
                apiResponse.result = newApiResponse.result;
                apiResponse.documentation = newApiResponse.documentation;
                apiResponse.terms_of_use = newApiResponse.terms_of_use;
                apiResponse.time_last_update_unix = newApiResponse.time_last_update_unix;
                apiResponse.time_last_update_utc = newApiResponse.time_last_update_utc;
                apiResponse.time_next_update_unix = newApiResponse.time_next_update_unix;
                apiResponse.time_next_update_utc = newApiResponse.time_next_update_utc;
                apiResponse.base_code = newApiResponse.base_code;
                apiResponse.conversion_rates = newApiResponse.conversion_rates;
            }
        }
    }
}
