using ClassLibrary_AdressBook.Interfaces;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ClassLibrary_AdressBook.Services;

public class ContactService : IContactService
{
    public List<IContact> _contactList;
    private readonly IJsonWriter _writer;

    public ContactService(IJsonWriter writer)
    {
        _contactList = new List<IContact>();
        _writer = writer;
    }

    public bool AddContact(IContact contact)
    {
        try
        {
            if (!_contactList.Any(c => c.Email == contact.Email))
            {
                _contactList.Add(contact);
                Debug.WriteLine($"{contact.FirstName}");
                return true;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

    public IContact GetContact(string email)
    {
        try
        {
            return _contactList.FirstOrDefault(c => c.Email == email)!;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public IEnumerable<IContact> GetContacts()
    {
        return _contactList;
    }

    public void UpdateContact(IContact contact, string fileName)
    {
        try
        {
            IContact existingContact = GetContact(contact.Email);

            if (existingContact != null)
            {
                existingContact.FirstName = contact.FirstName;
                existingContact.LastName = contact.LastName;
                existingContact.Email = contact.Email;
                existingContact.Phone = contact.Phone;
                existingContact.Address = contact.Address;
                existingContact.Id = contact.Id;    

                _writer.SaveToFile(fileName, "2", _contactList);
            }
        } catch (Exception ex) { Debug.WriteLine(ex.Message); }
    }

    public void RemoveContact(string email)
    {
        var contactToRemove = _contactList.First(e => e.Email == email);
        if (contactToRemove != null)
        {
            _contactList.Remove(contactToRemove);
        }
    }
}
