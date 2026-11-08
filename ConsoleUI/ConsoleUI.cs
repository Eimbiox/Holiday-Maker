class ConsoleUI
{
    public int MainMenu()
    {
        Console.Clear();
        Logo();
        Console.WriteLine($"1. Kund meny\n2. Sök efter lediga rum\n3. Avsluta");
        
        int input;
        Console.Write("\nVälj ett av alternativen: ");
        while (!int.TryParse(Console.ReadLine(),out input) || input < 1 || input > 3 )
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

    public string AskForCustomerEmail()
    {
        Console.Write("Skriv in kundens email: ");
        string? input = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(input) || string.IsNullOrEmpty(input))
        {
            Console.Write("Skriv ett giltigt email");
            input = Console.ReadLine();
        }
        return input;

    }

    public string ReadCustomString(string promt)
    {
        Console.Write(promt);
        string? input = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(input) || string.IsNullOrEmpty(input))
        {
            Console.Write("Det får inte vara tomt");
            input = Console.ReadLine();
        }
        return input;
    }

    public bool WantToCreateCustomer()
    {
        Console.Write("Kunden kunde inte hittas i systemet. Vill du skapa en ny kund? (Y/N): ");

        string? input = Console.ReadLine()?.ToUpper();

        while (input != "Y" && input != "N")
        {
            Console.WriteLine("Svara med Y eller N");
            input = Console.ReadLine()?.ToUpper();
        }
        if (input == "Y")
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}