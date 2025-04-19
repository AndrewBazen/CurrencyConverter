/* * Currency Converter
 * @description: A simple currency converter that fetches exchange rates from an API and displays them in a table.
 * @author: Andrew Bazen
 * @date: 2023-10-01
 * 
 */
using CurrencyConverter;
using static CurrencyConverter.TableRenderer;
using static CurrencyConverter.ExchangeRates;
using static CurrencyConverter.CommonCurrency;
using Spectre.Console;

int[] cacheDurationHours = [2, 4, 6, 8, 10, 12, 24]; // default cache durations in hours
bool loadFromCache = false; // flag to check if this is the first run of the program

// Writes the title of the program in a fancy font
AnsiConsole.Write(
    new FigletText("Currency Converter")
        .Centered()
        .Color(Color.Green));

// prompt the user for their API key
var apiKey = AnsiConsole.Prompt(
    new TextPrompt<string>("Please input your API key? [green][/]")
        .PromptStyle("green")
        .AllowEmpty()
        .Secret());

if (apiKey == "")
{
    // if the user has not provided an API key, load from cache
    loadFromCache = true;
    AnsiConsole.MarkupLine("[yellow]No API key provided. Loading from cache...[/]");
}

// prompt the user for the cache duration
int cacheDuration = AnsiConsole.Prompt(
    new SelectionPrompt<int>()
        .Title("Select a cache duration for updates:")
        .AddChoices(cacheDurationHours));

// ask user for currency type to convert from
Currency selectedCurrency = AnsiConsole.Prompt(
       new SelectionPrompt<Currency>()
           .Title("What currency do you want to convert from?")
           .AddChoices(Enum.GetValues(typeof(Currency)).Cast<Currency>()));

// ask user for amount
var amount = AnsiConsole.Prompt(
        new TextPrompt<double>("What is the amount you want to convert? [green](e.g. 100)[/]")
            .PromptStyle("green"));

// fetch exchange rates from the API
var exchangeRates = FetchExchangeRates(apiKey, cacheDuration, loadFromCache);


// check if the exchange rates are valid
if (exchangeRates == null)
{
    AnsiConsole.MarkupLine("[red]Failed to fetch exchange rates.[/]");
    AnsiConsole.MarkupLine("[red]Please check your API key and try again.[/]");
    // wait for user to acknowledge the error
    AnsiConsole.MarkupLine("[red]Press any key to exit...[/]");
    Console.ReadKey();
    AnsiConsole.MarkupLine("[red]Exiting the program...[/]");
    Environment.Exit(1);
}

RenderTable(selectedCurrency, exchangeRates, amount);

while (true)
{
    // ask user if they want to convert another currency
    var convertAnother = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("Do you want to convert another currency?")
            .AddChoices(new[] { "Yes", "No" }));
    if (convertAnother == "No")
    {
        AnsiConsole.MarkupLine("[green]Exiting the program...[/]");
        break;
    }
    // ask user for currency type to convert from
    selectedCurrency = AnsiConsole.Prompt(
       new SelectionPrompt<Currency>()
           .Title("What currency do you want to convert from?")
           .AddChoices(Enum.GetValues(typeof(Currency)).Cast<Currency>()));
    // ask user for amount
    amount = AnsiConsole.Prompt(
        new TextPrompt<double>("What is the amount you want to convert? [green](e.g. 100)[/]")
            .PromptStyle("green"));
    RenderTable(selectedCurrency, exchangeRates, amount);
}


