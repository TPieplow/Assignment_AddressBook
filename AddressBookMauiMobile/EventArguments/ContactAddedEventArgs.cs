using ClassLibrary_AdressBook.Interfaces;


namespace AddressBookMauiMobile.EventArguments;

public class ContactAddedEventArgs : EventArgs
{
    public IContact AddedContact { get; }

    public ContactAddedEventArgs(IContact addedContact)
    {
        AddedContact = (IContact?)addedContact!;
    }
}
