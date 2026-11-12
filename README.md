# Holiday Maker – TurboBoka 1997 Edition Pro

Ett konsolprogram i C# för att boka boenden. Programmet använder en MySQL-databas för kunder, boenden, rum och bokningar.

## ER-diagram

![ER-diagram](docs/ER%20Diagram.jpg)

## Krav

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- En MySQL-server, till exempel i Docker
- En databas-IDE, till exempel DataGrip

## Kom igång

1. **Ladda ner projektet** från GitHub och öppna det i den IDE du vill använda.

2. **Skapa databasen.** Öppna din databas-IDE och kör `Holiday-Maker.sql`. Du får då databasen `holiday_maker` med alla tabeller, kolumner och testdata som behövs.

3. **Ändra connection string.** Öppna `Data/DbConnection.cs` och ändra connection string så att den stämmer med din databasanslutning:

   ```csharp
   "Server=localhost;User ID=root;Password=DITT_LÖSENORD;Database=holiday_maker"
   ```

4. **Starta programmet.** När anslutningen är klar skriver du följande i terminalen i din IDE:

   ```bash
   dotnet run
   ```

## Funktioner

- Registrera en ny kund eller välja en befintlig kund via email
- Söka lediga rum utifrån ort, datum och antal personer
- Filtrera på pool, restaurang, max avstånd till strand och max avstånd till centrum
- Sortera på pris (lägst först) eller betyg (högst först)
- Boka ett rum. Programmet kollar igen att rummet är ledigt precis innan bokningen sparas, och visar sedan en bekräftelse med bokningsnummer och totalpris.
- Visa en kunds bokningar
- Ändra antal personer i en bokning, inom rummets kapacitet
- Avboka en bokning

## Begränsningar

- Datum anges som en dag i **december 2026**. Det går därför inte att boka över nyår.
