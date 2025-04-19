using Spectre.Console;
using System.Reflection;
using static CurrencyConverter.CommonCurrency;

namespace CurrencyConverter
{
    internal class TableRenderer
    {
        public static void RenderTable(Currency baseCurrency, APIResponse exchangeRates, double amount)
        {
            object? baseCurrencyValue = exchangeRates.conversion_rates.GetType().GetProperties()
                .FirstOrDefault(p => p.Name == Enum.GetName(typeof(Currency), baseCurrency))?.GetValue(exchangeRates.conversion_rates);
            // create the table for displaying the data
            var table = new Table().Centered()
                .Border(TableBorder.Rounded)
                .Title("[bold]USD Exchange Rates[/]")
                .AddColumn("[yellow]Currency[/]")
                .AddColumn("[green]Rate[/]")
                .AddColumn(Enum.GetName(typeof(Currency), baseCurrency) + " Amount")
                .AddColumn("[green]Converted Amount[/]");

            // add the observed currencies to the table with their conversion rates and converted values
            foreach (var rate in exchangeRates.conversion_rates.GetType().GetProperties())
            {
                if (rate.Name == Enum.GetName(typeof(Currency), baseCurrency))
                {
                    continue; // Skip the selected currency
                }

                // check if the rate is an observed currency
                if (!Enum.TryParse(typeof(Currency), rate.Name, out _))
                {
                    continue; // Skip if currency type is not observed
                }

                if (baseCurrency != Currency.USD)
                {
                    // convert the amount to the selected currency
                    var newTargetCurrency = rate.Name;
                    var targetValue = rate.GetValue(exchangeRates.conversion_rates);
                    var targetSymbol = CurrencyConverterUtil.GetCurrencySymbol(rate.Name);
                    double convertedUSDValue = amount / Convert.ToDouble(baseCurrencyValue);
                    double targetConvertedValue = Convert.ToDouble(targetValue) * convertedUSDValue;

                    targetConvertedValue = Math.Abs(Math.Round(targetConvertedValue, 2));   // clamp to 2 decimal places
                    table.AddRow($"[green]{newTargetCurrency}[/]", $"[green]{targetSymbol}{targetValue}[/]", $"[green]{amount}[/]", $"[green]{targetSymbol}{targetConvertedValue}[/]");

                }
                else
                {
                    // convert the amount to the selected currency
                    var targetCurrency = rate.Name;
                    var value = rate.GetValue(exchangeRates.conversion_rates);
                    var symbol = CurrencyConverterUtil.GetCurrencySymbol(rate.Name);
                    double convertedValue = Convert.ToDouble(value) * amount;

                    convertedValue = Math.Abs(Math.Round(convertedValue, 2));   // clamp to 2 decimal places
                    table.AddRow($"[green]{targetCurrency}[/]", $"[green]{symbol}{value}[/]", $"[green]{amount}[/]", $"[green]{symbol}{convertedValue}[/]");
                }
            }

            // add the table to a centered display panel
            var updated = new Panel(Align.Center(table))
                .Header($"[grey]Updated: {DateTime.Now:T}[/]", Justify.Right)
                .BorderColor(Color.Grey);

            // add the panel to the console
            AnsiConsole.Write(updated);
        }
    }
}
