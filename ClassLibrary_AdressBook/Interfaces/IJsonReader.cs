namespace ClassLibrary_AdressBook.Interfaces
{
    public interface IJsonReader
    {
        List<IContact> LoadFromFile(string fileName);
    }
}