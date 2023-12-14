using ClassLibrary_AdressBook.Interfaces;
using ClassLibrary_AdressBook.Models;

namespace ConsoleApp_AdressBook.UtilitiesConsoleApp;

public class ContactInformation
{
    /// <summary>
    /// Method displays contact details for the specified contact
    /// </summary>
    /// <param name="contact">The contact for which to display property values</param>
    public static void DisplayDetails(IContact contact)
    {
        Console.WriteLine("------------------------------------------------------------------------------");
        Console.WriteLine($"First name: \t{contact.FirstName}");
        Console.WriteLine($"Last name: \t{contact.LastName}");
        Console.WriteLine($"E-mail: \t{contact.Email}");
        Console.WriteLine($"Phone number: \t{contact.Phone}");
        Console.WriteLine($"Address: \t{contact.Address}");
        Console.WriteLine($"ID: \t\t{contact.Id}");
        Console.WriteLine("------------------------------------------------------------------------------");
    }
}
