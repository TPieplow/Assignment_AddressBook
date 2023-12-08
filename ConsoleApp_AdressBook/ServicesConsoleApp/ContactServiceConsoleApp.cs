﻿using ClassLibrary_AdressBook.Interfaces;
using ClassLibrary_AdressBook.Models;
using ConsoleApp_AdressBook.Interfaces;
using ConsoleApp_AdressBook.UtilitiesConsoleApp;

namespace ConsoleApp_AdressBook.ServiceConsoleApp;

public class ContactServiceConsoleApp : IContactServiceConsoleApp
{
    private readonly IContactService _contactService;
    private readonly ShowFolder _showFolder;

    public ContactServiceConsoleApp(IContactService contactService, ShowFolder showFolder)
    {
        _contactService = contactService;
        _showFolder = showFolder;
    }

    public void AddContactConsoleApp()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("### ADD NEW CONTACT ###");
            string firstName = Helpers.GetValidInput("First Name: ");
            string lastName = Helpers.GetValidInput("Last Name: ");
            string email = Helpers.GetValidEmail("E-mail: ");

            IContact existingContact = _contactService.GetContact(email);
            if (existingContact is not null)
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
        catch (Exception ex) { Console.WriteLine($"{ex.Message}"); }
    }

    public void ShowContactDetails()
    {
        try
        {
            Console.WriteLine("Enter email to search for a contact: ");
            string email = Console.ReadLine() ?? string.Empty;

            if (!string.IsNullOrEmpty(email))
            {
                IContact contact = _contactService?.GetContact(email)!;
                Console.Clear();
                Console.WriteLine($"Contact information for: {contact.FirstName} {contact.LastName}");
                ContactInformation.DisplayDetails(contact);
                DisplayMessage.Message("");
            }
            else
            {
                DisplayMessage.Message($"Contact not found. Please try again");
            }
        }
        catch (Exception ex) { Console.WriteLine($"{ex.Message}"); }
    }

    public void ShowAllContactDetails()
    {
        try
        {
            IEnumerable<IContact> contacts = _contactService?.GetContacts()!;

            Console.Clear();
            Console.WriteLine("### ALL CONTACTS ###");

            if (contacts is not null && contacts.Any())
            {
                foreach (IContact contact in contacts)
                {
                    ContactInformation.DisplayDetails(contact);
                }
                DisplayMessage.Message("");
            }
            else
            {
                DisplayMessage.Message("No contacts available, try loading a file...");
            }
        }
        catch (Exception ex) { Console.WriteLine($"{ex.Message}"); }
    }

    public void UpdateContact()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("### UPDATE CONTACT ###");
            Console.WriteLine("Enter email of the employee you want to update");
            string email = Console.ReadLine() ?? string.Empty!;

            if (email is not null)
            {
                IContact contactToUpdate = _contactService?.GetContact(email)!;
                Console.WriteLine($"Contact to update: {contactToUpdate.FirstName} ");

                if (contactToUpdate is not null)
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
                    _showFolder?.AvailableFiles(folderPath);

                    Console.Write("Enter the name of the file you wanna update: ");
                    string currentFileName = Console.ReadLine() ?? string.Empty!;

                    contactToUpdate.FirstName = newFirstName;
                    contactToUpdate.LastName = newLastName;
                    contactToUpdate.Email = newEmail;
                    contactToUpdate.Address = newAddress;
                    contactToUpdate.Phone = newNumber;

                    _contactService?.UpdateContact(contactToUpdate, currentFileName);
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
        try
        {
            string folderPath = @"C:\EC\csharp\Assignment_AdressBook\Contact_Files";
            Console.Clear();
            Console.WriteLine("### DELETE CONTACT ###");
            Console.WriteLine("E-mail of user to remove: ");
            string email = Console.ReadLine() ?? string.Empty!;
            _showFolder.AvailableFiles(folderPath);
            Console.WriteLine("Enter name of file where you wanna delete: ");
            string fileName = Console.ReadLine() ?? string.Empty!.Trim();

            if (!string.IsNullOrEmpty(email))
            {
                _contactService?.RemoveContact(email, fileName);
                DisplayMessage.Message($"Contact with e-mail {email} removed successfully.");
            }
            else
            {
                DisplayMessage.Message($"Couldnt find contact with {email}.");
            }
        }
        catch (Exception ex) { Console.WriteLine($"{ex.Message}"); }
    }
}
