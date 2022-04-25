# Koalunch - API Server

[![.NET](https://github.com/Narvio/koalunch-api/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Narvio/koalunch-api/actions/workflows/dotnet.yml)

Hosted at https://koalunch.azurewebsites.net/

Provides api for [Koalunch client](https://github.com/Narvio/koalunch) with daily menu information for restaurants around CTPark Brno Holandská.

List of provided restaurant menus:

- [Eatology (IQ Holandská)](http://iqrestaurant.cz/brno/menu.html)
- [IQ Morávka](http://www.iqrestaurant.cz/moravka.html?iframe=true)
- [Tusto Titanium](http://titanium.tusto.cz/tydenni-menu/)
- [Kometa Pub Arena](https://www.kometapub.cz/arena.php)
- [Rebio Holandská](http://www.rebio.cz/Holandska/Nase-nabidka/dW-ei.folder.aspx)
- [U Hovězího pupku](http://www.uhovezihopupku.cz/menu/)
- [Hostinec u Tesaře](http://www.utesare.cz/poledni-nabidka/)
- [Buffalo American Steakhouse](http://www.restauracebuffalo.cz/)
- [Jean Paul's Bistro](https://www.jpbistro.cz/menu-holandska/index.php)

## Technologies used

- [dotnet](https://dotnet.microsoft.com/)
- [Entity Framework](https://docs.microsoft.com/en-us/ef/)
- [Google Spreadsheets API](https://developers.google.com/sheets/api/quickstart/dotnet) as a feedback storage
- [xUnit](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test) + [Moq](https://www.nuget.org/packages/Moq/) for testing
- [GitHub Actions](https://github.com/features/actions) for continuous delivery
- [Microsoft Azure](https://azure.microsoft.com/) as a hosting platform
