using Assignment_AdressBook.EventArguments;

namespace Assignment_AdressBook.Interfaces;

public interface IAddContactViewModel
{

    /// <summary>
    /// Eventhandler reacting to newly added contacts
    /// </summary>
    event EventHandler<ContactAddedEventArgs> ContactAdded;

    /// <summary>
    /// Adds a contact via user input
    /// </summary>
    void AddContact();

    /// <summary>
    /// Takes the user back to main menu
    /// </summary>
    void BackToMenu();
}
