using Assignment_AdressBook.EventArguments;
using ClassLibrary_AdressBook.Interfaces;
using System.Collections.ObjectModel;

namespace Assignment_AdressBook.Interfaces;

public interface IGetContactsViewModel
{
    event EventHandler<ContactAddedEventArgs> ContactAdded;
    ObservableCollection<IContact>? ContactList {  get; }

    /// <summary>
    /// Lets the user navigate to UpdateContact, taking string email as a parameter since we are using email to search for the chosen contact.
    /// </summary>
    /// <param name="email">The email of the contact to update</param>
    void NavigateToEditContact(string email);

    /// <summary>
    /// Method for removing a contact from the list/file. 
    /// Takes email as a parameter since its the identifier 
    /// </summary>
    /// <param name="email">The email of the contact to be removed</param>
    void RemoveContact(string email);

    /// <summary>
    /// Navigation method that allows the user to return to the main page.
    /// Essential for managing user navigation.
    /// </summary>
    void BackToMenu();
}
