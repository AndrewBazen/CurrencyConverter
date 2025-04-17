/* * Currency Converter
 * @description: A simple currency converter that fetches exchange rates from an API and displays them in a table.
 * @author: Andrew Bazen
 * @date: 2023-10-01
 * 
 */
using CurrencyConverter;
using static CurrencyConverter.ExchangeRates;
using static CurrencyConverter.APIUpdate;
using static CurrencyConverter.CommonCurrency;
using Spectre.Console;

// Writes the title of the program in a fancy font
AnsiConsole.Write(
    new FigletText("Currency Converter")
        .Centered()
        .Color(Color.Green));

// creates a new APIResponse object to hold the exchange rates from the WEB API
APIResponse exchangeRates = FetchExchangeRates();

// ask user for currency type to convert from
Currency selectedCurrency = AnsiConsole.Prompt(
       new SelectionPrompt<Currency>()
           .Title("What currency do you want to convert from?")
           .AddChoices(Enum.GetValues(typeof(Currency)).Cast<Currency>()));

// ask user for amount
var amount = AnsiConsole.Prompt(
        new TextPrompt<double>("What is the amount you want to convert? [green](e.g. 100)[/]")
            .PromptStyle("green"));

// starts a live display of the exchange rates
AnsiConsole.Live(new Panel(new Table()))
           .Start(ctx =>
           {
               
               while (true)
               {
                   // run update for api to ensure the most up to date data
                   Update(exchangeRates);

                   var table = new Table().Centered()
                       .Border(TableBorder.Rounded)
                       .Title("[bold]USD Exchange Rates[/]")
                       .AddColumn("[yellow]Currency[/]")
                       .AddColumn("[green]Rate[/]")
                       .AddColumn(Enum.GetName(typeof(Currency), selectedCurrency) + " Amount")
                       .AddColumn("[green]Converted Amount[/]");

                   foreach (var rate in exchangeRates.conversion_rates.GetType().GetProperties())
                   {
                       if (rate.Name == Enum.GetName(typeof(Currency), selectedCurrency))
                       {
                           continue; // Skip the selected currency
                       }

                       // check if the rate is a valid currency
                       if (!Enum.TryParse(typeof(Currency), rate.Name, out _))
                       {
                           continue; // Skip if not a valid currency
                       }

                       var currency = rate.Name;
                       var value = rate.GetValue(exchangeRates.conversion_rates);
                       var symbol = CurrencyConverterUtil.GetCurrencySymbol(rate.Name);
                       double convertedValue = Convert.ToDouble(value) * amount;
                       // clamp to 2 decimal places
                       convertedValue = Math.Abs(Math.Round(convertedValue, 2));
                       table.AddRow($"[green]{currency}[/]", $"[green]{symbol}{value}[/]", $"[green]{amount}[/]", $"[green]{symbol}{convertedValue}[/]");
                   }

                   var updated = new Panel(Align.Center(table))
                       .Header($"[grey]Updated: {DateTime.Now:T}[/]", Justify.Right)
                       .BorderColor(Color.Grey);

                   ctx.UpdateTarget(updated);

                   Task.Delay(1000); // Refresh every 10000 ms

                   // check if user pressed q
               } while (true);
           });

