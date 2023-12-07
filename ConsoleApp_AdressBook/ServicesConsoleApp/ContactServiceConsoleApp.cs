using ClassLibrary_AdressBook.Interfaces;
using ClassLibrary_AdressBook.Models;
using ConsoleApp_AdressBook.Interfaces;
using ConsoleApp_AdressBook.UtilitiesConsoleApp;

namespace ConsoleApp_AdressBook.ServiceConsoleApp;

public class ContactServiceConsoleApp : IContactServiceConsoleApp
{
    private readonly IContactService _contactService;
    private readonly ShowFolder _showFolder;


    public ContactServiceConsoleApp(IContactService contactService)
    {
        _contactService = contactService;
        _showFolder = new ShowFolder();
    }

    public void AddContactConsoleApp()
    {
        Console.Clear();
        Console.WriteLine("### ADD NEW CONTACT ###");
        string firstName = Helpers.GetValidInput("First Name: ");
        string lastName = Helpers.GetValidInput("Last Name: ");
        string email = Helpers.GetValidEmail("E-mail: ");

        IContact existingContact = _contactService.GetContact(email);
        if (existingContact != null)
        {
            DisplayMessage.Message("A contact with the same email already exists, please try again");
        }

        string address = Helpers.GetValidInput("Address: ");
        string phoneNumber = Helpers.GetValidInput("Phone Number: ");

        Guid id = Guid.NewGuid();

        IContact newContact = new Contact(firstName, lastName, email, phoneNumber, address, id);

        _contactService.AddContact(newContact);
        DisplayMessage.Message("New contact added successfully.");
    }

    public void ShowContactDetails()
    {
        Console.WriteLine("Enter email to search for a contact: ");
        string email = Console.ReadLine()!;

        if (!string.IsNullOrEmpty(email))
        {
            IContact contact = _contactService.GetContact(email);
            Console.Clear();
            Console.WriteLine($" ### Contact Details for contact '{contact.FirstName} {contact.LastName}' ###");
            Console.WriteLine("------------------------------------------------------------------------------");
            Console.WriteLine($"{contact.Email}");
            Console.WriteLine($"{contact.Phone}");
            Console.WriteLine($"{contact.Address}");
            Console.WriteLine($"{contact.Id}");
            Console.WriteLine("------------------------------------------------------------------------------");
            DisplayMessage.Message("Test");
        }
        else
        {
            DisplayMessage.Message($"Contact not found. Please try again");
        }
    }

    public void ShowAllContactDetails()
    {
        IEnumerable<IContact> contacts = _contactService.GetContacts();

        Console.Clear();
        Console.WriteLine("### ALL CONTACTS ###");

        foreach (IContact contact in contacts)
        {
            Console.WriteLine($"First Name: {contact.FirstName} \nLast Name: {contact.LastName} \nE-mail: {contact.Email} \nAddress: {contact.Address} \nPhone number: {contact.Phone} \nID: {contact.Id}");
            Console.WriteLine("------------------------------------------------------------------------------");
        }
        DisplayMessage.Message("");
    }

    public void UpdateContact()
    {
        Console.Clear();
        Console.WriteLine("### UPDATE CONTACT ###");
        Console.WriteLine("Enter email of the employee you want to update");
        string email = Console.ReadLine()!;

        try
        {
            if (email != null)
            {
                IContact contactToUpdate = _contactService.GetContact(email);
                Console.WriteLine($"Contact to update: {contactToUpdate.FirstName} ");

                if (contactToUpdate != null)
                {
                    string newFirstName = Helpers.GetValidInput("First Name: ");
                    string newLastName = Helpers.GetValidInput("Last Name: ");
                    string newEmail = Helpers.GetValidEmail("Email: ");
                    string newAddress = Helpers.GetValidInput("Address: ");
                    string newNumber = Helpers.GetValidInput("Phone Number: ");

                    Console.Clear();
                    Console.WriteLine("### AVAILABLE FILES ###");

                    string folderPath = @"C:\EC\csharp\Assignment_AdressBook\Contact_Files";
                    string[] files = Directory.GetFiles(folderPath);

                    _showFolder.AvailableFiles(folderPath);

                    Console.Write("Enter the name of the file you wanna update: ");
                    string currentFileName = Console.ReadLine()!;

                    contactToUpdate.FirstName = newFirstName;
                    contactToUpdate.LastName = newLastName;
                    contactToUpdate.Email = newEmail;
                    contactToUpdate.Address = newAddress;
                    contactToUpdate.Phone = newNumber;

                    _contactService.UpdateContact(contactToUpdate, currentFileName);
                    DisplayMessage.Message($"Contact '{contactToUpdate.FirstName}' successfully updated");
                }
                else
                {
                    DisplayMessage.Message("Contact not found.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR: {ex.Message}");
        }
    }

    public void DeleteContact()
    {
        Console.Clear();
        Console.WriteLine("### DELETE CONTACT ###");
        Console.WriteLine("E-mail of user to remove: ");
        string email = Console.ReadLine()!;

        if (!string.IsNullOrEmpty(email))
        {
            _contactService.RemoveContact(email);
            DisplayMessage.Message($"Contact with e-mail {email} removed successfully.");
        }
        else
        {
            DisplayMessage.Message($"Couldnt find contact with {email}.");
        }
    }
}
