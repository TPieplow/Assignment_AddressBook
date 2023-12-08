using ClassLibrary_AdressBook.Interfaces;
using ClassLibrary_AdressBook.Models;

namespace ConsoleApp_AdressBook.UtilitiesConsoleApp;

public class ContactInformation
{
    public static void DisplayDetails(IContact contact)
    {
        Console.WriteLine($" ### Contact Details for contact '{contact.FirstName} {contact.LastName}' ###");
        Console.WriteLine("------------------------------------------------------------------------------");
        Console.WriteLine($"{contact.Email}");
        Console.WriteLine($"{contact.Phone}");
        Console.WriteLine($"{contact.Address}");
        Console.WriteLine($"{contact.Id}");
        Console.WriteLine("------------------------------------------------------------------------------");
    }
}
