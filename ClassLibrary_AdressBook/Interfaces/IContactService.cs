namespace ClassLibrary_AdressBook.Interfaces
{
    public interface IContactService
    {
        bool AddContact(IContact contact);
        IContact GetContact(string email);
        IEnumerable<IContact> GetContacts();
        void RemoveContact(string email);
        void UpdateContact(IContact contact, string fileName);
    }
}