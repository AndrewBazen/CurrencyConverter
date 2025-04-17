using CurrencyConverter;
using System.Text.Json;
using 

namespace CurrencyTest
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod2()
        {
            string url = "https://v6.exchangerate-api.com/v6/aed50817a65470dfacf0eb9d/latest/USD";
       
            HttpClient client = new HttpClient();
            var response = client.GetStringAsync(url);


            //JsonDocument doc = JsonDocument.Parse(response.Result);

            var rates = JsonSerializer.Deserialize<APIResponse>(response.Result);

            Assert.AreNotEqual(rates.conversion_rates.EUR, 0);

        }
    }
}
