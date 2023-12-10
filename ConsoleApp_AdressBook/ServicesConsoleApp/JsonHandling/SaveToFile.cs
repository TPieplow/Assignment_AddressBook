using ClassLibrary_AdressBook.Interfaces;
using ClassLibrary_AdressBook.JsonHandling;
using ConsoleApp_AdressBook.ServiceConsoleApp;

namespace ConsoleApp_AdressBook.ServicesConsoleApp.JsonHandling;

public class SaveToFile
{
    private readonly IEnumerable<IContact> _contacts;
    private readonly IJsonWriter _writer;

    public SaveToFile(IEnumerable<IContact> contacts, IJsonWriter writer)
    {
        _contacts = contacts;
        _writer = writer;
    }

    public void Save()
    {
        try
        {
            List<IContact> contacts = _contacts.ToList();
            Console.Write("Name the file: ");
            string fileName = Console.ReadLine() ?? string.Empty!;

            Console.WriteLine("What file do you want to create? ");
            Console.WriteLine("[1]. .txt-file");
            Console.WriteLine("[2]. .json-file");

            string textChoice = Console.ReadLine() ?? string.Empty!;

            if (!string.IsNullOrEmpty(textChoice))
            {
                _writer?.SaveToFile(fileName, textChoice, contacts);
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
