﻿using ClassLibrary_AdressBook.Interfaces;
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
        return null;
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

            if (existingContact is not null)
            {
                existingContact.FirstName = contact.FirstName ?? existingContact.FirstName;
                existingContact.LastName = contact.LastName ?? existingContact.LastName;
                existingContact.Email = contact.Email ?? existingContact.Email;
                existingContact.Phone = contact.Phone ?? existingContact.Phone;
                existingContact.Address = contact.Address ?? existingContact.Address;
                existingContact.Id = contact.Id != Guid.Empty ? contact.Id : existingContact.Id;

                _writer.SaveToFile(fileName, "2", _contactList);
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
    }

    public void RemoveContact(string email)
    {
        try
        {
            var contactToRemove = _contactList.First(e => e.Email == email);
            if (contactToRemove is not null)
            {
                _contactList.Remove(contactToRemove);
            }
        }
        catch (Exception ex) { Debug.WriteLine($"{ex.Message}"); }
    }
}
