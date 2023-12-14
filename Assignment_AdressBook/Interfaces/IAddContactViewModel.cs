using Assignment_AdressBook.EventArguments;

namespace Assignment_AdressBook.Interfaces;

public interface IAddContactViewModel
{
    event EventHandler<ContactAddedEventArgs> ContactAdded;
    void AddContact();
    void BackToMenu();
}
