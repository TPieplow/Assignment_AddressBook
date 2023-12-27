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
        _writer = writer;
        _reader = reader;
        _contactList = _reader.LoadFromFile();
    }

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

    public IContact GetContact(string email)
    {
        try
        {
            return _contactList.FirstOrDefault(c => c.Email.ToLower() == email.ToLower())!;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null;
    }

    public IEnumerable<IContact> GetContacts()
    {
        return _contactList.Select(c => (Contact)c);
    }

    public bool UpdateContact(IContact contact)
    {
        try
        {
            if (contact is not null)
            {

                IContact existingContact = _contactList.FirstOrDefault(c => string.Equals(c.Email, contact.Email, StringComparison.OrdinalIgnoreCase))!;

                if (existingContact is null)
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
                else
                {
                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return false;
    }

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
