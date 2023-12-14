namespace ConsoleApp_AdressBook.Interfaces
{
    public interface IContactServiceConsoleApp
    {
        /// <summary>
        /// Method that takes userinput and adds it to list (calling AddContact in classlibrary and save)
        /// </summary>
        void AddContactConsoleApp();

        /// <summary>
        /// Method that takes email from userinput and lotaces a contact to delete
        /// </summary>
        void DeleteContact();

        /// <summary>
        /// Method that shows all contacts for the user
        /// </summary>
        void ShowAllContactDetails();

        /// <summary>
        /// Method that displays one single contact, located by email
        /// </summary>
        void ShowContactDetails();

        /// <summary>
        /// Method that lets the user update a existing contact, using email to locate contact
        /// </summary>
        void UpdateContact();
    }
}