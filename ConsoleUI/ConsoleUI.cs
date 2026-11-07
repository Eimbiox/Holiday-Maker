class ConsoleUI
{
    public int MainMenu()
    {
        Console.Clear();
        Logo();
        Console.WriteLine($"1. Boka en kund\n2. Visa kundens bokningar\n3. Ändra kundens bokning\n4. Avboka\n5. Söka lediga rum\n6. Avsluta");
        
        int input;
        Console.Write("\nVälj ett av alternativen: ");
        while (!int.TryParse(Console.ReadLine(),out input) || input < 1 || input > 6 )
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Välj ett av alternativen: ");
            Console.ResetColor();
        }
        return input;
    }

    private void Logo()
    {
        Console.WriteLine(new string('=', 50));
        Console.WriteLine("TURBOBOKA 1997 EDITION PRO".PadLeft(38));
        Console.WriteLine(new string('=', 50));
    }
}