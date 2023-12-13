using ClassLibrary_AdressBook.Interfaces;
using ClassLibrary_AdressBook.JsonHandling;
using ConsoleApp_AdressBook.ServiceConsoleApp;
using ConsoleApp_AdressBook.UtilitiesConsoleApp;
using System.Diagnostics;

namespace ConsoleApp_AdressBook.ServicesConsoleApp.JsonHandling;

public class LoadFromFile
{
    private readonly IJsonReader _reader;
    private readonly IContactService _contactService;
    private readonly ShowFolder _showFolder;

    public LoadFromFile(IJsonReader reader, IContactService contactService)
    {
        _reader = reader;
        _contactService = contactService;
        _showFolder = new ShowFolder();
    }

    public void Load()
    {
        try
        {
            Console.Clear();
 
            List<IContact> loadedContacts = _reader.LoadFromFile();

            if (loadedContacts is not null)
            {
                foreach (IContact contact in loadedContacts)
                {
                    Console.WriteLine($"Loaded contacts: {contact.FirstName}");
                    _contactService?.AddContact(contact);
                }
                DisplayMessage.Message("Contacts loaded successfully.");
            }
            else
            {
                DisplayMessage.Message("Error: No contacts loaded from the file.");
            }
        }
        catch (Exception ex)
        {
            Debug.Write(ex.Message);
        }
    }
}
