using ClassLibrary_AdressBook.Interfaces;

namespace Assignment_AdressBook.ViewModels;

/// <summary>
/// Event arguments for the ContactAdded event, contains the added contact.
/// </summary>
public class ContactAddedEventArgs
{
    /// <summary>
    /// Allow get for added contact. Gives permission to "subscribe". 
    /// </summary>
    public IContact AddedContact { get; }

    /// <summary>
    /// Initializes the new instance of the ContactAddedEventArgs class with the specified contact.
    /// </summary>
    /// <param name="addedContact"></param>
    public ContactAddedEventArgs(IContact addedContact)
    {
        AddedContact = addedContact;
    }
}
