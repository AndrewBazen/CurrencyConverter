using CurrencyConverter;
using System.Text.Json;

namespace CurrencyTest
{
    [TestClass]
    public sealed class Test1
    {
        //[TestMethod]
        //public void ApiTest()
        //{
        //    string url = "https://v6.exchangerate-api.com/v6/aed50817a65470dfacf0eb9d/latest/USD";
       
        //    HttpClient client = new HttpClient();
        //    var response = client.GetStringAsync(url);


        //    //JsonDocument doc = JsonDocument.Parse(response.Result);

        //    var rates = JsonSerializer.Deserialize<APIResponse>(response.Result);

        //    Assert.AreNotEqual(rates.conversion_rates.EUR, 0);

        //}

        [TestMethod]
        public void ConvertTest()
        {
            var rates = new APIResponse
            {
                conversion_rates = new Conversion_Rates
                {
                    EUR = 0.8784f,
                    USD = 1,
                    GBP = 0.7547f,

                    // Add other currencies as needed
                }
            };

            // Test the conversion rates
            var rate = rates.conversion_rates.EUR;
            var amount = 100;
            // convert 100 EUR to USD
            var convertedValue = Math.Round(amount/rate,2);
            // Assert that the conversion rate is correct
            Assert.AreEqual(convertedValue, 113.84);
            rate = rates.conversion_rates.GBP;
            // Convert 100 EUR in USD to GBP
            var nextConvertedValue = Math.Round(convertedValue * rate, 2);
            // Assert that the conversion rate is correct
            Assert.AreEqual(nextConvertedValue, 85.92);
        }
    }
}
