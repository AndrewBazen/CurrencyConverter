
using Spectre.Console;
using System.ComponentModel.Design;
using System.Text.Json;

namespace CurrencyConverter
{
    /// <summary>
    /// Class to handle fetching and caching exchange rates.
    /// </summary>
    public class ExchangeRates
    {
        private const string CacheFilePath = "exchange_cache.json";  // Path to the cache file

        /// <summary> 
        /// calls methods to load exchange rates from the API or cached file.
        /// </summary>
        /// 
        /// <param name="apiKey"></param>
        /// <param name="cacheDurationHours"></param>
        /// <param name="loadFromCache"></param>
        /// <returns type="APIResponse"></returns>
        public static APIResponse? FetchExchangeRates(string apiKey, int cacheDurationHours, bool loadFromCache)
        {
            AnsiConsole.WriteLine(apiKey);
            TimeSpan cacheValidity = TimeSpan.FromHours(cacheDurationHours);
            var cacheFile = new FileInfo(CacheFilePath);
            var age = DateTime.Now - cacheFile.LastWriteTime;

            // if the user has not provided an API key load from cache
            if (loadFromCache && age < cacheValidity)
            {
                // Load from cache if the cache is not expired
                var cachedRates = LoadFromCacheFile();
                return cachedRates;
            }
            else if (!loadFromCache && apiKey != "")
            {
                // If the cache is expired or doesn't exist or this isnt the firstrun, fetch new rates
                var rates = ApiUtil.ApiCall(apiKey);
                if (rates == null)
                {
                    AnsiConsole.MarkupLine("[red]Failed to fetch exchange rates.[/]");
                    AnsiConsole.MarkupLine("[red]Please check your API key and try again.[/]");
                    return null;
                }
                UpdateCachedRates(rates);
                return rates;
            }
            else
            {
                // If there is no Api Key and the cach, ask the user if they want to load an old cache
                AnsiConsole.MarkupLine($"[red]No API key provided and no cached data available within {cacheDurationHours}hrs.[/]");
                var select = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Do you want to load an old cache?")
                        .AddChoices(new[] { "Yes", "No" }));
                if (select == "Yes")
                {
                    var cachedRates = LoadFromCacheFile();
                    if (cachedRates != null)
                    {
                        return cachedRates;
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[red]No cached data available.[/]");
                        return null;
                    }
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]Exiting the program...[/]");
                    return null;
                }
            }
        }

        /// <summary>
        /// Loads exchange rates from the cache file if it exists and is not expired.
        /// </summary>
        /// 
        /// <param name="cacheFilePath"></param>
        /// <param name="cacheDurationHours"></param>
        /// <returns type="APIResponse"></returns>
        public static APIResponse? LoadFromCacheFile()
        {
            // Check if the cache file exists
            if (!File.Exists(CacheFilePath))
            {
                AnsiConsole.MarkupLine("[red]Cache file not found.[/]");
                return null;
            }
            try
            {
                AnsiConsole.MarkupLine("[yellow]Loading exchange rates from cache...[/]");
                var cachedData = File.ReadAllText(CacheFilePath);
                var cachedRates = JsonSerializer.Deserialize<APIResponse>(cachedData);
                if (cachedRates != null)
                {
                    AnsiConsole.MarkupLine("[green]Exchange rates loaded from cache successfully.[/]");
                    return cachedRates;
                }
            }
            catch
            {
                AnsiConsole.MarkupLine("[yellow]Failed to parse cached rates, trying to fetch fresh data...[/]");
            }
            return null;
        }

        /// <summary>Updates the Cached file</summary>
        /// 
        /// <param name="rates"></param>
        /// <returns type="void"></returns>
        public static void UpdateCachedRates(APIResponse rates)
        {
            // clear the cache file if it exists
            if (File.Exists(CacheFilePath))
            {
                File.Delete(CacheFilePath);
            }
            else
            {
                // create the cache file if it doesn't exist
                File.Create(CacheFilePath).Dispose();
            }

            // Save the fetched rates to cache
            File.WriteAllText(CacheFilePath, JsonSerializer.Serialize(rates));
            AnsiConsole.MarkupLine("[green]Exchange rates fetched and cached successfully.[/]");
        }
    }
}
