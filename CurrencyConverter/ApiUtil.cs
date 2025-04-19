using Spectre.Console;
using System.Text.Json;

namespace CurrencyConverter
{
    /// <summary>
    /// Utility class for making API calls to fetch exchange rates.
    /// </summary>
    internal class ApiUtil
    {
        /// <summary>
        /// Makes an API call to fetch exchange rates using the provided API key.
        /// </summary>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public static APIResponse? ApiCall(string apiKey)
        {
            // implements the users API key to fetch exchange rates
            string url = $"https://v6.exchangerate-api.com/v6/{apiKey}/latest/USD";
            HttpClient client = new HttpClient();

            try
            {
                var response = client.GetStringAsync(url).Result;
                var rates = JsonSerializer.Deserialize<APIResponse>(response);
                if (rates != null)
                {
                    AnsiConsole.MarkupLine("[green]Exchange rates fetched successfully![/]");
                    return rates;
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]Failed to fetch exchange rates.[/]");
                    AnsiConsole.MarkupLine("[red]Please check your API key and try again.[/]");
                    // wait for user to acknowledge the error
                    AnsiConsole.MarkupLine("[red]Press any key to exit...[/]");
                    Console.ReadKey();
                    AnsiConsole.MarkupLine("[red]Exiting the program...[/]");
                    Environment.Exit(1);
                }
            }
            catch (HttpRequestException e)
            {
                AnsiConsole.MarkupLine($"[red]Error fetching exchange rates: {e.Message}[/]");
                AnsiConsole.MarkupLine("[red]Please check your API key and try again.[/]");
                // wait for user to acknowledge the error
                AnsiConsole.MarkupLine("[red]Press any key to exit...[/]");
                Console.ReadKey();
                AnsiConsole.MarkupLine("[red]Exiting the program...[/]");
                Environment.Exit(1);
            }
            catch (JsonException e)
            {
                AnsiConsole.MarkupLine($"[red]Error parsing exchange rates: {e.Message}[/]");
                AnsiConsole.MarkupLine("[red]Please check the API response format.[/]");
                // wait for user to acknowledge the error
                AnsiConsole.MarkupLine("[red]Press any key to exit...[/]");
                Console.ReadKey();
                AnsiConsole.MarkupLine("[red]Exiting the program...[/]");
                Environment.Exit(1);
            }
          
            finally
            {
                client.Dispose();
            }
            return null;
        }
    }
}
