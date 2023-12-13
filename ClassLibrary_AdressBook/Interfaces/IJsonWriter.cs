namespace ClassLibrary_AdressBook.Interfaces
{
    public interface IJsonWriter
    {
        void SaveToFile(List<IContact> contacts);
    }
}