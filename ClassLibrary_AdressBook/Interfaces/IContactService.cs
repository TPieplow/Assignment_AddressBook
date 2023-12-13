namespace ClassLibrary_AdressBook.Interfaces
{
    public interface IContactService
    {
        void LoadContactsAtStart();
        bool AddContact(IContact contact);
        IContact GetContact(string email);
        IEnumerable<IContact> GetContacts();
        void RemoveContact(string email);
        bool UpdateContact(IContact contact);
    }
}