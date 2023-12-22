using ClassLibrary_AdressBook.Interfaces;

namespace Assignment_AdressBook.Interfaces;

public interface IUpdateContactViewModel
{
    IContact? Contact { get; set; }

    /// <summary>
    /// Method responsible of calling the right contact to update.
    /// </summary>
    /// <param name="email">Taking email as a parameter to find the right contact to update.</param>
    void EditContact(string email);

    /// <summary>
    /// Method for calling the UpdateContact method. 
    /// </summary>
    /// <param name="contact">Takes a contact to update</param>
    void SaveAndUpdate(IContact contact);

    /// <summary>
    /// Navigation method that allows the user to return to the main page.
    /// Essential for managing user navigation.
    /// </summary>
    void BackToMenu();

}
