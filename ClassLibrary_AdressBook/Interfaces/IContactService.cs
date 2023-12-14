namespace ClassLibrary_AdressBook.Interfaces
{
    public interface IContactService
    {
        /// <summary>
        /// Method that loads all contacts in the file when program starts.
        /// </summary>
        void LoadContactsAtStart();

        /// <summary>
        /// Method that handles adding a contact to the list and saves the list to json-file.
        /// </summary>
        /// <param name="contact">Takes contact as parameter to check if the contact already exist in the list using email</param>
        /// <returns></returns>
        bool AddContact(IContact contact);

        /// <summary>
        /// Method that gets a contact using email to locate the chosen contact.
        /// </summary>
        /// <param name="email">Takes email as a parameter to search for the contact</param>
        /// <returns></returns>
        IContact GetContact(string email);

        /// <summary>
        /// Method that returns a list of all available contacts
        /// </summary>
        /// <returns>List of all contacts</returns>
        IEnumerable<IContact> GetContacts();

        /// <summary>
        /// Method that removes a contact and save it to file.
        /// </summary>
        /// <param name="email">Use email to locate the contact</param>
        void RemoveContact(string email);

        /// <summary>
        /// Method that updates a contact, if no updates are made, the old content will still be there
        /// </summary>
        /// <param name="contact">Takes contact as a parameter to search via email</param>
        /// <returns>True if updated and saved to list/file, else false</returns>
        bool UpdateContact(IContact contact);
    }
}