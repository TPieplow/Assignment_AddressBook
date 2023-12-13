using ClassLibrary_AdressBook.Interfaces;

namespace Assignment_AdressBook.ViewModels;

public class ContactAddedEventArgs
{
    public IContact AddedContact { get; }

    public ContactAddedEventArgs(IContact addedContact)
    {
        AddedContact = addedContact;
    }
}
