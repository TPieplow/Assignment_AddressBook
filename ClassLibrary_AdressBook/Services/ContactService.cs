using ClassLibrary_AdressBook.Interfaces;
using ClassLibrary_AdressBook.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ClassLibrary_AdressBook.Services;

public class ContactService : IContactService
{
    private List<IContact> _contactList;
    private readonly IJsonWriter _writer;
    private readonly IJsonReader _reader;
    

    public ContactService(IJsonWriter writer, IJsonReader reader)
    {
        _contactList = new List<IContact>();
        _writer = writer;
        _reader = reader;
    }


    /// <summary>
    /// Method that loads all contacts in the file when program starts.
    /// </summary>
    public void LoadContactsAtStart()
    {
        try
        {
            List<IContact> loadedContacts = _reader.LoadFromFile();
            _contactList?.Clear();
            _contactList?.AddRange(loadedContacts);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    /// <summary>
    /// Method that handles adding a contact to the list and saves the list to json-file.
    /// </summary>
    /// <param name="contact">Takes contact as parameter to check if the contact already exist in the list using email</param>
    /// <returns></returns>
    public bool AddContact(IContact contact)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(contact);
            if (!_contactList.Any(c => c.Email.ToLower() == contact.Email.ToLower()))
            {
                _contactList.Add(contact);
                _writer.SaveToFile(_contactList);

                Console.WriteLine($"Contact added successfully. Total contacts: {_contactList.Count}");
                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            throw;
        }
        return false;
    }

    /// <summary>
    /// Method that gets a contact using email to locate the chosen contact.
    /// </summary>
    /// <param name="email">Takes email as a parameter to search for the contact</param>
    /// <returns></returns>
    public IContact GetContact(string email)
    {
        try
        {
            return _contactList.FirstOrDefault(c => c.Email.ToLower() == email.ToLower())!;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null;
    }

    /// <summary>
    /// Method that returns a list of all available contacts
    /// </summary>
    /// <returns>List of all contacts</returns>
    public IEnumerable<IContact> GetContacts()
    {
        return _contactList.Select(c => (Contact)c);
    }

    /// <summary>
    /// Method that updates a contact, if no updates are made, the old content will still be there
    /// </summary>
    /// <param name="contact">Takes contact as a parameter to search via email</param>
    /// <returns>True if updated and saved to list/file, else false</returns>
    public bool UpdateContact(IContact contact)
    {
        try
        {
            IContact existingContact = GetContact(contact.Email);

            if (existingContact is not null)
            {
                existingContact.FirstName = contact.FirstName ?? existingContact.FirstName;
                existingContact.LastName = contact.LastName ?? existingContact.LastName;
                existingContact.Email = contact.Email ?? existingContact.Email;
                existingContact.Phone = contact.Phone ?? existingContact.Phone;
                existingContact.Address = contact.Address ?? existingContact.Address;
                existingContact.Id = contact.Id != Guid.Empty ? contact.Id : existingContact.Id;

                _writer.SaveToFile(_contactList);
                return true;
            }
        }
        catch (Exception ex) 
        {
            Debug.WriteLine(ex.Message);
        }
        return false;
    }

    /// <summary>
    /// Method that removes a contact and save it to file.
    /// </summary>
    /// <param name="email">Use email to locate the contact</param>
    public void RemoveContact(string email)
    {
        try
        {
            var contactToRemove = _contactList.First(e => e.Email == email);
            if (contactToRemove is not null)
            {
                _contactList.Remove(contactToRemove);
                _writer.SaveToFile(_contactList);
            }
            else
            {
                Debug.WriteLine($"Contact with {email} doesnt exist.");
            }
        }
        catch (Exception ex) { Debug.WriteLine($"{ex.Message}"); }
    }
}
