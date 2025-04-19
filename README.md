Sure! Based on the information you've provided, here's a draft for the README file:

---

# CurrencyConverter

A simple and efficient currency converter application built in **C#**. This tool fetches exchange rates from an API or locally cached data to provide currency conversions based on the user's chosen currency, amount, and update frequency.

## Features

- Real-time exchange rate updates via API integration.
- User-friendly interface for selecting currencies and entering amounts using the [Spectre.Console](https://spectreconsole.net/) package.
- Accurate and reliable currency conversion.
- 100% written in **C#** for cross-platform compatibility.

## Getting Started

### Prerequisites

- **.NET SDK**: Ensure you have the .NET SDK installed on your system. You can download it from [Microsoft's .NET website](https://dotnet.microsoft.com/).

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/AndrewBazen/CurrencyConverter.git
   cd CurrencyConverter
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Build the project:
   ```bash
   dotnet build
   ```

4. Run the application:
   ```bash
   dotnet run
   ```

## Usage

1. Upon starting the application, you will be asked to enter your API key.

     > ![API key prompt](https://github.com/user-attachments/assets/34e51352-f196-44fb-8c6d-a33803d167f5)
   - Currently, the supported API for exchange rates is [exchangerate-api.com](https://www.exchangerate-api.com/) (not sponsored).
  
2. You will need to choose the timeframe that you want your cached exchange rates to be valid.
     > ![cache duration prompt](https://github.com/user-attachments/assets/f9cc5b22-fdfc-474c-8a2e-bf0f3ff8e53d)
    - depending on your choice and the time your last cached exchange rates were pulled, the program can respond in a few ways.
        - if you do not input an API key, the program will try to use your cached rates.
        - if you input an API key, the program will automatically pull the most up-to-date rates and update your cache.
        - if you do not input an API key and your cache is older than your chosen duration, the program will ask you if you would like to use the old cached rates
            > ![old cache data](https://github.com/user-attachments/assets/ee73d29b-5014-482d-987b-f04dab180478)

        - if this is the first time you are running the program, and you do not have an API key, it will use the default rates that come in the program files. (will not be accurate)
     
5. Select the currency that you wish to convert from.
     > ![currency input](https://github.com/user-attachments/assets/b7da130a-e814-44b2-8caa-a75c798c504d)

7. input the amount that you wish to convert.
     > ![amount input](https://github.com/user-attachments/assets/9c6dd622-848b-409f-a681-3a86be3db1b5)
5. A table of currencies with associated rates and conversions for the input amount will be output.
     > ![table output](https://github.com/user-attachments/assets/73c208ac-fa45-4a3a-907c-a983bc7b42df)
6. From here, you can choose select yes to do another conversion, or select no to end the program.


## Contributing

Contributions are welcome! If you'd like to improve this project, please fork the repository and submit a pull request.

1. Fork the repo.
2. Create a feature branch:
   ```bash
   git checkout -b feature-name
   ```
3. Commit your changes:
   ```bash
   git commit -m "Description of changes"
   ```
4. Push to the branch:
   ```bash
   git push origin feature-name
   ```
5. Open a pull request.

## License

This project is open-source and available under the [MIT License](LICENSE).

---

Feel free to let me know if you'd like to customize or expand any part of this README further!
