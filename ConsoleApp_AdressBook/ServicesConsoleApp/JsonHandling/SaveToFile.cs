using ClassLibrary_AdressBook.Interfaces;
using ConsoleApp_AdressBook.ServiceConsoleApp;

namespace ConsoleApp_AdressBook.ServicesConsoleApp.JsonHandling;

public class SaveToFile
{
    private IContactService _contactService;
    private IEnumerable<IContact> _contacts;
    private readonly IJsonWriter _writer;

    public SaveToFile(IContactService contactService, IEnumerable<IContact> contacts, IJsonWriter writer)
    {
        _contactService = contactService;
        _contacts = contacts;
        _writer = writer;
    }

    public void Save()
    {
        try
        {
            List<IContact> contacts = _contactService.GetContacts().ToList();
            Console.Write("Name the file: ");
            string fileName = Console.ReadLine()!;

            if (!string.IsNullOrEmpty(fileName))
            {
                _writer?.SaveToFile(contacts);
                DisplayMessage.Message("File successfully saved.");
            }
            else
            {
                DisplayMessage.Message("Error while processing list.");
            }
        }
        catch (Exception ex) { Console.WriteLine($"{ex.Message}"); }
    }
}
